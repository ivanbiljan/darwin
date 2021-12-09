using System;

namespace Darwin.CodeAnalysis
{
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
                return new SyntaxToken(TokenType.EndOfFile);
            }

            switch (_input[_position++])
            {
                case var digit when digit >= '0' && digit <= '9':
                {
                    var start = _position - 1;
                    while (char.IsDigit(_input[_position]))
                    {
                        ++_position;
                    }

                    return new SyntaxToken(TokenType.Number, long.Parse(_input[start.._position]));
                }
                case var c when char.IsWhiteSpace(c):
                {
                    var start = _position - 1;
                    while (char.IsWhiteSpace(_input[_position]))
                    {
                        ++_position;
                    }

                    return new SyntaxToken(TokenType.Space, new string(' ', _position - start));
                }
                case '+':
                    return new SyntaxToken(TokenType.PlusSign, "+");
                case '-':
                    return new SyntaxToken(TokenType.MinusSign, "-");
                case '*':
                    return new SyntaxToken(TokenType.AsteriskSign, "*");
                case '/':
                    return new SyntaxToken(TokenType.SlashSign, "/");
                case '(':
                    return new SyntaxToken(TokenType.LeftParentheses, "(");
                case ')':
                    return new SyntaxToken(TokenType.RightParentheses, ")");
                default:
                    throw new ArgumentOutOfRangeException(nameof(_input), $"Unsupported token {_input[_position]}");
            }
        }
    }
}