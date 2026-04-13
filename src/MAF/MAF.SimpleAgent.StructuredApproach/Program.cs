using Azure.AI.Projects;
using Azure.Identity;
using MAF.SimpleAgent.StructuredApproach;
using Microsoft.Agents.AI;
using Spectre.Console;

AnsiConsole.MarkupLine("[blue]Simple Agent With Structured output[/]");

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
        name: "SimpleAgentWithStructuredOutput");
var question = AnsiConsole.Ask<string>("Ask your question",
    "Please provide information about John Smith, who is a 35-year-old software engineer.");
AnsiConsole.MarkupLine("[green]Question:[/]" + question);
AgentResponse<PersonInfo> response = await agent.RunAsync<PersonInfo>(question);
AnsiConsole.WriteLine($"Name: {response.Result.Name}, Age: {response.Result.Age}, Occupation: {response.Result.Occupation}");
