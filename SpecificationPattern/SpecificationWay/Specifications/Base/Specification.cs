using System.Linq.Expressions;

namespace SpecificationPattern.SpecificationWay.Specifications.Base;

public abstract class Specification<T> : ISpecification<T>
{
    private Func<T, bool>? _compiled;

    public abstract Expression<Func<T, bool>> ToExpression();

    public Func<T, bool> GetCompiledExpression()
        => _compiled ??= ToExpression().Compile();

    public virtual bool IsSatisfiedBy(T entity)
        => GetCompiledExpression()(entity);

    public static Specification<T> operator &(Specification<T> left, ISpecification<T> right)
        => new AndSpecification<T>(left, right);

    public static Specification<T> operator |(Specification<T> left, ISpecification<T> right)
        => new OrSpecification<T>(left, right);

    public static Specification<T> operator !(Specification<T> spec)
        => new NotSpecification<T>(spec);

    public static implicit operator Expression<Func<T, bool>>(Specification<T> spec)
        => spec.ToExpression();
}