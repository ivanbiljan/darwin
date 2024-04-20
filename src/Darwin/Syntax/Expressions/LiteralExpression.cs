using System.Collections.Generic;
using Darwin.LexicalAnalysis;

namespace Darwin.Syntax;

internal sealed record LiteralExpression(SyntaxToken SyntaxToken) : DarwinExpression
{
    public override DarwinExpressionType Type => DarwinExpressionType.Literal;

    public override T Accept<T>(Visitor<T> visitor)
    {
        return visitor.VisitLiteralExpression(this);
    }

    public override IEnumerable<SyntaxNode> GetChildren()
    {
        yield return SyntaxToken;
    }
}