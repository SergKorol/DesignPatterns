using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Settings;
using TemplateMethodPattern.Naive;
using TemplateMethodPattern.Template;

namespace TemplateMethodPattern;

public class Program
{
    public static async Task Main()
    {
        // await NaivePaymentMethod();
        // await TemplatePaymentMethod();
        BenchmarkRunner.Run<Program>(new BenchmarkConfig());
    }

    [Benchmark]
    public async Task TemplatePaymentMethod()
    {
        var paypal = new PayPalPaymentProcessor();
        var stripe = new StripePaymentProcessor();

        Console.WriteLine("=== PayPal ===");
        await paypal.ProcessPaymentAsync(50m);

        Console.WriteLine("\n=== Stripe ===");
        await stripe.ProcessPaymentAsync(75m);
    }

    [Benchmark]
    public async Task NaivePaymentMethod()
    {
        var paypal = new PayPalService();
        var stripe = new StripeService();

        Console.WriteLine("=== PayPal ===");
        await paypal.ProcessPaymentAsync(50m);

        Console.WriteLine("\n=== Stripe ===");
        await stripe.ProcessPaymentAsync(75m);
    }
}