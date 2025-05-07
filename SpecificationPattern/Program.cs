using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Settings;
using SpecificationPattern.ExpressionWay;
using SpecificationPattern.LinqWay;
using SpecificationPattern.SpecificationWay;

namespace SpecificationPattern;

public class Program
{
    private const string CustomerName = "Bob";

    public static void Main()
    {
        BenchmarkRunner.Run<Program>(new BenchmarkConfig());
    }

    [Benchmark]
    public void RunLinqWay()
    {
        var linq = new LinqWayCustomerService();

        Print(linq.GetActiveCustomers());
        Print(linq.GetPremiumCustomers());
        Print(linq.GetActiveAndPremiumCustomers());
        Print(linq.GetActiveOrPremiumCustomers());
        Print(linq.GetActiveAndPremiumCustomersWithActivePayments());
        Print(linq.GetInactiveCustomers());
        Print(linq.GetInactiveAndPremiumCustomers());
        Print(linq.GetInactiveOrPremiumCustomers());
        Print(linq.GetInactiveAndPremiumCustomersWithInactivePayments());
        Print(linq.GetCustomersWithoutPayments());
        Print(CustomerName, linq.CheckIsActivePremiumCustomerByName(CustomerName));
    }

    [Benchmark]
    public void RunExpressionWay()
    {
        var expression = new ExpressionWayCustomerService();

        Print(expression.GetActiveCustomers());
        Print(expression.GetPremiumCustomers());
        Print(expression.GetActiveAndPremiumCustomers());
        Print(expression.GetActiveOrPremiumCustomers());
        Print(expression.GetActiveAndPremiumCustomersWithActivePayments());
        Print(expression.GetInactiveCustomers());
        Print(expression.GetInactiveAndPremiumCustomers());
        Print(expression.GetInactiveOrPremiumCustomers());
        Print(expression.GetInactiveAndPremiumCustomersWithInactivePayments());
        Print(expression.GetCustomersWithoutPayments());
        Print(CustomerName, expression.CheckIsActivePremiumCustomerByName(CustomerName));
    }
    
    [Benchmark]
    public void RunSpecificationWay()
    {
        var specification = new SpecificationWayCustomerService();

        Print(specification.GetActiveCustomers());
        Print(specification.GetPremiumCustomers());
        Print(specification.GetActiveAndPremiumCustomers());
        Print(specification.GetActiveOrPremiumCustomers());
        Print(specification.GetActiveAndPremiumCustomersWithActivePayments());
        Print(specification.GetInactiveCustomers());
        Print(specification.GetInactiveAndPremiumCustomers());
        Print(specification.GetInactiveOrPremiumCustomers());
        Print(specification.GetInactiveAndPremiumCustomersWithInactivePayments());
        Print(specification.GetCustomersWithoutPayments());
        Print(CustomerName, specification.CheckIsActivePremiumCustomerByName(CustomerName));
    }

    private static void Print<T>(IEnumerable<T> customers) where T : class
    {
        foreach (var customer in customers)
        {
            Console.WriteLine(customer.ToString());
        }

        Console.WriteLine("\n");
    }

    private static void Print(string title, bool isActivePremiumCustomer)
    {
        Console.WriteLine($"{title} is active premium customer: {isActivePremiumCustomer}");
    }
}