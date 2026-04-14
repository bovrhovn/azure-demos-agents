using Azure.AI.OpenAI;
using Azure.AI.Projects;
using Azure.Identity;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using Spectre.Console;

AnsiConsole.MarkupLine("[blue]Simple Agent With RAG with text provider[/]");

#region ENV variables

var endpoint = Environment.GetEnvironmentVariable("ENDPOINT");
ArgumentException.ThrowIfNullOrEmpty(endpoint, "ENDPOINT environment variable is not set.");
var deploymentName = Environment.GetEnvironmentVariable("DEPLOYMENTNAME");
ArgumentException.ThrowIfNullOrEmpty(deploymentName, "DEPLOYMENTNAME environment variable is not set.");

AnsiConsole.MarkupLine("[blue]Endpoint [/]: " + endpoint);
AnsiConsole.MarkupLine("[blue]Deployment Name[/]: " + deploymentName);

#endregion

TextSearchProviderOptions textSearchOptions = new()
{
    SearchTime = TextSearchProviderOptions.TextSearchBehavior.BeforeAIInvoke,
};

IChatClient client =
    new ChatClientBuilder(
            new AzureOpenAIClient(new Uri(endpoint), new DefaultAzureCredential())
                .GetChatClient(deploymentName)
                .AsIChatClient())
        .Build();

AIAgent agent = client
    .AsAIAgent(new ChatClientAgentOptions()
    {
        ChatOptions = new()
        {
            Instructions = "You are a helpful support specialist. Answer questions using the provided context and cite the source document when available."
        },
        AIContextProviders = [new TextSearchProvider(SearchAdapter, textSearchOptions)] 
    });
var question = AnsiConsole.Ask<string>("Ask your question",
    "What is the return policy?");
AnsiConsole.MarkupLine("[green]Question:[/]" + question);
AgentSession session = await agent.CreateSessionAsync();
var agentResponse = await agent.RunAsync(question, session);
AnsiConsole.MarkupLine("[green]Answer: [/]" + agentResponse.Text);

static Task<IEnumerable<TextSearchProvider.TextSearchResult>> SearchAdapter(string query, CancellationToken cancellationToken)
{
    // The mock search inspects the user's question and returns pre-defined snippets
    // that resemble documents stored in an external knowledge source.
    List<TextSearchProvider.TextSearchResult> results = new();
    if (query.Contains("return", StringComparison.OrdinalIgnoreCase) || query.Contains("refund", StringComparison.OrdinalIgnoreCase))
    {
        results.Add(new()
        {
            SourceName = "Contoso Outdoors Return Policy",
            SourceLink = "https://contoso.com/policies/returns",
            Text = "Customers may return any item within 30 days of delivery. Items should be unused and include original packaging. Refunds are issued to the original payment method within 5 business days of inspection."
        });
    }

    return Task.FromResult<IEnumerable<TextSearchProvider.TextSearchResult>>(results);
}