using Azure.AI.Projects;
using Azure.Identity;
using Microsoft.Agents.AI;
using Spectre.Console;

AnsiConsole.MarkupLine("[blue]Session Context Simple Agent Example[/]");

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
        name: "SessionContext");
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

var answerAboutMe = await agent.RunAsync("What do you remember about me?", session);;
AnsiConsole.MarkupLine("[green]Answer:[/]" + answerAboutMe);