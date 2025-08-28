using ChainOfResponsibilityPattern.Naive.Logger;
using ChainOfResponsibilityPattern.Naive.Models;

namespace ChainOfResponsibilityPattern.Naive.Services;

public interface IShippingService
{
    void ShipProduct(Customer customer, Product product);
}

public class ShippingService(ILogger logger) : IShippingService
{
    private readonly ILogger _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public void ShipProduct(Customer customer, Product product)
    {
        _logger.LogInfo($"Product '{product.Name}' shipped to customer '{customer.Name}'");
    }
}