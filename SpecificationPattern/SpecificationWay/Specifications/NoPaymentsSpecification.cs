using System.Linq.Expressions;
using SpecificationPattern.SpecificationWay.Models;
using SpecificationPattern.SpecificationWay.Specifications.Base;

namespace SpecificationPattern.SpecificationWay.Specifications;

public class NoPaymentsSpecification : Specification<Customer>
{
    public override Expression<Func<Customer, bool>> ToExpression()
    {
        return customer => customer.Payments == null;
    }
}