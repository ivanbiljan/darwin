using System.Collections.Generic;
using Darwin.Parser;

namespace Darwin.Syntax;

internal sealed record LiteralExpression(SyntaxToken SyntaxToken) : DarwinExpression
{
    public override DarwinExpressionType Type => DarwinExpressionType.Literal;

    public override T Accept<T>(SyntaxVisitor<T> syntaxVisitor)
    {
        return syntaxVisitor.VisitLiteralExpression(this);
    }

    public override IEnumerable<SyntaxNode> GetChildren()
    {
        yield return SyntaxToken;
    }
}