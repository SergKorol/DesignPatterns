using SpecificationPattern.SpecificationWay.Models;
using SpecificationPattern.SpecificationWay.Specifications;

namespace SpecificationPattern.SpecificationWay;

public class SpecificationWayCustomerService
{
    private readonly IEnumerable<Customer> _customers =
    [
        new(1, "Alice", true, 1500m, [new("Card", true), new("PayPal", false), new("Crypto", true)]),
        new(2, "Bob", false, 800m, null),
        new(3, "Charlie", true, 200m, [new("Card", false), new("PayPal", false), new("Crypto", true)]),
        new(4, "David", true, 1200m, null),
        new(5, "Eve", false, 2500m, [new("Card", false), new("PayPal", false), new("Crypto", false)])
    ];

    private static readonly ActiveCustomerSpecification ActiveSpec = new();
    private static readonly PremiumCustomerSpecification PremiumSpec = new();
    private static readonly CustomerNameSpecification NameSpec = new();
    private static readonly HasPaymentsSpecification HasPaymentsSpec = new();
    private static readonly NoPaymentsSpecification NoPaymentsSpec = new();
    private static readonly ActiveCardOrCryptoPaymentSpecification ActiveCardOrCryptoSpec = new();
    private static readonly HasActivePaymentSpecification HasActivePaymentSpec = new();
    private static readonly HasInactivePaymentSpecification HasInactivePaymentSpec = new();

    private static readonly Func<Customer, bool> ActiveFunc = ActiveSpec.GetCompiledExpression();
    private static readonly Func<Customer, bool> PremiumFunc = PremiumSpec.GetCompiledExpression();

    private static readonly Func<Customer, bool> ActiveAndPremiumFunc =
        (ActiveSpec & PremiumSpec).GetCompiledExpression();

    private static readonly Func<Customer, bool> ActiveOrPremiumFunc =
        (ActiveSpec | PremiumSpec).GetCompiledExpression();

    private static readonly Func<Customer, bool> InactiveFunc = (!ActiveSpec).GetCompiledExpression();

    private static readonly Func<Customer, bool> InactiveAndPremiumFunc =
        (!ActiveSpec & PremiumSpec).GetCompiledExpression();

    private static readonly Func<Customer, bool> InactiveOrPremiumFunc =
        (!ActiveSpec | PremiumSpec).GetCompiledExpression();

    private static readonly Func<Customer, bool> NoPaymentsFunc = NoPaymentsSpec.GetCompiledExpression();

    private static readonly Func<Customer, bool> ActivePremiumWithActivePaymentsFunc =
        (ActiveSpec & PremiumSpec & HasPaymentsSpec & ActiveCardOrCryptoSpec).GetCompiledExpression();

    private static readonly Func<Customer, bool> InactivePremiumWithInactivePaymentsFunc =
        (!ActiveSpec & PremiumSpec & HasPaymentsSpec & HasInactivePaymentSpec).GetCompiledExpression();

    public IEnumerable<Customer> GetActiveCustomers() => _customers.Where(ActiveFunc);
    public IEnumerable<Customer> GetPremiumCustomers() => _customers.Where(PremiumFunc);
    public IEnumerable<Customer> GetActiveAndPremiumCustomers() => _customers.Where(ActiveAndPremiumFunc);
    public IEnumerable<Customer> GetActiveOrPremiumCustomers() => _customers.Where(ActiveOrPremiumFunc);
    public IEnumerable<Customer> GetInactiveCustomers() => _customers.Where(InactiveFunc);
    public IEnumerable<Customer> GetInactiveAndPremiumCustomers() => _customers.Where(InactiveAndPremiumFunc);
    public IEnumerable<Customer> GetInactiveOrPremiumCustomers() => _customers.Where(InactiveOrPremiumFunc);

    public IEnumerable<Customer> GetActiveAndPremiumCustomersWithActivePayments()
        => _customers.Where(ActivePremiumWithActivePaymentsFunc);

    public IEnumerable<Customer> GetInactiveAndPremiumCustomersWithInactivePayments()
        => _customers.Where(InactivePremiumWithInactivePaymentsFunc);

    public IEnumerable<Customer> GetCustomersWithoutPayments()
        => _customers.Where(NoPaymentsFunc);

    public bool CheckIsActivePremiumCustomerByName(string name)
    {
        NameSpec.Name = name;
        var combinedSpec = NameSpec & ActiveSpec & PremiumSpec;
        return _customers.Any(combinedSpec.IsSatisfiedBy);
    }
}