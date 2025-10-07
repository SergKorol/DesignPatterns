namespace TemplateMethodPattern.Template;

public class StripePaymentProcessor : PaymentProcessor
{
    protected override Task ValidatePaymentAsync()
    {
        Console.WriteLine("✅ Checking Stripe-account...");
        return Task.CompletedTask;
    }
    
    protected override Task AuthorizePaymentAsync(decimal amount)
    {
        Console.WriteLine($"🔐 Authorizing Stripe to {amount:C}...");
        return Task.CompletedTask;
    }
    
    //extended
    protected override Task AfterPaymentExecutedAsync(decimal amount)
    {
        var cashback = Math.Round(amount * 0.05m, 2);
        Console.WriteLine($"🎁 Cashback is credited {cashback:C} to your Stripe-account!");
        return Task.CompletedTask;
    }
    
    protected override Task ExecutePaymentAsync(decimal amount)
    {
        Console.WriteLine($"💰 Executing Stripe payment to {amount:C}...");
        return Task.CompletedTask;
    }
}