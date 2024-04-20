using System.Collections.Generic;
using Darwin.Parser;

namespace Darwin.Syntax;

internal sealed record BinaryExpression(
    DarwinExpression LeftOperand,
    SyntaxToken Operator,
    DarwinExpression RightOperand
) : DarwinExpression
{
    public override DarwinExpressionType Type => DarwinExpressionType.Binary;

    public override T Accept<T>(SyntaxVisitor<T> syntaxVisitor)
    {
        return syntaxVisitor.VisitBinaryExpression(this);
    }

    public override IEnumerable<SyntaxNode> GetChildren()
    {
        yield return LeftOperand;
        yield return Operator;
        yield return RightOperand;
    }
}