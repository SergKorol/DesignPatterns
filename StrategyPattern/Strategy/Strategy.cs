using System.Globalization;

namespace StrategyPattern.Strategy;

public class Strategy(IPaymentContext? paymentContext, IEnumerable<IPayment>? payments)
{
    public void Run()
    {
        Console.WriteLine("Choose payment method: 1. CreditCard, 2. PayPal, 3. Crypto");
        int choice = new Random().Next(1, 3);
        Console.WriteLine($"Chosen payment method: {choice}");
        IPayment? payment = choice switch
                {
                    1 => payments?.OfType<CreditCardPaymentStrategy>().FirstOrDefault(),
                    2 => payments?.OfType<PayPalPaymentStrategy>().FirstOrDefault(),
                    3 => payments?.OfType<CryptoCurrencyPaymentStrategy>().FirstOrDefault(),
                    _ => null
                };
        
                if (payment == null)
                {
                    Console.WriteLine("Invalid choice or strategy not found.");
                    return;
                }
        
                paymentContext?.SetPaymentStrategy(payment);

        Console.WriteLine("Enter payment amount:");
        var input = 1 + (new Random().NextDouble() * (1000 - 1));
        var amount = decimal.Parse(input.ToString(CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
        Console.WriteLine($"Entered amount: {amount}");
        paymentContext?.ExecutePayment(amount);
    }
}