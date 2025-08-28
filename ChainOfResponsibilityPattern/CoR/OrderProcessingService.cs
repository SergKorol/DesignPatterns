using ChainOfResponsibilityPattern.CoR.Context;
using ChainOfResponsibilityPattern.CoR.Handlers;
using ChainOfResponsibilityPattern.CoR.Handlers.Base;
using ChainOfResponsibilityPattern.CoR.Logger;
using ChainOfResponsibilityPattern.CoR.Models;
using ChainOfResponsibilityPattern.CoR.Response;
using ChainOfResponsibilityPattern.CoR.Result;

namespace ChainOfResponsibilityPattern.CoR;

public class OrderProcessingService
{
    private readonly PurchaseHandler _handlerChain;
    private readonly ILogger _logger;

    public OrderProcessingService(ILogger logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        
        var inputValidator = new InputValidationHandler(_logger);
        var stockValidator = new StockValidationHandler(_logger);
        var balanceValidator = new BalanceValidationHandler(_logger);
        var inventoryReservation = new InventoryReservationHandler(_logger);
        var paymentProcessor = new PaymentProcessingHandler(_logger);
        var shippingHandler = new ShippingHandler(_logger);

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
        _logger.LogInfo($"Starting order processing for customer: {customer.Name}, product: {product.Name}");

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

        _logger.LogInfo($"Order processing completed with result: {processedContext.Result}");
        return response;
    }
}