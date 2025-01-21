namespace StrategyPattern.Strategy;

public class CryptoCurrencyPaymentStrategy : IPayment
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"Paid {amount:C} using Crypto Currency.");
    }
}