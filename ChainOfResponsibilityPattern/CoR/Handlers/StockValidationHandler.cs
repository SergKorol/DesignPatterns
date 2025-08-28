using ChainOfResponsibilityPattern.CoR.Context;
using ChainOfResponsibilityPattern.CoR.Handlers.Base;
using ChainOfResponsibilityPattern.CoR.Result;

namespace ChainOfResponsibilityPattern.CoR.Handlers;

public class StockValidationHandler : PurchaseHandler
{
    protected override void ProcessRequest(PurchaseContext context)
    {
        if (context.Product == null || context.Product.Stock >= context.Quantity) return;
        context.Result = PurchaseResult.OutOfStock;
        context.ErrorMessage = $"Insufficient stock. Required: {context.Quantity}, Available: {context.Product.Stock}";
        context.IsProcessed = true;
    }
}