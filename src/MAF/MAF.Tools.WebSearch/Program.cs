using Azure;
using Azure.AI.Agents.Persistent;
using Azure.Identity;
using Spectre.Console;

AnsiConsole.MarkupLine("[blue]Tools Agent Example: Web Search with Bing[/]");

#region ENV variables

var endpoint = Environment.GetEnvironmentVariable("ENDPOINT");
ArgumentException.ThrowIfNullOrEmpty(endpoint, "ENDPOINT environment variable is not set.");
var deploymentName = Environment.GetEnvironmentVariable("DEPLOYMENTNAME");
ArgumentException.ThrowIfNullOrEmpty(deploymentName, "DEPLOYMENTNAME environment variable is not set.");
var bingConnectionId = Environment.GetEnvironmentVariable("BingConnectionId");
ArgumentException.ThrowIfNullOrEmpty(bingConnectionId, "BingConnectionId environment variable is not set.");

AnsiConsole.MarkupLine("[blue]Endpoint [/]: " + endpoint);
AnsiConsole.MarkupLine("[blue]Deployment Name[/]: " + deploymentName);
AnsiConsole.MarkupLine("[blue]Bing Connection ID[/]: " + bingConnectionId);

#endregion

PersistentAgentsClient agentClient = new(endpoint, new DefaultAzureCredential());

var bingTool = new BingGroundingToolDefinition(
    new BingGroundingSearchToolParameters([
        new BingGroundingSearchConfiguration(connectionId: bingConnectionId)
    ])
);

PersistentAgent agent = agentClient.Administration.CreateAgent(
    model: deploymentName,
    name: "my-agent-with-search",
    instructions: "You are a helpful agent.",
    tools: [bingTool]);

var question = AnsiConsole.Ask<string>("Ask your question?");
AnsiConsole.MarkupLine("[green]Question:[/]" + question);
var thread = agentClient.Threads.CreateThread();
// Create message to thread
PersistentThreadMessage message = agentClient.Messages.CreateMessage(
    thread.Value.Id,
    MessageRole.User,
    question);

// Run the agent
ThreadRun run = agentClient.Runs.CreateRun(thread, agent);
do
{
    Thread.Sleep(TimeSpan.FromMilliseconds(500));
    run = agentClient.Runs.GetRun(thread.Value.Id, run.Id);
}
while (run.Status == RunStatus.Queued
       || run.Status == RunStatus.InProgress);

Pageable<PersistentThreadMessage> messages = agentClient.Messages.GetMessages(
    threadId: thread.Value.Id,
    order: ListSortOrder.Ascending
);

foreach (PersistentThreadMessage threadMessage in messages)
{
    AnsiConsole.Markup($"[gray]{threadMessage.CreatedAt:yyyy-MM-dd HH:mm:ss}[/] - [blue]{threadMessage.Role,10}[/]: ");
    foreach (MessageContent contentItem in threadMessage.ContentItems)
    {
        switch (contentItem)
        {
            case MessageTextContent textItem:
            {
                var response = textItem.Text;
                if (textItem.Annotations != null)
                {
                    foreach (MessageTextAnnotation annotation in textItem.Annotations)
                    {
                        if (annotation is MessageTextUriCitationAnnotation uriAnnotation)
                        {
                            response = response.Replace(uriAnnotation.Text, $" [{uriAnnotation.UriCitation.Title}]({uriAnnotation.UriCitation.Uri})");
                        }
                    }
                }
                AnsiConsole.Markup($"[green]Agent response:[/] {response}");
                break;
            }
            case MessageImageFileContent imageFileItem:
                AnsiConsole.Write($"<image from ID: {imageFileItem.FileId}");
                break;
        }

        AnsiConsole.WriteLine();
    }
}