using ChainOfResponsibilityPattern.Naive.Models;

namespace ChainOfResponsibilityPattern.Naive.Services;

public interface IShippingService
{
    bool ShipProduct(Customer customer, Product product);
}

public class ShippingService() : IShippingService
{
    public bool ShipProduct(Customer customer, Product product)
    {
        try
        {
            Console.WriteLine($"Product '{product.Id}' shipped to customer '{customer.Id}'");
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}