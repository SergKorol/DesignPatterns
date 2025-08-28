using ChainOfResponsibilityPattern.CoR.Context;
using ChainOfResponsibilityPattern.CoR.Handlers.Base;
using ChainOfResponsibilityPattern.CoR.Logger;
using ChainOfResponsibilityPattern.CoR.Result;

namespace ChainOfResponsibilityPattern.CoR.Handlers;

public class BalanceValidationHandler(ILogger logger) : PurchaseHandler(logger)
{
    protected override void ProcessRequest(PurchaseContext context)
    {
        Logger.LogInfo("Checking customer balance...");

        if (context.Customer != null && context.Customer.Balance < context.TotalPrice)
        {
            context.Result = PurchaseResult.InsufficientFunds;
            context.ErrorMessage = $"Insufficient funds. Required: {context.TotalPrice:C}, Available: {context.Customer.Balance:C}";
            context.IsProcessed = true;
            Logger.LogError(context.ErrorMessage);
            return;
        }

        Logger.LogInfo($"Balance validation passed. Customer balance: {context.Customer?.Balance:C}, Required: {context.TotalPrice:C}");
    }
}