using System;

namespace Darwin.Syntax
{
    // TODO: line number support
    
    internal ref struct Lexer
    {
        private readonly ReadOnlySpan<char> _input;
        private int _position;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Lexer" /> class with the specified input string.
        /// </summary>
        /// <param name="input">The input string.</param>
        public Lexer(ReadOnlySpan<char> input)
        {
            _input = input;
            _position = 0;
        }

        /// <summary>
        ///     Scans the input string for the next token and emits it.
        /// </summary>
        /// <returns>The next token.</returns>
        public SyntaxToken Emit()
        {
            if (_position >= _input.Length)
            {
                return new SyntaxToken(TokenType.EndOfFile, new SourceLocation(0, new TextSpan(_input.Length, 0)),
                    "EOF");
            }

            switch (_input[_position++])
            {
                case <= '9' and >= '0':
                {
                    var start = _position - 1;
                    while (char.IsDigit(_input[_position]))
                    {
                        ++_position;
                    }

                    var stringRepresentation = _input[start.._position].ToString();
                    return new SyntaxToken(TokenType.Number,
                        new SourceLocation(0, new TextSpan(start, _position - start)), stringRepresentation,
                        long.Parse(stringRepresentation));
                }
                case var c when char.IsWhiteSpace(c):
                {
                    var start = _position - 1;
                    while (char.IsWhiteSpace(_input[_position]))
                    {
                        ++_position;
                    }

                    return new SyntaxToken(TokenType.Space,
                        new SourceLocation(0, new TextSpan(start, _position - start)),
                        new string(' ', _position - start));
                }
                case '+':
                    return new SyntaxToken(TokenType.PlusSign, new SourceLocation(0, new TextSpan(_position - 1, 1)),
                        "+");
                case '-':
                    return new SyntaxToken(TokenType.MinusSign, new SourceLocation(0, new TextSpan(_position - 1, 1)),
                        "-");
                case '*':
                    return new SyntaxToken(TokenType.AsteriskSign,
                        new SourceLocation(0, new TextSpan(_position - 1, 1)), "*");
                case '/':
                    return new SyntaxToken(TokenType.SlashSign, new SourceLocation(0, new TextSpan(_position - 1, 1)),
                        "/");
                case '(':
                    return new SyntaxToken(TokenType.LeftParenthesis,
                        new SourceLocation(0, new TextSpan(_position - 1, 1)), "(");
                case ')':
                    return new SyntaxToken(TokenType.RightParenthesis,
                        new SourceLocation(0, new TextSpan(_position - 1, 1)), ")");
                default:
                    throw new ArgumentOutOfRangeException(nameof(_input), $"Unsupported token {_input[_position]}");
            }
        }
    }
}