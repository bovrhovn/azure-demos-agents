using Azure.AI.OpenAI;
using Azure.AI.Projects;
using Azure.Identity;
using Microsoft.Agents.AI;
using Microsoft.Agents.AI.Compaction;
using Microsoft.Extensions.AI;
using Spectre.Console;

AnsiConsole.MarkupLine("[blue]Session Compaction Agent Example[/]");

#region ENV variables

var endpoint = Environment.GetEnvironmentVariable("ENDPOINT");
ArgumentException.ThrowIfNullOrEmpty(endpoint, "ENDPOINT environment variable is not set.");
var deploymentName = Environment.GetEnvironmentVariable("DEPLOYMENTNAME");
ArgumentException.ThrowIfNullOrEmpty(deploymentName, "DEPLOYMENTNAME environment variable is not set.");

AnsiConsole.MarkupLine("[blue]Endpoint [/]: " + endpoint);
AnsiConsole.MarkupLine("[blue]Deployment Name[/]: " + deploymentName);

#endregion

var chatClient = new AzureOpenAIClient(new Uri(endpoint), new DefaultAzureCredential())
    .GetChatClient(deploymentName)
    .AsIChatClient();

var pipeline = new PipelineCompactionStrategy(
    new ToolResultCompactionStrategy(CompactionTriggers.TokensExceed(0x200)),
    new SlidingWindowCompactionStrategy(CompactionTriggers.TurnsExceed(4)),
    new TruncationCompactionStrategy(CompactionTriggers.TokensExceed(0x8000)));

AIAgent agent =
    chatClient.AsBuilder()
        .UseAIContextProviders(new CompactionProvider(pipeline))
        .BuildAIAgent(new ChatClientAgentOptions
        {
            Name = "SessionContext",
            ChatOptions = new() { Instructions = "You are a friendly assistant. Keep your answers brief." }
        });

var question = AnsiConsole.Ask<string>("Ask your question",
    "What is the 2nd largest city in United States by population size?");
AnsiConsole.MarkupLine("[green]Question:[/]" + question);
var answer = await agent.RunAsync(question);
AnsiConsole.MarkupLine("[green]Answer:[/]" + answer);