using ChainOfResponsibilityPattern.Naive.Logger;
using ChainOfResponsibilityPattern.Naive.Models;
using ChainOfResponsibilityPattern.Naive.Response;
using ChainOfResponsibilityPattern.Naive.Result;
using ChainOfResponsibilityPattern.Naive.Services;

namespace ChainOfResponsibilityPattern.Naive;

public class Order
{
    private readonly ILogger _logger;
    private readonly IInventoryService _inventoryService;
    private readonly IPaymentService _paymentService;
    private readonly IShippingService _shippingService;

    public Order(ILogger logger, IInventoryService inventoryService, 
        IPaymentService paymentService, IShippingService shippingService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _inventoryService = inventoryService ?? throw new ArgumentNullException(nameof(inventoryService));
        _paymentService = paymentService ?? throw new ArgumentNullException(nameof(paymentService));
        _shippingService = shippingService ?? throw new ArgumentNullException(nameof(shippingService));
    }

    public PurchaseResponse Purchase(Customer? customer, Product? product, int quantity = 1)
    {
        if (customer == null)
            return CreateErrorResponse(PurchaseResult.InvalidInput, "Customer cannot be null", 0, 0);
        
        if (product == null)
            return CreateErrorResponse(PurchaseResult.InvalidInput, "Product cannot be null", 0, 0);
        
        if (quantity <= 0)
            return CreateErrorResponse(PurchaseResult.InvalidInput, "Quantity must be greater than zero", 
                                     customer.Balance, product.Stock);

        _logger.LogInfo($"Starting purchase for customer {customer.Name}, product {product.Name}, quantity {quantity}");

        var totalPrice = product.Price * quantity;

        if (product.Stock < quantity)
        {
            var message = $"Purchase failed: Insufficient stock. Required: {quantity}, Available: {product.Stock}";
            _logger.LogError(message);
            return CreateErrorResponse(PurchaseResult.OutOfStock, message, customer.Balance, product.Stock);
        }

        if (customer.Balance < totalPrice)
        {
            var message = $"Purchase failed: Insufficient funds. Required: {totalPrice:C}, Available: {customer.Balance:C}";
            _logger.LogError(message);
            return CreateErrorResponse(PurchaseResult.InsufficientFunds, message, customer.Balance, product.Stock);
        }

        if (!_inventoryService.ReserveProduct(product, quantity))
        {
            const string message = "Purchase failed: Unable to reserve product";
            _logger.LogError(message);
            return CreateErrorResponse(PurchaseResult.OutOfStock, message, customer.Balance, product.Stock);
        }

        _logger.LogInfo($"Product reserved: {quantity} units of {product.Name}");

        try
        {
            if (!_paymentService.ProcessPayment(customer, totalPrice))
            {
                _inventoryService.ReleaseProduct(product, quantity);
                const string message = "Purchase failed: Payment processing failed";
                _logger.LogError(message);
                return CreateErrorResponse(PurchaseResult.InsufficientFunds, message, customer.Balance, product.Stock);
            }

            _logger.LogInfo($"Payment processed: {totalPrice:C} from customer {customer.Name}");

            _shippingService.ShipProduct(customer, product);

            var successMessage = $"Purchase completed successfully! Customer: {customer.Name}, Product: {product.Name}, Quantity: {quantity}, Total: {totalPrice:C}";
            _logger.LogInfo(successMessage);
            
            return new PurchaseResponse
            {
                Result = PurchaseResult.Success,
                Message = successMessage,
                RemainingBalance = customer.Balance,
                RemainingStock = product.Stock,
                ProcessedAt = DateTime.Now
            };
        }
        catch (Exception ex)
        {
            _paymentService.RefundPayment(customer, totalPrice);
            _inventoryService.ReleaseProduct(product, quantity);
            
            var errorMessage = $"Purchase failed due to unexpected error: {ex.Message}";
            _logger.LogError(errorMessage);
            return CreateErrorResponse(PurchaseResult.InvalidInput, errorMessage, customer.Balance, product.Stock);
        }
    }

    private static PurchaseResponse CreateErrorResponse(PurchaseResult result, string message, decimal balance, int stock)
    {
        return new PurchaseResponse
        {
            Result = result,
            Message = message,
            RemainingBalance = balance,
            RemainingStock = stock
        };
    }
}