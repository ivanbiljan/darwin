using System;

namespace Darwin.Syntax
{
    internal sealed class Evaluator
    {
        private readonly SyntaxNode _root;

        public Evaluator(SyntaxNode root)
        {
            _root = root;
        }

        public object Evaluate(SyntaxNode expression)
        {
            return expression switch
            {
                BinaryExpression binaryExpression => EvaluateBinaryExpression(binaryExpression),
                LiteralExpression literalExpression => EvaluateLiteralExpression(literalExpression),
                ParenthesizedExpression parenthesizedExpression => Evaluate(parenthesizedExpression.Expression),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private object EvaluateLiteralExpression(LiteralExpression expression)
        {
            return expression.SyntaxToken.Value!;
        }

        private object EvaluateBinaryExpression(BinaryExpression expression)
        {
            var (leftOperand, @operator, rightOperand) = expression;
            
            var left = (long) Evaluate(leftOperand);
            var right = (long) Evaluate(rightOperand);
            switch (@operator.Type)
            {
                case TokenType.PlusSign:
                    return left + right;
                case TokenType.MinusSign:
                    return left - right;
                case TokenType.AsteriskSign:
                    return left * right;
                case TokenType.SlashSign:
                    return left / right;
            }
            
            return default;
        }
    }
}