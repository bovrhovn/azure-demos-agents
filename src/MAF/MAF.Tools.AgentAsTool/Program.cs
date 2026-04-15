using Azure.AI.Projects;
using Azure.Identity;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using Spectre.Console;

AnsiConsole.MarkupLine("[blue]Tools Agent Example: Call tool as another agent[/]");

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
        tools: [AIFunctionFactory.Create(CalculateTaxMethod)]);
var question = AnsiConsole.Ask<string>("Ask your question",
    "What is the tax for customer TheCustomer for last 6 months?");

AIAgent mainAgent = new AIProjectClient(
        new Uri(endpoint),
        new DefaultAzureCredential())
    .AsAIAgent(
        model: deploymentName,
        name:"MainAgentWithAgentAsTool",
        instructions: "You are a helpful assistant.",
        tools: [agent.AsAIFunction()]);

AnsiConsole.MarkupLine("[green]Question:[/]" + question);
var answer = await mainAgent.RunAsync(question);
AnsiConsole.MarkupLine("[green]Answer:[/]" + answer);

static float CalculateTaxMethod(string customerName, int months)
{
    // In real world scenario, you would have complex logic here to calculate tax based on customer and months
    var baseTax = 100; // base tax amount
    var tax = baseTax * months; // simple calculation for demonstration
    return tax;
}