namespace MAF.Middleware.Helpers;

/// <summary>
/// Provides content-filtering guardrail logic for agent input messages.
/// </summary>
public static class GuardrailHelper
{
    private static readonly string[] BlockedWords = ["password", "secret", "credentials"];

    /// <summary>
    /// Checks whether the given message contains any blocked words.
    /// </summary>
    /// <param name="message">The user message to check.</param>
    /// <returns>The matched blocked word, or <c>null</c> if no blocked word was found.</returns>
    public static string? FindBlockedWord(string message)
    {
        var lower = message.ToLowerInvariant();
        foreach (var word in BlockedWords)
        {
            if (lower.Contains(word))
                return word;
        }
        return null;
    }

    /// <summary>Returns all configured blocked words.</summary>
    public static IReadOnlyList<string> GetBlockedWords() => BlockedWords;
}
