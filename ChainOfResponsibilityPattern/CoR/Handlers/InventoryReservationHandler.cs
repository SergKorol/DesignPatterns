using ChainOfResponsibilityPattern.CoR.Context;
using ChainOfResponsibilityPattern.CoR.Handlers.Base;

namespace ChainOfResponsibilityPattern.CoR.Handlers;

public class InventoryReservationHandler : PurchaseHandler
{
    protected override void ProcessRequest(PurchaseContext context)
    {
        if (context.Product == null) return;
        context.Product.Stock -= context.Quantity;
        context.ProductReserved = true;
    }

    protected override void Rollback(PurchaseContext context)
    {
        if (!context.ProductReserved) return;
        if (context.Product != null) context.Product.Stock += context.Quantity;
        context.ProductReserved = false;
    }
}