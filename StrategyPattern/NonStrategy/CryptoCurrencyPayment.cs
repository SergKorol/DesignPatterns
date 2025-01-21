namespace StrategyPattern.NonStrategy;

public class CryptoCurrencyPayment : ICryptoCurrencyPayment
{
    public void CryptoCurrencyPay(decimal amount)
    {
        Console.WriteLine($"Paid {amount:C} using Crypto Currency.");
    }
}