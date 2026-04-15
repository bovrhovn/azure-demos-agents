using MAF.Rag.TextSearchProvider.Helpers;
using Xunit;

namespace MAF.Tests;

public class DocumentSearchAdapterTests
{
    [Theory]
    [InlineData("What is the return policy?")]
    [InlineData("How do I return an item?")]
    [InlineData("Can I get a refund?")]
    [InlineData("RETURN my order")]
    [InlineData("REFUND request")]
    public void Search_ReturnOrRefundQuery_ReturnsResults(string query)
    {
        var results = DocumentSearchAdapter.Search(query).ToList();
        Assert.NotEmpty(results);
    }

    [Fact]
    public void Search_ReturnQuery_ReturnsContosoReturnPolicy()
    {
        var results = DocumentSearchAdapter.Search("return policy").ToList();

        Assert.Single(results);
        Assert.Equal("Contoso Outdoors Return Policy", results[0].SourceName);
        Assert.Equal("https://contoso.com/policies/returns", results[0].SourceLink);
        Assert.Contains("30 days", results[0].Text);
    }

    [Fact]
    public void Search_RefundQuery_ReturnsResultWithRefundText()
    {
        var results = DocumentSearchAdapter.Search("refund process").ToList();

        Assert.NotEmpty(results);
        Assert.Contains("5 business days", results[0].Text);
    }

    [Theory]
    [InlineData("What is the weather today?")]
    [InlineData("Tell me a joke")]
    [InlineData("How to deploy to Azure?")]
    [InlineData("")]
    public void Search_UnrelatedQuery_ReturnsEmpty(string query)
    {
        var results = DocumentSearchAdapter.Search(query).ToList();
        Assert.Empty(results);
    }

    [Fact]
    public void Search_NullQuery_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => DocumentSearchAdapter.Search(null!).ToList());
    }

    [Fact]
    public void Search_ReturnQuery_ResultHasNonEmptySourceLink()
    {
        var results = DocumentSearchAdapter.Search("return an item").ToList();

        Assert.NotEmpty(results);
        Assert.All(results, r =>
        {
            Assert.False(string.IsNullOrWhiteSpace(r.SourceLink));
            Assert.False(string.IsNullOrWhiteSpace(r.SourceName));
            Assert.False(string.IsNullOrWhiteSpace(r.Text));
        });
    }
}
