using System;
using System.Collections.Generic;
using Darwin.Diagnostics;

namespace Darwin.LexicalAnalysis;

internal ref struct Lexer
{
    private readonly ReadOnlySpan<char> _input;
    private int _lineNumber = 1;
    private int _position;

    /// <summary>
    ///     Initializes a new instance of the <see cref="Lexer" /> class with the specified input string.
    /// </summary>
    /// <param name="input">The input string.</param>
    public Lexer(ReadOnlySpan<char> input)
    {
        _input = input;
    }

    public IEnumerable<SyntaxToken> TokenizeInput()
    {
        var tokens = new List<SyntaxToken>();

        while (true)
        {
            var currentToken = EmitToken();
            if (currentToken is null)
            {
                continue;
            }
            
            if (currentToken is {Type: TokenType.EndOfFile})
            {
                break;
            }
            
            tokens.Add(currentToken);
        }

        return tokens;
    }

    private char Lookahead
    {
        get
        {
            if (_input.IsEmpty || _position + 1 >= _input.Length)
            {
                return '\0';
            }

            return _input[_position + 1];
        }
    }

    /// <summary>
    ///     Scans the input string for the next token and emits it.
    /// </summary>
    /// <returns>The next token.</returns>
    private SyntaxToken? EmitToken()
    {
        if (_position >= _input.Length)
        {
            return new SyntaxToken(
                TokenType.EndOfFile,
                new SourceLocation(0, new TextSpan(_input.Length, 0)),
                "EOF"
            );
        }

        switch (_input[_position])
        {
            case '"':
            {
                var start = _position;
                while (_position < _input.Length && Lookahead != '"')
                {
                    _position++;
                }

                if (_position >= _input.Length)
                {
                    DiagnosticBag.AddError(
                        new SourceLocation(_lineNumber, TextSpan.FromBounds(start, _position)),
                        "Unterminated string"
                    );

                    return null;
                }
                
                _position++;
                
                var token = new SyntaxToken(
                    TokenType.StringLiteral,
                    new SourceLocation(_lineNumber, TextSpan.FromBounds(start, _position)),
                    _input[(start + 1).._position].ToString()
                );

                return token;
            }
            case '_' or >= 'a' and <= 'z' or >= 'A' and <= 'Z':
            {
                var start = _position;
                while (char.IsAsciiLetterOrDigit(_input[_position]) || _input[_position] is '_')
                {
                    _position++;
                }

                var keywordOrIdentifier = _input[start.._position].ToString();
                var tokenType = SyntaxRules.Keywords.GetValueOrDefault(keywordOrIdentifier, TokenType.Identifier);
                
                return new SyntaxToken(
                    tokenType,
                    new SourceLocation(_lineNumber, TextSpan.FromBounds(start, _position)),
                    keywordOrIdentifier
                );
            }
            case >= '0' and <= '9':
            {
                var start = _position;
                while (_position < _input.Length && char.IsDigit(_input[_position]))
                {
                    ++_position;
                }

                var stringRepresentation = _input[start.._position].ToString();

                return new SyntaxToken(
                    TokenType.Number,
                    new SourceLocation(0, new TextSpan(start, _position - start)),
                    long.Parse(stringRepresentation)
                );
            }
            case var c when char.IsWhiteSpace(c):
            {
                while (_position < _input.Length && char.IsWhiteSpace(_input[_position]))
                {
                    ++_position;
                }

                return null;
            }
            case '\n':
            case '\r':
            {
                if (_input[_position] == '\r' && Lookahead == '\n')
                {
                    _position += 2;
                    _lineNumber++;

                    return null;
                }

                while (_input[_position] == '\n')
                {
                    _lineNumber++;
                }

                return null;
            }
            case '+':
                return new SyntaxToken(
                    TokenType.PlusSign,
                    new SourceLocation(0, new TextSpan(_position++, 1)),
                    "+"
                );
            case '-':
                return new SyntaxToken(
                    TokenType.MinusSign,
                    new SourceLocation(0, new TextSpan(_position++, 1)),
                    "-"
                );
            case '*':
            {
                if (Lookahead == '*')
                {
                    _position += 2;

                    return new SyntaxToken(
                        TokenType.DoubleAsteriskSign,
                        new SourceLocation(0, new TextSpan(_position, 2)),
                        "**"
                    );
                }

                return new SyntaxToken(
                    TokenType.AsteriskSign,
                    new SourceLocation(0, new TextSpan(_position++, 1)),
                    "*"
                );
            }
            case '/':
                return new SyntaxToken(
                    TokenType.SlashSign,
                    new SourceLocation(0, new TextSpan(_position++, 1)),
                    "/"
                );
            case '(':
                return new SyntaxToken(
                    TokenType.LeftParenthesis,
                    new SourceLocation(0, new TextSpan(_position++, 1)),
                    "("
                );
            case ')':
                return new SyntaxToken(
                    TokenType.RightParenthesis,
                    new SourceLocation(0, new TextSpan(_position++, 1)),
                    ")"
                );
            default:
                throw new ArgumentOutOfRangeException(nameof(_input), $"Unsupported token {_input[_position]}");
        }
    }
}