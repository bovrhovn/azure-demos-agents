using MAF.Tools.AgentAsTool.Helpers;
using Xunit;

namespace MAF.Tests;

public class AgentAsToolHelperTests
{
    [Fact]
    public void CalculateTax_OneMonth_ReturnsBaseTax()
    {
        var result = AgentAsToolHelper.CalculateTax("CustomerA", 1);
        Assert.Equal(100f, result);
    }

    [Fact]
    public void CalculateTax_SixMonths_ReturnsCorrectAmount()
    {
        var result = AgentAsToolHelper.CalculateTax("TheCustomer", 6);
        Assert.Equal(600f, result);
    }

    [Fact]
    public void CalculateTax_ZeroMonths_ReturnsZero()
    {
        var result = AgentAsToolHelper.CalculateTax("CustomerA", 0);
        Assert.Equal(0f, result);
    }

    [Fact]
    public void CalculateTax_CustomerNameDoesNotAffectResult()
    {
        var resultA = AgentAsToolHelper.CalculateTax("Alice", 4);
        var resultB = AgentAsToolHelper.CalculateTax("Bob", 4);
        Assert.Equal(resultA, resultB);
    }

    [Theory]
    [InlineData("Corp Inc.", 12, 1200f)]
    [InlineData("Startup Ltd.", 3, 300f)]
    [InlineData("Enterprise Co.", 24, 2400f)]
    public void CalculateTax_VariousInputs_ReturnsBaseTaxTimesMonths(
        string customer, int months, float expected)
    {
        var result = AgentAsToolHelper.CalculateTax(customer, months);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void CalculateTax_ResultMatchesExpectedFormula()
    {
        const int baseTax = 100;
        const int months = 7;
        var result = AgentAsToolHelper.CalculateTax("AnyCustomer", months);
        Assert.Equal(baseTax * months, result);
    }
}
