using ChainOfResponsibilityPattern.Naive.Models;

namespace ChainOfResponsibilityPattern.Naive.Services;

public interface IInventoryService
{
    bool ReserveProduct(Product product, int quantity = 1);
    void ReleaseProduct(Product product, int quantity = 1);
}

public class InventoryService : IInventoryService
{
    public bool ReserveProduct(Product product, int quantity = 1)
    {
        if (product.Stock < quantity) return false;
        product.Stock -= quantity;
        return true;
    }

    public void ReleaseProduct(Product product, int quantity = 1)
    {
        product.Stock += quantity;
    }
}