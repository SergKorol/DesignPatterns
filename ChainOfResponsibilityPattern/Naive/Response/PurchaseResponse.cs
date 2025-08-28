using ChainOfResponsibilityPattern.Naive.Result;

namespace ChainOfResponsibilityPattern.Naive.Response;

public class PurchaseResponse
{
    public PurchaseResult Result { get; init; }
    public string? Message { get; init; }
    public decimal RemainingBalance { get; init; }
    public int RemainingStock { get; init; }
    public DateTime ProcessedAt { get; init; }

    public bool IsSuccessful => Result == PurchaseResult.Success;
}