namespace TemplateMethodPattern.Template;

public class PayPalPaymentProcessor : PaymentProcessor
{
    protected override Task ValidatePaymentAsync()
    {
        Console.WriteLine("✅ Checking PayPal-account...");
        return Task.CompletedTask;
    }
    
    protected override Task AuthorizePaymentAsync(decimal amount)
    {
        Console.WriteLine($"🔐 Authorizing PayPal to {amount:C}...");
        return Task.CompletedTask;
    }
    
    protected override Task ExecutePaymentAsync(decimal amount)
    {
        Console.WriteLine($"💰 Executing PayPal payment to {amount:C}...");
        return Task.CompletedTask;
    }
}