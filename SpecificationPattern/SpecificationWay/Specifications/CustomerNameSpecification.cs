using System.Linq.Expressions;
using SpecificationPattern.SpecificationWay.Models;
using SpecificationPattern.SpecificationWay.Specifications.Base;

namespace SpecificationPattern.SpecificationWay.Specifications;

public class CustomerNameSpecification : Specification<Customer>
{
    public string Name { get; set; } = string.Empty;
    public override Expression<Func<Customer, bool>> ToExpression() => customer => customer.Name == Name;
}