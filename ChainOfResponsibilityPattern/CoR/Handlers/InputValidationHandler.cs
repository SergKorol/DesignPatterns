using ChainOfResponsibilityPattern.CoR.Context;
using ChainOfResponsibilityPattern.CoR.Handlers.Base;
using ChainOfResponsibilityPattern.CoR.Result;

namespace ChainOfResponsibilityPattern.CoR.Handlers;

public class InputValidationHandler : PurchaseHandler
{
    protected override void ProcessRequest(PurchaseContext context)
    {
        if (context.Customer == null)
        {
            context.Result = PurchaseResult.InvalidInput;
            context.ErrorMessage = "Customer cannot be null";
            context.IsProcessed = true;
            return;
        }

        if (context.Product == null)
        {
            context.Result = PurchaseResult.InvalidInput;
            context.ErrorMessage = "Product cannot be null";
            context.IsProcessed = true;
            return;
        }

        if (context.Quantity <= 0)
        {
            context.Result = PurchaseResult.InvalidInput;
            context.ErrorMessage = "Quantity must be greater than zero";
            context.IsProcessed = true;
            return;
        }

        context.TotalPrice = context.Product.Price * context.Quantity;
    }
}