﻿using System.Collections.Generic;

namespace Darwin.Syntax
{
    /// <summary>
    ///     Represents a syntax token. The smallest lexical unit that has meaning.
    /// </summary>
    /// <param name="Type">The type of token.</param>
    /// <param name="Lexeme">A string representation of the parsed lexeme.</param>
    /// <param name="Value">The value associated with this token.</param>
    internal record SyntaxToken(TokenType Type, SourceLocation LocationInformation, string Lexeme,
        object? Value = null) : SyntaxNode;

    internal static class SyntaxTokenExtensions
    {
        private static readonly IList<TokenType> UnaryOperators = new List<TokenType>
        {
            TokenType.PlusSign,
            TokenType.MinusSign
        };

        public static bool IsUnaryOperator(this SyntaxToken syntaxToken) => UnaryOperators.Contains(syntaxToken.Type);
    }
}