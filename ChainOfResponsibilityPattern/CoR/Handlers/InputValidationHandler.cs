using ChainOfResponsibilityPattern.CoR.Context;
using ChainOfResponsibilityPattern.CoR.Handlers.Base;
using ChainOfResponsibilityPattern.CoR.Logger;
using ChainOfResponsibilityPattern.CoR.Result;

namespace ChainOfResponsibilityPattern.CoR.Handlers;

public class InputValidationHandler : PurchaseHandler
{
    public InputValidationHandler(ILogger logger) : base(logger) { }

    protected override void ProcessRequest(PurchaseContext context)
    {
        Logger.LogInfo("Validating input parameters...");

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

        // Calculate total price
        context.TotalPrice = context.Product.Price * context.Quantity;

        Logger.LogInfo($"Input validation passed. Customer: {context.Customer.Name}, " +
                        $"Product: {context.Product.Name}, Quantity: {context.Quantity}, " +
                        $"Total Price: {context.TotalPrice:C}");
    }
}