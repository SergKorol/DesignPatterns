namespace TemplateMethodPattern.Naive;

public class PayPalService
{
    public async Task ProcessPaymentAsync(decimal amount)
    {
        await ValidatePaymentAsync();
        await AuthorizePaymentAsync(amount);
        await ExecutePaymentAsync(amount);
        await SendReceiptAsync();
        await Task.CompletedTask;
    }

    private Task ValidatePaymentAsync()
    {
        Console.WriteLine("✅ Checking PayPal-account...");
        return Task.CompletedTask;
    }

    private Task AuthorizePaymentAsync(decimal amount)
    {
        Console.WriteLine($"🔐 Authorizing PayPal to {amount:C}...");
        return Task.CompletedTask;
    }

    private Task ExecutePaymentAsync(decimal amount)
    {
        Console.WriteLine($"💰 Executing PayPal payment to {amount:C}...");
        return Task.CompletedTask;
    }

    private Task SendReceiptAsync()
    {
        Console.WriteLine("📧 Sending receipt via email...");
        return Task.CompletedTask;
    }
}