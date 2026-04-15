using MAF.Tools.Function.Helpers;
using Xunit;

namespace MAF.Tests;

public class TaxMethodTests
{
    [Fact]
    public void CalculateTaxMethod_OneMonth_ReturnsBaseTax()
    {
        var result = TaxMethod.CalculateTaxMethod("CustomerA", 1);
        Assert.Equal(100f, result);
    }

    [Fact]
    public void CalculateTaxMethod_SixMonths_ReturnsCorrectAmount()
    {
        var result = TaxMethod.CalculateTaxMethod("TheCustomer", 6);
        Assert.Equal(600f, result);
    }

    [Fact]
    public void CalculateTaxMethod_TwelveMonths_ReturnsCorrectAmount()
    {
        var result = TaxMethod.CalculateTaxMethod("AnnualCustomer", 12);
        Assert.Equal(1200f, result);
    }

    [Fact]
    public void CalculateTaxMethod_ZeroMonths_ReturnsZero()
    {
        var result = TaxMethod.CalculateTaxMethod("CustomerA", 0);
        Assert.Equal(0f, result);
    }

    [Theory]
    [InlineData("Alice", 3, 300f)]
    [InlineData("Bob", 9, 900f)]
    [InlineData("Corp Inc.", 24, 2400f)]
    public void CalculateTaxMethod_VariousInputs_ReturnsBaseTaxTimesMonths(
        string customer, int months, float expected)
    {
        var result = TaxMethod.CalculateTaxMethod(customer, months);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void CalculateTaxMethod_CustomerNameDoesNotAffectResult()
    {
        var resultA = TaxMethod.CalculateTaxMethod("CustomerA", 5);
        var resultB = TaxMethod.CalculateTaxMethod("CompletelyDifferentName", 5);
        Assert.Equal(resultA, resultB);
    }
}
