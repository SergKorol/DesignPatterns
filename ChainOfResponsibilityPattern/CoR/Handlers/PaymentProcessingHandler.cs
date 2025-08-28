using ChainOfResponsibilityPattern.CoR.Context;
using ChainOfResponsibilityPattern.CoR.Handlers.Base;
using ChainOfResponsibilityPattern.CoR.Logger;
using ChainOfResponsibilityPattern.CoR.Result;

namespace ChainOfResponsibilityPattern.CoR.Handlers;

public class PaymentProcessingHandler(ILogger logger) : PurchaseHandler(logger)
{
    protected override void ProcessRequest(PurchaseContext context)
    {
        Logger.LogInfo("Processing payment...");

        if (context.Customer != null && context.Customer.Balance >= context.TotalPrice)
        {
            context.Customer.Balance -= context.TotalPrice;
            context.PaymentProcessed = true;
            Logger.LogInfo($"Payment processed successfully. Amount: {context.TotalPrice:C}, Remaining balance: {context.Customer.Balance:C}");
        }
        else
        {
            context.Result = PurchaseResult.PaymentFailed;
            context.ErrorMessage = "Payment processing failed";
            context.IsProcessed = true;
            Logger.LogError(context.ErrorMessage);
        }
    }

    protected override void Rollback(PurchaseContext context)
    {
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