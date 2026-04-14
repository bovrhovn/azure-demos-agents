using Azure.AI.Projects;
using Azure.Identity;
using Microsoft.Agents.AI;
using Spectre.Console;

AnsiConsole.MarkupLine("[blue]Simple Agent With Background Responses[/]");

#region ENV variables

var endpoint = Environment.GetEnvironmentVariable("ENDPOINT");
ArgumentException.ThrowIfNullOrEmpty(endpoint, "ENDPOINT environment variable is not set.");
var deploymentName = Environment.GetEnvironmentVariable("DEPLOYMENTNAME");
ArgumentException.ThrowIfNullOrEmpty(deploymentName, "DEPLOYMENTNAME environment variable is not set.");

AnsiConsole.MarkupLine("[blue]Endpoint [/]: " + endpoint);
AnsiConsole.MarkupLine("[blue]Deployment Name[/]: " + deploymentName);

#endregion

AIAgent agent = new AIProjectClient(new Uri(endpoint), new DefaultAzureCredential())
    .AsAIAgent(
        model: deploymentName,
        instructions: "You are a friendly assistant.",
        name: "SimpleAgentWithBackgrondResponses");
var question = AnsiConsole.Ask<string>("Ask your question",
    "do long poem about development in the style of Lord of the Rings");
AnsiConsole.MarkupLine("[green]Question:[/]" + question);
AgentRunOptions options = new()
{
    AllowBackgroundResponses = true
};

AgentSession session = await agent.CreateSessionAsync();
AnsiConsole.MarkupLine("[green]Session created, creating long poem.[/]");
AgentResponse response = await agent.RunAsync(question, session, options);
// Continue to poll until the final response is received
while (response is { ContinuationToken: { } token })
{
    // Wait before polling again.
    await Task.Delay(TimeSpan.FromSeconds(2));
    options.ContinuationToken = token;
    response = await agent.RunAsync(session, options);
}
AnsiConsole.MarkupLine("[green]Long poem done.[/]");