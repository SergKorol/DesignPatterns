using Microsoft.Extensions.DependencyInjection;
using StrategyPattern.NonStrategy;
using StrategyPattern.Strategy;

namespace StrategyPattern;

public static class RegisterServices
{
    public static void RegisterStrategyServices(this ServiceCollection services)
    {
        services.AddSingleton<IPayment, CreditCardPaymentStrategy>();
        services.AddSingleton<IPayment, PayPalPaymentStrategy>();
        services.AddSingleton<IPayment, CryptoCurrencyPaymentStrategy>();
        services.AddSingleton<IPaymentContext, PaymentContext>();
    }
    
    public static void RegisterNonStrategyServices(this ServiceCollection services)
    {
        services.AddSingleton<ICreditCardPayment, CreditCardPayment>();
        services.AddSingleton<IPayPalPayment, PayPalPayment>();
        services.AddSingleton<ICryptoCurrencyPayment, CryptoCurrencyPayment>();
    }
}