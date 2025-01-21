namespace StrategyPattern.NonStrategy;

public class PayPalPayment : IPayPalPayment
{
    public void PayPalPay(decimal amount)
    {
        Console.WriteLine($"Paid {amount:C} using PayPal.");
    }
}
