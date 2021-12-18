using System;
using System.Collections.Generic;
using System.Linq;

namespace Darwin.Syntax
{
    internal abstract record SyntaxNode
    {
        public virtual IEnumerable<SyntaxNode> GetChildren() => Enumerable.Empty<SyntaxNode>();
    }

    /// <summary>
    /// Represents the base class for an expression. 
    /// </summary>
    internal abstract record DarwinExpression : SyntaxNode
    {
        /// <summary>
        /// Gets the type of expression.
        /// </summary>
        public abstract DarwinExpressionType Type { get; }
    }
    
    internal sealed record LiteralExpression(SyntaxToken SyntaxToken) : DarwinExpression
    {
        public override DarwinExpressionType Type => DarwinExpressionType.Literal;

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return SyntaxToken;
        }
    }

    internal sealed record BinaryExpression(DarwinExpression LeftOperand, SyntaxToken Operator,
        DarwinExpression RightOperand) : DarwinExpression
    {
        public override DarwinExpressionType Type => DarwinExpressionType.Binary;

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return LeftOperand;
            yield return Operator;
            yield return RightOperand;
        }
    }

    internal enum DarwinExpressionType
    {
        Unary,
        Binary,
        Literal
    }

    internal sealed class SyntaxTree
    {
        public SyntaxTree(SyntaxNode root, SyntaxToken endOfFile)
        {
            Root = root;
            EndOfFile = endOfFile;
        }

        public SyntaxToken EndOfFile { get; }
        public SyntaxNode Root { get; }
    }

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

            var token = _tokens.ElementAt(_currentTokenIndex);
            if (token.Type != tokenType)
            {
                throw new Exception($"Expected {tokenType} but got {token.Type}");
            }

            return token;
        }

        public SyntaxTree Parse()
        {
            var root = ParseExpression();
            var endOfFile = AssertToken(TokenType.EndOfFile);
            return new SyntaxTree(root, endOfFile);
        }

        private DarwinExpression ParseExpression()
        {
            var left = ParseLiteral();
            while (Current.Type == TokenType.PlusSign || Current.Type == TokenType.MinusSign)
            {
                var op = _tokens[_currentTokenIndex++];
                var right = ParseExpression();
                return new BinaryExpression(left, op, right);
            }

            return left;
        }

        private LiteralExpression ParseLiteral()
        {
            return new LiteralExpression(_tokens.ElementAtOrDefault(_currentTokenIndex++));
        }
    }

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