namespace Darwin.LexicalAnalysis;

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
    Unexpected = 12,
    LeftCurlyBrace = 13,
    RightCurlyBrace = 14,
    DoubleAsteriskSign = 15,
    Illegal = 98,
    EndOfFile = 99
}