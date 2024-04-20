using System.Collections.Generic;
using Darwin.LexicalAnalysis;

namespace Darwin.Syntax;

internal sealed record BinaryExpression(
    DarwinExpression LeftOperand,
    SyntaxToken Operator,
    DarwinExpression RightOperand
) : DarwinExpression
{
    public override DarwinExpressionType Type => DarwinExpressionType.Binary;

    public override T Accept<T>(Visitor<T> visitor)
    {
        return visitor.VisitBinaryExpression(this);
    }

    public override IEnumerable<SyntaxNode> GetChildren()
    {
        yield return LeftOperand;
        yield return Operator;
        yield return RightOperand;
    }
}