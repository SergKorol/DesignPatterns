using ChainOfResponsibilityPattern.CoR.Context;
using ChainOfResponsibilityPattern.CoR.Handlers.Base;
using ChainOfResponsibilityPattern.CoR.Logger;
using ChainOfResponsibilityPattern.CoR.Result;

namespace ChainOfResponsibilityPattern.CoR.Handlers;

public class ShippingHandler(ILogger logger) : PurchaseHandler(logger)
{
    protected override void ProcessRequest(PurchaseContext context)
    {
        Logger.LogInfo("Initiating shipping...");

        try
        {
            Logger.LogInfo($"Product '{context.Product?.Name}' shipped to customer '{context.Customer?.Name}'");

            context.Result = PurchaseResult.Success;
            context.ProcessedAt = DateTime.Now;
            context.IsProcessed = true;

            Logger.LogInfo("Purchase completed successfully!");
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
        Logger.LogInfo("Rolling back due to shipping failure...");
        
        if (context.PaymentProcessed)
        {
            Logger.LogInfo("Rolling back payment...");
            if (context.Customer != null) context.Customer.Balance += context.TotalPrice;
            context.PaymentProcessed = false;
        }

        if (!context.ProductReserved) return;
        Logger.LogInfo("Rolling back inventory reservation...");
        if (context.Product != null) context.Product.Stock += context.Quantity;
        context.ProductReserved = false;
    }
}