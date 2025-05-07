using SpecificationPattern.ExpressionWay.Extensions;
using SpecificationPattern.ExpressionWay.Models;

namespace SpecificationPattern.ExpressionWay;

public class ExpressionWayCustomerService
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
        => _customers.FilterActive();

    public IEnumerable<Customer> GetPremiumCustomers()
        => _customers.FilterPremium(PremiumThreshold);

    public IEnumerable<Customer> GetActiveAndPremiumCustomers()
        => _customers.FilterActive().FilterPremium(PremiumThreshold);

    public IEnumerable<Customer> GetActiveOrPremiumCustomers()
        => _customers.FilterActiveOrPremium(PremiumThreshold);

    public IEnumerable<Customer> GetInactiveCustomers()
        => _customers.FilterInactive();

    public IEnumerable<Customer> GetInactiveAndPremiumCustomers()
        => _customers.FilterInactive().FilterPremium(PremiumThreshold);

    public IEnumerable<Customer> GetInactiveOrPremiumCustomers()
        => _customers.FilterInactiveOrPremium(PremiumThreshold);

    public IEnumerable<Customer> GetActiveAndPremiumCustomersWithActivePayments()
        => _customers.CheckActiveAndPremiumCustomersWithActivePayments(PremiumThreshold);

    public IEnumerable<Customer> GetInactiveAndPremiumCustomersWithInactivePayments()
        => _customers.CheckInactiveAndPremiumCustomersWithInactivePayments(PremiumThreshold);

    public IEnumerable<Customer> GetCustomersWithoutPayments()
        => _customers.CheckCustomersWithoutPayments();

    public bool CheckIsActivePremiumCustomerByName(string name)
        => _customers.CheckActivePremiumCustomer(name, PremiumThreshold);
}