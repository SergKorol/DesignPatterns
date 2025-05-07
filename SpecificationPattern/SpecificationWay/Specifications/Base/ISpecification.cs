using System.Linq.Expressions;

namespace SpecificationPattern.SpecificationWay.Specifications.Base;

public interface ISpecification<T>
{
    bool IsSatisfiedBy(T entity);
    Expression<Func<T, bool>> ToExpression();
}