using ChainOfResponsibilityPattern.CoR.Context;
using ChainOfResponsibilityPattern.CoR.Handlers.Base;
using ChainOfResponsibilityPattern.CoR.Logger;
using ChainOfResponsibilityPattern.CoR.Result;

namespace ChainOfResponsibilityPattern.CoR.Handlers;

public class StockValidationHandler(ILogger logger) : PurchaseHandler(logger)
{
    protected override void ProcessRequest(PurchaseContext context)
    {
        Logger.LogInfo("Checking stock availability...");

        if (context.Product != null && context.Product.Stock < context.Quantity)
        {
            context.Result = PurchaseResult.OutOfStock;
            context.ErrorMessage = $"Insufficient stock. Required: {context.Quantity}, Available: {context.Product.Stock}";
            context.IsProcessed = true;
            Logger.LogError(context.ErrorMessage);
            return;
        }

        Logger.LogInfo($"Stock validation passed. Available: {context.Product?.Stock}, Required: {context.Quantity}");
    }
}