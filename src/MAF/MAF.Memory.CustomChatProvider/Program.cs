using System.Text.Json;
using Azure.AI.Projects;
using Azure.Identity;
using MAF.Memory.CustomChatProvider;
using Microsoft.Agents.AI;
using Microsoft.Agents.AI.Compaction;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.VectorData;
using Spectre.Console;


AnsiConsole.MarkupLine("[blue]Custom Chat provider Agent Example[/]");

#region ENV variables

var endpoint = Environment.GetEnvironmentVariable("ENDPOINT");
ArgumentException.ThrowIfNullOrEmpty(endpoint, "ENDPOINT environment variable is not set.");
var deploymentName = Environment.GetEnvironmentVariable("DEPLOYMENTNAME");
ArgumentException.ThrowIfNullOrEmpty(deploymentName, "DEPLOYMENTNAME environment variable is not set.");

AnsiConsole.MarkupLine("[blue]Endpoint [/]: " + endpoint);
AnsiConsole.MarkupLine("[blue]Deployment Name[/]: " + deploymentName);

#endregion


AIAgent agent = new AIProjectClient(new Uri(endpoint), new DefaultAzureCredential())
    .AsAIAgent(new ChatClientAgentOptions
    {
        ChatOptions = new() { Instructions = "You are good at telling jokes." },
        Name = "Joker",
        ChatHistoryProvider = new InMemoryChatHistoryProvider()
    });
var question = AnsiConsole.Ask<string>("Ask your question",
    "What is the 2nd largest city in United States by population size?");
var session = await agent.CreateSessionAsync();
while (question != "EXIT")
{
    AnsiConsole.MarkupLine("[green]Question:[/]" + question);
    var answer = await agent.RunAsync(question, session);
    AnsiConsole.MarkupLine("[green]Answer:[/]" + answer);
    question = AnsiConsole.Ask<string>("Ask your next question","EXIT");
}
JsonElement serialized = await agent.SerializeSessionAsync(session);
// Store serialized payload in durable storage.
AnsiConsole.MarkupLine("[blue]Serialized Session Context:[/]");
AnsiConsole.WriteLine(serialized.ToString());
//AgentSession resumed = await agent.DeserializeSessionAsync(serialized);
