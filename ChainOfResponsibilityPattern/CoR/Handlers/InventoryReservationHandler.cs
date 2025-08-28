using ChainOfResponsibilityPattern.CoR.Context;
using ChainOfResponsibilityPattern.CoR.Handlers.Base;
using ChainOfResponsibilityPattern.CoR.Logger;

namespace ChainOfResponsibilityPattern.CoR.Handlers;

public class InventoryReservationHandler(ILogger logger) : PurchaseHandler(logger)
{
    protected override void ProcessRequest(PurchaseContext context)
    {
        Logger.LogInfo("Reserving inventory...");

        // Reserve the product
        if (context.Product == null) return;
        context.Product.Stock -= context.Quantity;
        context.ProductReserved = true;

        Logger.LogInfo($"Product reserved successfully. Remaining stock: {context.Product.Stock}");
    }

    protected override void Rollback(PurchaseContext context)
    {
        if (!context.ProductReserved) return;
        Logger.LogInfo("Rolling back inventory reservation...");
        if (context.Product != null) context.Product.Stock += context.Quantity;
        context.ProductReserved = false;
    }
}