using System.Linq.Expressions;

namespace SpecificationPattern.SpecificationWay.Helpers;

internal static class ExpressionCombiner
{
    public static Expression<Func<T, bool>> CombineAnd<T>(Expression<Func<T, bool>> left,
        Expression<Func<T, bool>> right)
    {
        var param = Expression.Parameter(typeof(T));
        var body = Expression.AndAlso(
            ParameterReplacer.Replace(left.Body, left.Parameters[0], param),
            ParameterReplacer.Replace(right.Body, right.Parameters[0], param));
        return Expression.Lambda<Func<T, bool>>(body, param);
    }

    public static Expression<Func<T, bool>> CombineOr<T>(Expression<Func<T, bool>> left,
        Expression<Func<T, bool>> right)
    {
        var param = Expression.Parameter(typeof(T));
        var body = Expression.OrElse(
            ParameterReplacer.Replace(left.Body, left.Parameters[0], param),
            ParameterReplacer.Replace(right.Body, right.Parameters[0], param));
        return Expression.Lambda<Func<T, bool>>(body, param);
    }

    public static Expression<Func<T, bool>> CombineNot<T>(Expression<Func<T, bool>> expression)
    {
        var param = expression.Parameters[0];
        var body = Expression.Not(expression.Body);
        return Expression.Lambda<Func<T, bool>>(body, param);
    }
}