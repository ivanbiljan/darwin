using System;
using System.Collections.Generic;
using Darwin.LexicalAnalysis;

namespace Darwin.Syntax.Expressions;

internal sealed record UnaryExpression(SyntaxToken Operator, DarwinExpression Expression) : DarwinExpression
{
    public override DarwinExpressionType Type => DarwinExpressionType.Unary;

    public override T Accept<T>(SyntaxVisitor<T> syntaxVisitor)
    {
        throw new NotImplementedException();
    }

    public override IEnumerable<SyntaxNode> GetChildren()
    {
        yield return Operator;
        yield return Expression;
    }
}