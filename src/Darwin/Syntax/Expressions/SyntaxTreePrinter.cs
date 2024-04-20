namespace Darwin.Syntax.Expressions;

internal sealed class SyntaxTreePrinter : Visitor<string>
{
    public string VisitUnaryExpression(UnaryExpression unaryExpression)
    {
        return $"{unaryExpression.Operator.Type} {unaryExpression.Expression.Accept(this)}";
    }

    public string VisitBinaryExpression(BinaryExpression binaryExpression)
    {
        return binaryExpression.Accept(this);
    }

    public string VisitLiteralExpression(LiteralExpression literalExpression)
    {
        if (literalExpression.SyntaxToken.Value is null)
        {
            return "null";
        }
        
        return literalExpression.SyntaxToken.Value.ToString();
    }

    public string VisitParenthesizedExpression(ParenthesizedExpression parenthesizedExpression)
    {
        return $"({parenthesizedExpression.Expression.Accept(this)})";
    }
}