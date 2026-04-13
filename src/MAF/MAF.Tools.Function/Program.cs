using Azure.AI.Projects;
using Azure.Identity;
using MAF.Tools.Function.Helpers;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using Spectre.Console;

AnsiConsole.MarkupLine("[blue]Tools Agent Example: Call with function[/]");

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
        name: "SimpleAgentWithFunction",
        tools: [AIFunctionFactory.Create(TaxMethod.CalculateTaxMethod)]);
var question = AnsiConsole.Ask<string>("Ask your question",
    "What is the tax for customer TheCustomer for last 6 months?");
AnsiConsole.MarkupLine("[green]Question:[/]" + question);
var answer = await agent.RunAsync(question);
AnsiConsole.MarkupLine("[green]Answer:[/]" + answer);