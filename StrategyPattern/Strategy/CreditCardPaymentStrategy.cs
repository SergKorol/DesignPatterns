namespace StrategyPattern.Strategy;

public class CreditCardPaymentStrategy : IPayment
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"Paid {amount:C} using Credit Card.");
    }
}
