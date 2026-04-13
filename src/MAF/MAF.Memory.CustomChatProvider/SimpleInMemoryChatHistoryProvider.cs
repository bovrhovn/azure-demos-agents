using System.Text.Json.Serialization;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;

namespace MAF.Memory.CustomChatProvider;

public sealed class SimpleInMemoryChatHistoryProvider(
    Func<AgentSession?, SimpleInMemoryChatHistoryProvider.State>? stateInitializer = null,
    string? stateKey = null)
    : ChatHistoryProvider
{
    private readonly ProviderSessionState<State> _sessionState = new(
        stateInitializer ?? (_ => new State()),
        stateKey ?? nameof(SimpleInMemoryChatHistoryProvider));

    public string StateKey => this._sessionState.StateKey;

    protected override ValueTask<IEnumerable<ChatMessage>> ProvideChatHistoryAsync(InvokingContext context, CancellationToken cancellationToken = default) =>
        // return all messages in the session state
        new(this._sessionState.GetOrInitializeState(context.Session).Messages);

    protected override ValueTask StoreChatHistoryAsync(InvokedContext context, CancellationToken cancellationToken = default)
    {
        var state = this._sessionState.GetOrInitializeState(context.Session);

        // Add both request and response messages to the session state.
        var allNewMessages = context.RequestMessages.Concat(context.ResponseMessages ?? []);
        state.Messages.AddRange(allNewMessages);

        this._sessionState.SaveState(context.Session, state);

        return default;
    }

    public sealed class State
    {
        [JsonPropertyName("messages")]
        public List<ChatMessage> Messages { get; set; } = [];
    }
}