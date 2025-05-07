using SpecificationPattern.LinqWay.Models;

namespace SpecificationPattern.LinqWay;

public class LinqWayCustomerService
{
    private readonly IEnumerable<Customer> _customers =
    [
        new(1, "Alice", true, 1500m, [new("Card", true), new("PayPal", false), new("Crypto", true)]),
        new(2, "Bob", false, 800m, null),
        new(3, "Charlie", true, 200m, [new("Card", false), new("PayPal", false), new("Crypto", true)]),
        new(4, "David", true, 1200m, null),
        new(5, "Eve", false, 2500m, [new("Card", false), new("PayPal", false), new("Crypto", false)])
    ];

    const decimal PremiumThreshold = 1000m;

    public IEnumerable<Customer> GetActiveCustomers()
        => _customers.Where(x => x.IsActive);

    public IEnumerable<Customer> GetPremiumCustomers()
        => _customers.Where(x => x.TotalPurchases >= PremiumThreshold);

    public IEnumerable<Customer> GetActiveAndPremiumCustomers()
        => _customers.Where(x => x is { IsActive: true, TotalPurchases: >= PremiumThreshold });

    public IEnumerable<Customer> GetActiveOrPremiumCustomers()
        => _customers.Where(x => x.IsActive || x.TotalPurchases >= PremiumThreshold);

    public IEnumerable<Customer> GetInactiveCustomers()
        => _customers.Where(x => !x.IsActive);

    public IEnumerable<Customer> GetInactiveAndPremiumCustomers()
        => _customers.Where(x => x is { IsActive: false, TotalPurchases: >= PremiumThreshold });

    public IEnumerable<Customer> GetInactiveOrPremiumCustomers()
        => _customers.Where(x => !x.IsActive || x.TotalPurchases >= PremiumThreshold);

    public IEnumerable<Customer> GetActiveAndPremiumCustomersWithActivePayments()
        => _customers.Where(x =>
            x is { IsActive: true, TotalPurchases: >= PremiumThreshold, Payments: not null } &&
            x.Payments.Any(y => y is { IsActive: true, Method: "Card" } || y.Method == "Crypto"));

    public IEnumerable<Customer> GetInactiveAndPremiumCustomersWithInactivePayments()
        => _customers.Where(x =>
            x is { IsActive: false, TotalPurchases: >= PremiumThreshold, Payments: not null } &&
            x.Payments.Any(y => y is { IsActive: false }));

    public IEnumerable<Customer> GetCustomersWithoutPayments()
        => _customers.Where(x => x is { Payments: null });

    public bool CheckIsActivePremiumCustomerByName(string name)
    {
        Customer? customer = _customers.FirstOrDefault(x => x.Name == name);

        return customer is { IsActive: true, TotalPurchases: >= PremiumThreshold };
    }
}