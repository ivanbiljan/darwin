namespace Darwin.CodeAnalysis {
    /// <summary>
    /// Represents a syntax token. The smallest lexical unit that has meaning.
    /// </summary>
    internal sealed class SyntaxToken {
        /// <summary>
        /// Initializes a new instance of the <see cref="SyntaxToken"/> class with the specified <see cref="TokenType"/> and value.
        /// </summary>
        /// <param name="type">The type of token.</param>
        /// <param name="value">The value.</param>
        public SyntaxToken(TokenType type, object value) {
            Type = type;
            Value = value;
        }

        /// <summary>
        /// Gets or sets the type of token.
        /// </summary>
        public TokenType Type { get; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public object Value { get; }
    }
}