using System.Linq.Expressions;
using SpecificationPattern.SpecificationWay.Helpers;
using SpecificationPattern.SpecificationWay.Specifications.Base;

namespace SpecificationPattern.SpecificationWay.Specifications;

public class OrSpecification<T>(ISpecification<T> left, ISpecification<T> right) : Specification<T>
{
    private readonly ISpecification<T> _left = left ?? throw new ArgumentNullException(nameof(left));
    private readonly ISpecification<T> _right = right ?? throw new ArgumentNullException(nameof(right));

    public override Expression<Func<T, bool>> ToExpression() =>
        ExpressionCombiner.CombineOr(_left.ToExpression(), _right.ToExpression());

    public override bool IsSatisfiedBy(T entity) => _left.IsSatisfiedBy(entity) || _right.IsSatisfiedBy(entity);
}