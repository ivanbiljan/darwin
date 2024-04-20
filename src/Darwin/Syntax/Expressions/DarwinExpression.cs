using Darwin.LexicalAnalysis;

namespace Darwin.Syntax;

/// <summary>
///     Represents the base class for an expression.
/// </summary>
internal abstract record DarwinExpression : SyntaxNode
{
    /// <summary>
    ///     Gets the type of expression.
    /// </summary>
    public abstract DarwinExpressionType Type { get; }

    public abstract T Accept<T>(Visitor<T> visitor);
}