using System.Linq.Expressions;
using SpecificationPattern.SpecificationWay.Models;
using SpecificationPattern.SpecificationWay.Specifications.Base;

namespace SpecificationPattern.SpecificationWay.Specifications;

public class PremiumCustomerSpecification(decimal threshold = 1000m) : Specification<Customer>
{
    public override Expression<Func<Customer, bool>> ToExpression() => customer => customer.TotalPurchases >= threshold;
}