using ChainOfResponsibilityPattern.CoR.Context;
using ChainOfResponsibilityPattern.CoR.Handlers;
using ChainOfResponsibilityPattern.CoR.Handlers.Base;
using ChainOfResponsibilityPattern.CoR.Models;
using ChainOfResponsibilityPattern.CoR.Response;
using ChainOfResponsibilityPattern.CoR.Result;

namespace ChainOfResponsibilityPattern.CoR;

public class OrderProcessingService
{
    private readonly PurchaseHandler _handlerChain;

    public OrderProcessingService()
    {
        var inputValidator = new InputValidationHandler();
        var stockValidator = new StockValidationHandler();
        var balanceValidator = new BalanceValidationHandler();
        var inventoryReservation = new InventoryReservationHandler();
        var paymentProcessor = new PaymentProcessingHandler();
        var shippingHandler = new ShippingHandler();

        inputValidator
            .SetNext(stockValidator)
            .SetNext(balanceValidator)
            .SetNext(inventoryReservation)
            .SetNext(paymentProcessor)
            .SetNext(shippingHandler);

        _handlerChain = inputValidator;
    }

    public PurchaseResponse ProcessOrder(Customer customer, Product product, int quantity = 1)
    {
        var context = new PurchaseContext
        {
            Customer = customer,
            Product = product,
            Quantity = quantity
        };

        var processedContext = _handlerChain.Handle(context);

        var response = new PurchaseResponse
        {
            Result = processedContext.Result,
            Message = processedContext.Result == PurchaseResult.Success 
                ? "Purchase completed successfully!" 
                : processedContext.ErrorMessage,
            RemainingBalance = customer.Balance,
            RemainingStock = product.Stock,
            ProcessedAt = processedContext.ProcessedAt
        };

        return response;
    }
}