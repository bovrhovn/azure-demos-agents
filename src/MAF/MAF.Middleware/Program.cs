using System.ComponentModel;
using Azure.AI.Projects;
using Azure.Identity;
using MAF.Middleware.Helpers;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using Spectre.Console;

AnsiConsole.MarkupLine("[blue]Agent Middleware Example[/]");

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
        instructions: "You are a friendly assistant. Keep your answers brief.",
        tools: [AIFunctionFactory.Create(GetDateTime, name: nameof(GetDateTime))],
        name: "SimpleAgentWithDateTime");
var middlewareEnabledAgent = agent
    .AsBuilder()
    .Use(runFunc: CustomAgentRunMiddleware, runStreamingFunc: null)
    .Build();
var question = AnsiConsole.Ask<string>("Ask your question",
    "What is the current time in seconds?");

while (question.ToUpper() != "EXIT")
{
    AnsiConsole.MarkupLine("[green]Question:[/]" + question);
    var answer = await middlewareEnabledAgent.RunAsync(question);
    AnsiConsole.MarkupLine("[green]Answer:[/]" + answer);
    question = AnsiConsole.Ask<string>("Ask another question", "EXIT");
}

[Description("The current datetime offset.")]
static string GetDateTime() => DateTimeOffset.Now.ToString();

async Task<AgentResponse> CustomAgentRunMiddleware(
    IEnumerable<ChatMessage> messages,
    AgentSession? session,
    AgentRunOptions? options,
    AIAgent innerAgent,
    CancellationToken cancellationToken)
{
    var lastMessage = messages.LastOrDefault()?.Text?.ToLower() ?? "";
    var blockedWord = GuardrailHelper.FindBlockedWord(lastMessage);

    if (blockedWord is not null)
    {
        Console.WriteLine($"[Guardrail] Blocked request containing '{blockedWord}'.");
        return new AgentResponse([
            new ChatMessage(ChatRole.Assistant,
                $"Sorry, I cannot process requests containing '{blockedWord}'.")
        ]);
    }

    Console.WriteLine($"Input: {messages.Count()}");
    var response = await innerAgent.RunAsync(messages, session, options, cancellationToken).ConfigureAwait(false);
    Console.WriteLine($"Output: {response.Messages.Count}");
    return response;
}