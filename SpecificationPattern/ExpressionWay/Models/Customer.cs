namespace SpecificationPattern.ExpressionWay.Models;

public record Customer(int Id, string Name, bool IsActive, decimal TotalPurchases, IEnumerable<Payment>? Payments)
{
    public string Name { get; init; } = Name ?? throw new ArgumentNullException(nameof(Name));
    public bool IsActive { get; set; } = IsActive;

    public override string ToString() =>
        $"ID: {Id}, Name: {Name}, Active: {IsActive}, Purchases: {TotalPurchases:C}, Payments: {(Payments != null ? string.Join(", ", Payments.Select(p => $"{p.Method}({(p.IsActive ? "Active" : "Inactive")})")) : "None")}";
}