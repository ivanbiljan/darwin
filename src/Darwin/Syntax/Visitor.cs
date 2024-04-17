using Darwin.Syntax.Expressions;

namespace Darwin.Syntax;

internal interface Visitor<out T>
{
    T VisitUnaryExpression(UnaryExpression unaryExpression);

    T VisitBinaryExpression(BinaryExpression binaryExpression);

    T VisitLiteralExpression(LiteralExpression literalExpression);

    T VisitParenthesizedExpression(ParenthesizedExpression parenthesizedExpression);
}