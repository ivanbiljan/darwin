using System.Collections.Generic;

namespace Darwin.Syntax.Expressions;

internal sealed record UnaryExpression(SyntaxToken Operator, DarwinExpression Expression) : DarwinExpression
{
    public override DarwinExpressionType Type => DarwinExpressionType.Unary;

    public override IEnumerable<SyntaxNode> GetChildren()
    {
        yield return Operator;
        yield return Expression;
    }
}