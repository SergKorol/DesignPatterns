namespace StrategyPattern.NonStrategy;

public class CreditCardPayment : ICreditCardPayment
{
    public void CreditCardPay(decimal amount)
    {
        Console.WriteLine($"Paid {amount:C} using Credit Card.");
    }
}
