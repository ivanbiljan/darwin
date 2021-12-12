using System.Collections.Generic;
using System.Linq;

namespace Darwin.Syntax
{
    /// <summary>
    /// Represents the base class for an expression. 
    /// </summary>
    internal abstract class DarwinExpression
    {
        /// <summary>
        /// Gets the type of expression.
        /// </summary>
        public abstract TokenType Type { get; }

        public virtual IEnumerable<DarwinExpression> GetChildren() => Enumerable.Empty<DarwinExpression>();
    }

    internal sealed class UnaryExpression : DarwinExpression
    {
        public override TokenType Type { get; }
    }

    internal sealed class BinaryExpression : DarwinExpression
    {
        public DarwinExpression Left { get; }
        public TokenType OperatorType { get; }
        public DarwinExpression Right { get; }

        public BinaryExpression(DarwinExpression left, TokenType operatorType, DarwinExpression right)
        {
            Left = left;
            OperatorType = operatorType;
            Right = right;
        }
        
        public override TokenType Type => TokenType.BinaryExpression;

        public override IEnumerable<DarwinExpression> GetChildren()
        {
            yield return Left;
            yield return Right;
        }
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