using Azure.AI.OpenAI;
using Azure.Identity;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using Spectre.Console;

AnsiConsole.MarkupLine("[blue]Skills Agent with File example[/]");

#region ENV variables

var endpoint = Environment.GetEnvironmentVariable("ENDPOINT");
ArgumentException.ThrowIfNullOrEmpty(endpoint, "ENDPOINT environment variable is not set.");
var deploymentName = Environment.GetEnvironmentVariable("DEPLOYMENTNAME");
ArgumentException.ThrowIfNullOrEmpty(deploymentName, "DEPLOYMENTNAME environment variable is not set.");
var skillFolderPath = Environment.GetEnvironmentVariable("SKILLSFOLDERPATH");
ArgumentException.ThrowIfNullOrEmpty(skillFolderPath, "SKILLSFOLDERPATH environment variable is not set.");
var htmlPath = Environment.GetEnvironmentVariable("HTMLPATH");
ArgumentException.ThrowIfNullOrEmpty(htmlPath, "HTMLPATH environment variable is not set.");

AnsiConsole.MarkupLine("[blue]Endpoint [/]: " + endpoint);
AnsiConsole.MarkupLine("[blue]Deployment Name[/]: " + deploymentName);
AnsiConsole.MarkupLine("[blue]Skills Folder Name[/]: " + skillFolderPath);
AnsiConsole.MarkupLine("[blue]Html file[/]: " + htmlPath);

#endregion

// Discover skills from the 'skills' directory
var skillsProvider = new AgentSkillsProvider(skillFolderPath);

// Create an agent with the skills provider
AIAgent agent = new AzureOpenAIClient(new Uri(endpoint), new DefaultAzureCredential())
    .GetResponsesClient()
    .AsIChatClient(deploymentName)
    .AsAIAgent(new ChatClientAgentOptions
        {
            Name = "SkillsAgentWithFileSkills",
            ChatOptions = new()
            {
                Instructions = "You are a helpful assistant.",
            },
            AIContextProviders = [skillsProvider],
        });
var question = "Can you convert this to markdown?";

AnsiConsole.MarkupLine("[green]Question:[/]" + question);
var answer = await agent.RunAsync(question);
AnsiConsole.MarkupLine("[green]Answer:[/]" + answer);