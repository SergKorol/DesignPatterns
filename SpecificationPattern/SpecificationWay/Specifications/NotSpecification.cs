using System.Linq.Expressions;
using SpecificationPattern.SpecificationWay.Helpers;
using SpecificationPattern.SpecificationWay.Specifications.Base;

namespace SpecificationPattern.SpecificationWay.Specifications;

public class NotSpecification<T>(ISpecification<T> inner) : Specification<T>
{
    private readonly ISpecification<T> _inner = inner ?? throw new ArgumentNullException(nameof(inner));
    public override Expression<Func<T, bool>> ToExpression() => ExpressionCombiner.CombineNot(_inner.ToExpression());
    public override bool IsSatisfiedBy(T entity) => !_inner.IsSatisfiedBy(entity);
}