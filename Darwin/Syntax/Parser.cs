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

    internal sealed class Parser
    {
        private readonly IList<SyntaxToken> _tokens;
        private int _currentTokenIndex = 0;

        public Parser(IList<SyntaxToken> tokens)
        {
            _tokens = tokens;
        }

        private SyntaxToken? Current => _tokens.ElementAtOrDefault(_currentTokenIndex);

        private SyntaxToken? Lookahead => _tokens.ElementAtOrDefault(_currentTokenIndex + 1);

        public BinaryExpression ParseBinaryExpression()
        {
            return default;
        }

        public LiteralExpression ParseLiteral()
        {
            return new LiteralExpression(_tokens[_currentTokenIndex++]);
        }
    }

    internal sealed class Evaluator
    {
        private readonly SyntaxNode _root;

        public Evaluator(SyntaxNode root)
        {
            _root = root;
        }

        public object? Evaluate(SyntaxNode expression)
        {
            return null;
        }

        private object? EvaluateBinaryExpression(BinaryExpression expression)
        {
            return default;
        }
    }
}