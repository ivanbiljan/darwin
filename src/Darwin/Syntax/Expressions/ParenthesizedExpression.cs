﻿using System.Collections.Generic;
using Darwin.Parser;

namespace Darwin.Syntax;

internal sealed record ParenthesizedExpression(
    SyntaxToken LeftParenthesisToken,
    DarwinExpression Expression,
    SyntaxToken RightParenthesisToken
) : DarwinExpression
{
    public override DarwinExpressionType Type => DarwinExpressionType.Parenthesized;

    public override T Accept<T>(SyntaxVisitor<T> syntaxVisitor)
    {
        return syntaxVisitor.VisitParenthesizedExpression(this);
    }

    public override IEnumerable<SyntaxNode> GetChildren()
    {
        yield return LeftParenthesisToken;
        yield return Expression;
        yield return RightParenthesisToken;
    }
}