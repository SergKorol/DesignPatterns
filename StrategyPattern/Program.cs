using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.Extensions.DependencyInjection;
using StrategyPattern.NonStrategy;
using StrategyPattern.Strategy;

namespace StrategyPattern;

public class Program
{
    public static void Main()
    {
        BenchmarkRunner.Run<Program>(new BenchmarkConfig());
    }

    [Benchmark]
    public void RunStrategy()
    {
        var services = new ServiceCollection();
        services.RegisterStrategyServices();
        var serviceProvider = services.BuildServiceProvider();
        var strategy = new Strategy.Strategy(serviceProvider.GetService<IPaymentContext>(), serviceProvider.GetServices<IPayment>());
        strategy.Run();
    }

    [Benchmark]
    public void RunNonStrategy()
    {
        var services = new ServiceCollection();
        services.RegisterNonStrategyServices();
        var serviceProvider = services.BuildServiceProvider();
        var nonStrategy = new NonStrategy.NonStrategy(serviceProvider.GetService<ICreditCardPayment>(), serviceProvider.GetService<IPayPalPayment>(), serviceProvider.GetService<ICryptoCurrencyPayment>());
        nonStrategy.Run();
    }
}