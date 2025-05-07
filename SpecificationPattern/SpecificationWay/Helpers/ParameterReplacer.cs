using System.Linq.Expressions;

namespace SpecificationPattern.SpecificationWay.Helpers;

internal class ParameterReplacer : ExpressionVisitor
{
    private readonly ParameterExpression _source;
    private readonly ParameterExpression _target;

    private ParameterReplacer(ParameterExpression source, ParameterExpression target)
    {
        _source = source;
        _target = target;
    }

    public static Expression Replace(Expression expression, ParameterExpression source, ParameterExpression target)
        => new ParameterReplacer(source, target).Visit(expression);

    protected override Expression VisitParameter(ParameterExpression node)
        => node == _source ? _target : base.VisitParameter(node);
}