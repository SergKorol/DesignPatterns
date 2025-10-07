namespace TemplateMethodPattern.Template;

public abstract class PaymentProcessor
{
    public async Task ProcessPaymentAsync(decimal amount)
    {
        await ValidatePaymentAsync();
        await AuthorizePaymentAsync(amount);
        await ExecutePaymentAsync(amount);
        
        //extended
        await AfterPaymentExecutedAsync(amount);
        
        await SendReceiptAsync();
    }
    
    protected abstract Task ValidatePaymentAsync();
    protected abstract Task AuthorizePaymentAsync(decimal amount);
    protected abstract Task ExecutePaymentAsync(decimal amount);
    
    //extended
    protected virtual Task AfterPaymentExecutedAsync(decimal amount)
    {
        return Task.CompletedTask;
    }
    protected virtual Task SendReceiptAsync()
    {
        Console.WriteLine("📧 Sending receipt via email...");
        return Task.CompletedTask;
    }
}