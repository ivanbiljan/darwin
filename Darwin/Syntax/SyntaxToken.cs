﻿namespace Darwin.Syntax
{
    /// <summary>
    ///     Represents a syntax token. The smallest lexical unit that has meaning.
    /// </summary>
    /// <param name="Type">The type of token.</param>
    /// <param name="Lexeme">A string representation of the parsed lexeme.</param>
    /// <param name="Value">The value associated with this token.</param>
    internal record SyntaxToken(TokenType Type, SourceLocation LocationInformation, string Lexeme,
        object? Value = null) : SyntaxNode;
}