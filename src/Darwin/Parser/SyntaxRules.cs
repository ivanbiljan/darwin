﻿using System.Collections.Generic;

namespace Darwin.Parser;

internal static class SyntaxRules
{
    public static readonly Dictionary<string, TokenType> Keywords = new()
    {
        ["let"] = TokenType.LetKeyword,
        ["if"] = TokenType.IfKeyword,
        ["else"] = TokenType.ElseKeyword,
        ["elif"] = TokenType.ElseIfKeyword,
        ["for"] = TokenType.ForKeyword,
        ["while"] = TokenType.WhileKeyword,
        ["func"] = TokenType.FunctionKeyword,
        ["string"] = TokenType.StringKeyword,
        ["bool"] = TokenType.BooleanKeyword,
        ["number"] = TokenType.NumberKeyword
    };

    public static int GetBinaryOperatorPrecedence(TokenType type)
    {
        switch (type)
        {
            case TokenType.DoubleAsteriskSign:
                return 5;

            case TokenType.AsteriskSign:
            case TokenType.SlashSign:
                return 4;

            case TokenType.PlusSign:
            case TokenType.MinusSign:
                return 3;

            case TokenType.LessThan:
            case TokenType.LessThanOrEqual:
            case TokenType.GreaterThan:
            case TokenType.GreaterThanOrEqual:
                return 2;

            case TokenType.EqualsEquals:
            case TokenType.BangEquals:
                return 1;

            default:
                return 0;
        }
    }
}