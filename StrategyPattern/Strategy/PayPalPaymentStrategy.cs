namespace StrategyPattern.Strategy;

public class PayPalPaymentStrategy : IPayment
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"Paid {amount:C} using PayPal.");
    }
}
