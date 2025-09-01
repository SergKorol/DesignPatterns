using ChainOfResponsibilityPattern.Naive.Models;
using ChainOfResponsibilityPattern.Naive.Response;
using ChainOfResponsibilityPattern.Naive.Result;
using ChainOfResponsibilityPattern.Naive.Services;

namespace ChainOfResponsibilityPattern.Naive;

public class Order(
    IInventoryService inventoryService,
    IPaymentService paymentService,
    IShippingService shippingService)
{
    private readonly IInventoryService _inventoryService = inventoryService ?? throw new ArgumentNullException(nameof(inventoryService));
    private readonly IPaymentService _paymentService = paymentService ?? throw new ArgumentNullException(nameof(paymentService));
    private readonly IShippingService _shippingService = shippingService ?? throw new ArgumentNullException(nameof(shippingService));

    public PurchaseResponse Purchase(Customer? customer, Product? product, int quantity = 1)
    {
        if (customer == null)
            return CreateErrorResponse(PurchaseResult.InvalidInput, "Customer cannot be null", 0, 0);

        if (product == null)
            return CreateErrorResponse(PurchaseResult.InvalidInput, "Product cannot be null", 0, 0);

        if (quantity <= 0)
            return CreateErrorResponse(PurchaseResult.InvalidInput, "Quantity must be greater than zero",
                customer.Balance, product.Stock);


        var totalPrice = product.Price * quantity;

        if (product.Stock < quantity)
        {
            var message = $"Purchase failed: Insufficient stock. Required: {quantity}, Available: {product.Stock}";
            return CreateErrorResponse(PurchaseResult.OutOfStock, message, customer.Balance, product.Stock);
        }

        if (customer.Balance < totalPrice)
        {
            var message =
                $"Purchase failed: Insufficient funds. Required: {totalPrice:C}, Available: {customer.Balance:C}";
            return CreateErrorResponse(PurchaseResult.InsufficientFunds, message, customer.Balance, product.Stock);
        }

        if (!_inventoryService.ReserveProduct(product, quantity))
        {
            const string message = "Purchase failed: Unable to reserve product";
            return CreateErrorResponse(PurchaseResult.OutOfStock, message, customer.Balance, product.Stock);
        }


        try
        {
            if (!_paymentService.ProcessPayment(customer, totalPrice))
            {
                _inventoryService.ReleaseProduct(product, quantity);
                const string message = "Purchase failed: Payment processing failed";
                return CreateErrorResponse(PurchaseResult.PaymentFailed, message, customer.Balance, product.Stock);
            }

            if (!_shippingService.ShipProduct(customer, product))
            {
                _inventoryService.ReleaseProduct(product, quantity);
                _paymentService.RefundPayment(customer, totalPrice);
                const string message = "Purchase failed: Shipment processing failed";
                return CreateErrorResponse(PurchaseResult.ShippingFailed, message, customer.Balance, product.Stock);
            }

            var successMessage =
                $"Purchase completed successfully! Customer: {customer.Name}, Product: {product.Name}, Quantity: {quantity}, Total: {totalPrice:C}";

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
            return CreateErrorResponse(PurchaseResult.InvalidInput, errorMessage, customer.Balance, product.Stock);
        }
    }

    private static PurchaseResponse CreateErrorResponse(PurchaseResult result, string message, decimal balance,
        int stock)
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