using System;
using System.Collections.Generic;
using System.Linq;

namespace Darwin.Syntax
{
    internal sealed class Parser
    {
        private readonly IList<SyntaxToken> _tokens;
        private int _currentTokenIndex = 0;

        public Parser(IList<SyntaxToken> tokens)
        {
            _tokens = tokens.Where(t => t.Type != TokenType.Space).ToList();
        }

        private SyntaxToken? Current => _tokens.ElementAtOrDefault(_currentTokenIndex);

        private SyntaxToken? Lookahead => _tokens.ElementAtOrDefault(_currentTokenIndex + 1);

        private SyntaxToken AssertToken(TokenType tokenType)
        {
            if (_currentTokenIndex >= _tokens.Count)
            {
                throw new Exception("Cannot read past input length");
            }

            var token = ConsumeToken();
            if (token.Type != tokenType)
            {
                throw new Exception($"Expected {tokenType} but got {token.Type} at column {token.LocationInformation.TextSpan.Start}");
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

        public SyntaxTree Parse()
        {
            var root = ParseExpression();
            var endOfFile = AssertToken(TokenType.EndOfFile);
            return new SyntaxTree(root, endOfFile);
        }

        private DarwinExpression ParseExpression()
        {
            var left = ParseTerm();
            while (Current.Type is TokenType.PlusSign or TokenType.MinusSign)
            {
                var op = ConsumeToken();
                var right = ParseTerm();
                left = new BinaryExpression(left, op, right);
            }

            return left;
        }

        private DarwinExpression ParseTerm()
        {
            var left = ParseFactor();
            while (Current.Type is TokenType.AsteriskSign or TokenType.SlashSign)
            {
                var op = ConsumeToken();
                var right = ParseFactor();
                left = new BinaryExpression(left, op, right);
            }

            return left;
        }

        private DarwinExpression ParseFactor()
        {
            switch (Current.Type)
            {
                case TokenType.Number:
                    return ParseLiteral();
                case TokenType.LeftParenthesis:
                    return ParseParenthesizedExpression();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private LiteralExpression ParseLiteral()
        {
            return new LiteralExpression(ConsumeToken());
        }

        private ParenthesizedExpression ParseParenthesizedExpression()
        {
            var leftParenthesisToken = ConsumeToken();
            var expression = ParseExpression();
            var rightParenthesisToken = AssertToken(TokenType.RightParenthesis);
            return new ParenthesizedExpression(leftParenthesisToken, expression, rightParenthesisToken);
        }
    }
}