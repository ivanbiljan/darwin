﻿namespace Darwin.Parser;

public enum TokenType
{
    Space = 1,
    Number = 2,
    PlusSign = 3,
    MinusSign = 4,
    AsteriskSign = 5,
    SlashSign = 6,
    LeftParenthesis = 7,
    RightParenthesis = 8,
    Dot = 9,
    Colon = 10,
    Semicolon = 11,
    LeftCurlyBrace = 12,
    RightCurlyBrace = 13,
    DoubleAsteriskSign = 14,
    Bang = 15,
    BangEquals = 16,
    LessThan = 17,
    LessThanOrEqual = 18,
    GreaterThan = 19,
    GreaterThanOrEqual = 20,
    IfKeyword = 21,
    ElseKeyword = 22,
    ElseIfKeyword = 23,
    LetKeyword = 24,
    StringKeyword = 25,
    BooleanKeyword = 26,
    NumberKeyword = 27,
    WhileKeyword = 28,
    ForKeyword = 29,
    FunctionKeyword = 30,
    Equals = 31,
    EqualsEquals = 32,
    StringLiteral = 33,
    Identifier = 34,
    Illegal = 98,
    EndOfFile = 99
}