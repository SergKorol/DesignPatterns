using ChainOfResponsibilityPattern.CoR.Context;
using ChainOfResponsibilityPattern.CoR.Handlers.Base;
using ChainOfResponsibilityPattern.CoR.Result;

namespace ChainOfResponsibilityPattern.CoR.Handlers;

public class BalanceValidationHandler : PurchaseHandler
{
    protected override void ProcessRequest(PurchaseContext context)
    {
        if (context.Customer == null || context.Customer.Balance >= context.TotalPrice) return;
        context.Result = PurchaseResult.InsufficientFunds;
        context.ErrorMessage =
            $"Insufficient funds. Required: {context.TotalPrice:C}, Available: {context.Customer.Balance:C}";
        context.IsProcessed = true;
    }
}