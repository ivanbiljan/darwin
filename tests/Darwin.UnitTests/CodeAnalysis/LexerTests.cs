using System;
using System.Collections.Generic;
using Darwin.CodeAnalysis;
using Xunit;

namespace Darwin.UnitTests
{
    public sealed class LexerTests
    {
        public static IEnumerable<object[]> ParseData = new object[][]
        {
            new object[] {"1 + 2 * 3456 * (4 / 20)", new SyntaxToken[]
            {
                new(TokenType.Number, 1L),
                new(TokenType.Space, " "),
                new(TokenType.PlusSign, "+"),
                new(TokenType.Space, " "),
                new(TokenType.Number, 2L),
                new(TokenType.Space, " "),
                new(TokenType.AsteriskSign, "*"),
                new(TokenType.Space, " "),
                new(TokenType.Number, 3456L),
                new(TokenType.Space, " "),
                new(TokenType.AsteriskSign, "*"),
                new(TokenType.Space, " "),
                new(TokenType.LeftParentheses, "("),
                new(TokenType.Number, 4L),
                new(TokenType.Space, " "),
                new(TokenType.SlashSign, "/"),
                new(TokenType.Space, " "),
                new(TokenType.Number, 20L),
                new(TokenType.RightParentheses, ")"),
                new(TokenType.EndOfFile),
            }}
        };

        [Fact]
        private void Parse_EmptyString_ReturnsEof()
        {
            var lexer = new Lexer("");

            var token = lexer.Emit();
            
            Assert.Equal(TokenType.EndOfFile, token.Type);
            Assert.Null(token.Value);
        }
        
        [Theory]
        [MemberData(nameof(ParseData))]
        private void Parse_IsCorrect(string input, IEnumerable<SyntaxToken> expectedTokens)
        {
            var lexer = new Lexer(input);

            foreach (var expectedToken in expectedTokens)
            {
                var actualToken = lexer.Emit();
                Assert.Equal(expectedToken.Type, actualToken.Type);
                Assert.Equal(expectedToken.Value, actualToken.Value);
            }
        }
    }
}