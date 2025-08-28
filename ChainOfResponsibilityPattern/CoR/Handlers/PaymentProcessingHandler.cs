using ChainOfResponsibilityPattern.CoR.Context;
using ChainOfResponsibilityPattern.CoR.Handlers.Base;
using ChainOfResponsibilityPattern.CoR.Result;

namespace ChainOfResponsibilityPattern.CoR.Handlers;

public class PaymentProcessingHandler : PurchaseHandler
{
    protected override void ProcessRequest(PurchaseContext context)
    {

        if (context.Customer != null && context.Customer.Balance >= context.TotalPrice)
        {
            context.Customer.Balance -= context.TotalPrice;
            context.PaymentProcessed = true;
        }
        else
        {
            context.Result = PurchaseResult.PaymentFailed;
            context.ErrorMessage = "Payment processing failed";
            context.IsProcessed = true;
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