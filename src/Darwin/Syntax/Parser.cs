using System;
using System.Collections.Generic;
using System.Linq;
using Darwin.Syntax.Expressions;

namespace Darwin.Syntax
{
    internal sealed class Parser
    {
        private readonly IList<SyntaxToken> _tokens;
        private int _currentTokenIndex;

        public Parser(IEnumerable<SyntaxToken> tokens)
        {
            _tokens = tokens.Where(t => t.Type != TokenType.Space).ToList();
        }

        private SyntaxToken? Current => _tokens.ElementAtOrDefault(_currentTokenIndex);

        private SyntaxToken? Lookahead => _tokens.ElementAtOrDefault(_currentTokenIndex + 1);

        public SyntaxTree Parse()
        {
            var root = ParseExpression();
            var endOfFile = AssertToken(TokenType.EndOfFile);
            return new SyntaxTree(root, endOfFile);
        }

        private SyntaxToken AssertToken(TokenType tokenType)
        {
            if (_currentTokenIndex >= _tokens.Count)
            {
                throw new Exception("Cannot read past input length");
            }

            var token = ConsumeToken();
            if (token.Type != tokenType)
            {
                throw new Exception(
                    $"Expected {tokenType} but got {token.Type} at column {token.LocationInformation.TextSpan.Start}");
            }

            return token;
        }

        private SyntaxToken ConsumeToken()
        {
            if (_currentTokenIndex >= _tokens.Count)
            {
                throw new Exception("Cannot read past input length");
            }

            return _tokens[_currentTokenIndex++];
        }

        private DarwinExpression ParseExpression(int precedence = 0)
        {
            var left = ParsePrimary();
            while (true)
            {
                var @operator = Current;
                var operatorPrecedence = SyntaxRules.GetBinaryOperatorPrecedence(@operator.Type);
                if (operatorPrecedence <= precedence)
                {
                    break;
                }

                ConsumeToken();
                var right = ParseExpression(operatorPrecedence);
                left = new BinaryExpression(left, @operator, right);
            }

            return left;
        }

        private DarwinExpression ParsePrimary()
        {
            switch (Current.Type)
            {
                case TokenType.MinusSign:
                    return ParseUnaryExpression();
                case TokenType.Number:
                    return ParseLiteral();
                case TokenType.LeftParenthesis:
                    return ParseParenthesizedExpression();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private LiteralExpression ParseLiteral() => new LiteralExpression(ConsumeToken());

        private ParenthesizedExpression ParseParenthesizedExpression()
        {
            var leftParenthesisToken = ConsumeToken();
            var expression = ParseExpression();
            var rightParenthesisToken = AssertToken(TokenType.RightParenthesis);
            return new ParenthesizedExpression(leftParenthesisToken, expression, rightParenthesisToken);
        }

        internal static class SyntaxRules
        {
            public static int GetBinaryOperatorPrecedence(TokenType type)
            {
                switch (type)
                {
                    case TokenType.DoubleAsteriskSign:
                        return 3;
                    
                    case TokenType.AsteriskSign:
                    case TokenType.SlashSign:
                        return 2;
                    
                    case TokenType.PlusSign:
                    case TokenType.MinusSign:
                        return 1;
                    
                    default:
                        return 0;
                }
            }
        }

        private DarwinExpression ParseUnaryExpression()
        {
            var @operator = ConsumeToken();
            var expression = ParseExpression();
            return new UnaryExpression(@operator, expression);
        }
    }
}