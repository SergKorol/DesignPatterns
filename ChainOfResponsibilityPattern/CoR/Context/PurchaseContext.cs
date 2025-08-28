using ChainOfResponsibilityPattern.CoR.Models;
using ChainOfResponsibilityPattern.CoR.Result;

namespace ChainOfResponsibilityPattern.CoR.Context;

public class PurchaseContext
{
    public Customer? Customer { get; init; }
    public Product? Product { get; init; }
    public int Quantity { get; init; }
    public decimal TotalPrice { get; set; }
    public bool IsProcessed { get; set; }
    public PurchaseResult Result { get; set; }
    public string? ErrorMessage { get; set; }
    public DateTime ProcessedAt { get; set; }
    public bool ProductReserved { get; set; }
    public bool PaymentProcessed { get; set; }
}