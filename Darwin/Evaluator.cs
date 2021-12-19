﻿using System;

namespace Darwin.Syntax
{
    internal sealed class Evaluator
    {
        public object? Evaluate(SyntaxNode expression)
        {
            return expression switch
            {
                UnaryExpression unaryExpression => EvaluateUnaryExpression(unaryExpression),
                BinaryExpression binaryExpression => EvaluateBinaryExpression(binaryExpression),
                LiteralExpression literalExpression => EvaluateLiteralExpression(literalExpression),
                ParenthesizedExpression parenthesizedExpression => Evaluate(parenthesizedExpression.Expression),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private object? EvaluateUnaryExpression(UnaryExpression unaryExpression)
        {
            switch (unaryExpression.Operator.Type)
            {
                case TokenType.MinusSign:
                    return -(long)(Evaluate(unaryExpression.Expression) ?? throw new InvalidOperationException());
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static object? EvaluateLiteralExpression(LiteralExpression expression)
        {
            return expression.SyntaxToken.Value;
        }

        private object EvaluateBinaryExpression(BinaryExpression expression)
        {
            var (leftOperand, @operator, rightOperand) = expression;
            
            var left = (long) (Evaluate(leftOperand) ?? throw new InvalidOperationException());
            var right = (long) (Evaluate(rightOperand) ?? throw new InvalidOperationException());
            return @operator.Type switch
            {
                TokenType.PlusSign => left + right,
                TokenType.MinusSign => left - right,
                TokenType.AsteriskSign => left * right,
                TokenType.SlashSign => left / right,
                _ => throw new ArgumentOutOfRangeException(nameof(@operator.Type), "Unsupported binary operator")
            };
        }
    }
}