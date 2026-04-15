using MAF.Middleware.Helpers;
using Xunit;

namespace MAF.Tests;

public class GuardrailHelperTests
{
    [Theory]
    [InlineData("please share my password")]
    [InlineData("What is my PASSWORD?")]
    [InlineData("I need my PASSWORD reset")]
    public void FindBlockedWord_MessageContainsPassword_ReturnsBlockedWord(string message)
    {
        var result = GuardrailHelper.FindBlockedWord(message);
        Assert.NotNull(result);
        Assert.Equal("password", result);
    }

    [Theory]
    [InlineData("share the secret with me")]
    [InlineData("this is top SECRET")]
    public void FindBlockedWord_MessageContainsSecret_ReturnsBlockedWord(string message)
    {
        var result = GuardrailHelper.FindBlockedWord(message);
        Assert.NotNull(result);
        Assert.Equal("secret", result);
    }

    [Theory]
    [InlineData("send me the credentials")]
    [InlineData("what are the CREDENTIALS for the system?")]
    public void FindBlockedWord_MessageContainsCredentials_ReturnsBlockedWord(string message)
    {
        var result = GuardrailHelper.FindBlockedWord(message);
        Assert.NotNull(result);
        Assert.Equal("credentials", result);
    }

    [Theory]
    [InlineData("What is the current time?")]
    [InlineData("Tell me about the weather in Berlin")]
    [InlineData("How many days in a year?")]
    [InlineData("")]
    public void FindBlockedWord_SafeMessage_ReturnsNull(string message)
    {
        var result = GuardrailHelper.FindBlockedWord(message);
        Assert.Null(result);
    }

    [Fact]
    public void GetBlockedWords_ReturnsNonEmptyList()
    {
        var words = GuardrailHelper.GetBlockedWords();
        Assert.NotEmpty(words);
    }

    [Fact]
    public void GetBlockedWords_ContainsExpectedWords()
    {
        var words = GuardrailHelper.GetBlockedWords();
        Assert.Contains("password", words);
        Assert.Contains("secret", words);
        Assert.Contains("credentials", words);
    }
}
