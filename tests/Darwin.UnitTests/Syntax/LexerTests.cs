// using System;
// using System.Collections.Generic;
// using Darwin.Syntax;
// using Xunit;
//
// namespace Darwin.UnitTests.Syntax
// {
//     public sealed class LexerTests
//     {
//         public static IEnumerable<object[]> ParseData = new object[][]
//         {
//             new object[]
//             {
//                 "1 + 2 * 3456 * (4 / 20)", new SyntaxToken[]
//                 {
//                     new(TokenType.Number, default, "1", 1L),
//                     new(TokenType.Space, default, " "),
//                     new(TokenType.PlusSign, default, "+"),
//                     new(TokenType.Space, default, " "),
//                     new(TokenType.Number, default, "2", 2L),
//                     new(TokenType.Space, default, " "),
//                     new(TokenType.AsteriskSign, default, "*"),
//                     new(TokenType.Space, default, " "),
//                     new(TokenType.Number, default, "3456", 3456L),
//                     new(TokenType.Space, default, " "),
//                     new(TokenType.AsteriskSign, default, "*"),
//                     new(TokenType.Space, default, " "),
//                     new(TokenType.LeftParenthesis, default, "("),
//                     new(TokenType.Number, default, "4", 4L),
//                     new(TokenType.Space, default, " "),
//                     new(TokenType.SlashSign, default, "/"),
//                     new(TokenType.Space, default, " "),
//                     new(TokenType.Number, default, "20", 20L),
//                     new(TokenType.RightParenthesis, default, ")"),
//                     new(TokenType.EndOfFile, default, "EOF"),
//                 }
//             }
//         };
//
//         [Fact]
//         private void Parse_EmptyString_ReturnsEof()
//         {
//             var lexer = new Lexer("");
//
//             var token = lexer.Emit();
//             
//             Assert.Equal(TokenType.EndOfFile, token.Type);
//             Assert.Null(token.Value);
//         }
//         
//         [Theory]
//         [MemberData(nameof(ParseData))]
//         private void Parse_IsCorrect(string input, IEnumerable<SyntaxToken> expectedTokens)
//         {
//             var lexer = new Lexer(input);
//
//             foreach (var expectedToken in expectedTokens)
//             {
//                 var actualToken = lexer.Emit();
//                 Assert.Equal(expectedToken.Type, actualToken.Type);
//                 Assert.Equal(expectedToken.Lexeme, actualToken.Lexeme);
//                 Assert.Equal(expectedToken.Value, actualToken.Value);
//             }
//         }
//     }
// }

