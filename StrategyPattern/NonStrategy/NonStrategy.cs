using System.Globalization;

namespace StrategyPattern.NonStrategy;

public class NonStrategy(ICreditCardPayment? creditCardPayment, IPayPalPayment? payPalPayment, ICryptoCurrencyPayment? cryptoCurrencyPayment)
{
    public void Run()
    {
        Console.WriteLine("Choose payment method: 1. CreditCard, 2. PayPal, 3. Crypto");
        var choice = new Random().Next(1, 3);
        Console.WriteLine($"Chosen payment method: {choice}");
        Console.WriteLine("Enter payment amount:");
        var input = 1 + new Random().NextDouble() * (1000 - 1);
        var amount = decimal.Parse(input.ToString(CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
        Console.WriteLine($"Entered amount: {amount}");
        switch (choice)
        {
            case 1:
                creditCardPayment?.CreditCardPay(amount);
                break;
            case 2:
                payPalPayment?.PayPalPay(amount);
                break;
            case 3:
                cryptoCurrencyPayment?.CryptoCurrencyPay(amount);
                break;
            default:
                Console.WriteLine("Invalid choice.");
                return;
        }
    }
}