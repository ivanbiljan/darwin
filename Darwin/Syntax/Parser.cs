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

    internal enum DarwinExpressionType
    {
        Unary,
        Binary,
        Literal
    }

    internal sealed class Parser
    {
        private readonly IList<SyntaxToken> _tokens;
        private int _tokenIndex;

        public Parser(IList<SyntaxToken> tokens)
        {
            _tokens = tokens;
        }

        private SyntaxToken? Current => _tokens.ElementAtOrDefault(_tokenIndex);
    }
}