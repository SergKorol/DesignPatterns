using ChainOfResponsibilityPattern.CoR.Context;
using ChainOfResponsibilityPattern.CoR.Handlers.Base;
using ChainOfResponsibilityPattern.CoR.Result;

namespace ChainOfResponsibilityPattern.CoR.Handlers;

public class ShippingHandler : PurchaseHandler
{
    protected override void ProcessRequest(PurchaseContext context)
    {
        try
        {
            context.Result = PurchaseResult.Success;
            context.ProcessedAt = DateTime.Now;
            context.IsProcessed = true;
        }
        catch (Exception ex)
        {
            context.Result = PurchaseResult.ShippingFailed;
            context.ErrorMessage = $"Shipping failed: {ex.Message}";
            context.IsProcessed = true;
            throw;
        }
    }

    protected override void Rollback(PurchaseContext context)
    {
        if (context.PaymentProcessed)
        {
            if (context.Customer != null) context.Customer.Balance += context.TotalPrice;
            context.PaymentProcessed = false;
        }

        if (!context.ProductReserved) return;
        if (context.Product != null) context.Product.Stock += context.Quantity;
        context.ProductReserved = false;
    }
}