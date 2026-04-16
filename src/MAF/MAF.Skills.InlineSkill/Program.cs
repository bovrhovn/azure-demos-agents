using Azure.AI.OpenAI;
using Azure.Identity;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using Spectre.Console;

AnsiConsole.MarkupLine("[blue]Skills Agent with Inline example[/]");

#region ENV variables

var endpoint = Environment.GetEnvironmentVariable("ENDPOINT");
ArgumentException.ThrowIfNullOrEmpty(endpoint, "ENDPOINT environment variable is not set.");
var deploymentName = Environment.GetEnvironmentVariable("DEPLOYMENTNAME");
ArgumentException.ThrowIfNullOrEmpty(deploymentName, "DEPLOYMENTNAME environment variable is not set.");

AnsiConsole.MarkupLine("[blue]Endpoint [/]: " + endpoint);
AnsiConsole.MarkupLine("[blue]Deployment Name[/]: " + deploymentName);

#endregion

// Add dynamic skills inline
var projectInfoSkill = new AgentInlineSkill(
        name: "project-info",
        description: "Project status and configuration information",
        instructions: """
                      Use this skill for questions about the current project.
                      1. Read the environment resource for deployment configuration details.
                      2. Read the team-roster resource for information about team members.
                      """)
    .AddResource("environment", () =>
    {
        var env = Environment.GetEnvironmentVariable("APP_ENV") ?? "development";
        var region = Environment.GetEnvironmentVariable("SKILL_SAMPLE_APP_REGION") ?? "SwedenCentral";
        return $"Environment: {env}, Region: {region}";
    })
    .AddResource(
        "team-roster",
        "Radoslaw TheBoss (Tech Lead), Bojan TheWorkerBee (Backend Engineer)");

var skillsProvider = new AgentSkillsProvider(projectInfoSkill);
// Create an agent with the skills provider
AIAgent agent = new AzureOpenAIClient(new Uri(endpoint), new DefaultAzureCredential())
    .GetResponsesClient()
    .AsIChatClient(deploymentName)
    .AsAIAgent(new ChatClientAgentOptions
    {
        Name = "SkillsAgentWithInlineSkills",
        ChatOptions = new()
        {
            Instructions = "You are a helpful assistant.",
        },
        AIContextProviders = [skillsProvider],
    });
var question = "Give me latest information about the project?";
AnsiConsole.MarkupLine("[green]Question:[/]" + question);
var answer = await agent.RunAsync(question);
AnsiConsole.MarkupLine("[green]Answer:[/]" + answer);