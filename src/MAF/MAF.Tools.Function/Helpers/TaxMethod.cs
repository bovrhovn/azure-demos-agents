namespace MAF.Tools.Function.Helpers;

public static class TaxMethod
{
    public static float CalculateTaxMethod(string customerName, int months)
    {
        // In real world scenario, you would have complex logic here to calculate tax based on customer and months
        var baseTax = 100; // base tax amount
        var tax = baseTax * months; // simple calculation for demonstration
        return tax;
    }
}