using SpecificationPattern.ExpressionWay.Models;

namespace SpecificationPattern.ExpressionWay.Extensions;

public static class CustomerFilteringExtensions
{
    public static IEnumerable<Customer> FilterActive(this IEnumerable<Customer> customers)
        => customers.Where(c => c.IsActive);

    public static IEnumerable<Customer> FilterInactive(this IEnumerable<Customer> customers)
        => customers.Where(c => !c.IsActive);

    public static IEnumerable<Customer> FilterPremium(this IEnumerable<Customer> customers,
        decimal minimumPurchaseAmount)
        => customers.Where(c => c.TotalPurchases >= minimumPurchaseAmount);

    public static IEnumerable<Customer> FilterActiveOrPremium(this IEnumerable<Customer> customers, decimal minAmount)
        => customers.Where(c => c.IsActive || c.TotalPurchases >= minAmount);

    public static IEnumerable<Customer> FilterInactiveOrPremium(this IEnumerable<Customer> customers, decimal minAmount)
        => customers.Where(c => !c.IsActive || c.TotalPurchases >= minAmount);

    public static bool CheckActivePremiumCustomer(this IEnumerable<Customer> customers, string name, decimal minAmount)
    {
        Customer? customer = customers.FirstOrDefault(x => x.Name == name);

        return customer is { IsActive: true } && customer.TotalPurchases >= minAmount;
    }

    public static IEnumerable<Customer> CheckActiveAndPremiumCustomersWithActivePayments(
        this IEnumerable<Customer> customers, decimal minAmount)
        => customers.Where(x =>
            x is { IsActive: true, Payments: not null } && x.TotalPurchases >= minAmount &&
            x.Payments.Any(y => y is { IsActive: true, Method: "Card" } || y.Method == "Crypto"));

    public static IEnumerable<Customer> CheckInactiveAndPremiumCustomersWithInactivePayments(
        this IEnumerable<Customer> customers, decimal minAmount)
        => customers.Where(x =>
            x is { IsActive: false, Payments: not null } && x.TotalPurchases >= minAmount &&
            x.Payments.Any(y => y is { IsActive: false }));

    public static IEnumerable<Customer> CheckCustomersWithoutPayments(this IEnumerable<Customer> customers)
        => customers.Where(x => x is { Payments: null });
}