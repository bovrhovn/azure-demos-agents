namespace MAF.Tools.AgentAsTool.Helpers;

/// <summary>
/// Business logic helper used by the AgentAsTool sample.
/// Exposes the tax calculation as a public method so it can be tested
/// independently of the Azure SDK wiring in Program.cs.
/// </summary>
public static class AgentAsToolHelper
{
    /// <summary>
    /// Calculates a tax amount for a customer over a given number of months.
    /// The base tax rate is 100 units per month.
    /// </summary>
    /// <param name="customerName">Name of the customer (informational only).</param>
    /// <param name="months">Number of months to calculate tax for.</param>
    /// <returns>Total tax amount (base rate × months).</returns>
    public static float CalculateTax(string customerName, int months)
    {
        var baseTax = 100;
        return baseTax * months;
    }
}
