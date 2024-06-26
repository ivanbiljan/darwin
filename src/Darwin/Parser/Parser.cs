﻿using System;
using System.Collections.Generic;
using System.Linq;
using Darwin.Syntax;
using Darwin.Syntax.Expressions;

namespace Darwin.Parser;

internal sealed class Parser
{
    private readonly IList<SyntaxToken> _tokens;
    private int _currentTokenIndex;

    public Parser(IEnumerable<SyntaxToken> tokens)
    {
        _tokens = tokens.Where(t => t.Type != TokenType.Space).ToList();
    }

    private SyntaxToken? Current => _tokens.ElementAtOrDefault(_currentTokenIndex);

    private SyntaxToken? Lookahead => _tokens.ElementAtOrDefault(_currentTokenIndex + 1);

    public SyntaxTree Parse()
    {
        var root = ParseExpression();
        var endOfFile = AssertToken(TokenType.EndOfFile);

        return new SyntaxTree(root, endOfFile);
    }

    private SyntaxToken AssertToken(TokenType tokenType)
    {
        if (_currentTokenIndex >= _tokens.Count)
        {
            throw new Exception("Cannot read past input length");
        }

        var token = ConsumeToken();
        if (token.Type != tokenType)
        {
            throw new Exception(
                $"Expected {tokenType} but got {token.Type} at column {token.LocationInformation.TextSpan.Start}"
            );
        }

        return token;
    }

    private SyntaxToken ConsumeToken()
    {
        if (_currentTokenIndex >= _tokens.Count)
        {
            throw new Exception("Cannot read past input length");
        }

        return _tokens[_currentTokenIndex++];
    }

    private DarwinExpression ParseExpression(int precedence = 0)
    {
        /***
         * The grammar is as follows:
         * expression => equality (==, !=)
         * equality => comparison (== | !=) comparison
         * comparison => term (<, >=, >, >=) term
         * term => factor (- | +) factor
         * factor => unary (* | /) unary
         * unary => ! unary | primary
         * primary => numeric literals, groupings
         *
         * The order is used to define the precedence of binary operators
         */
        
        var left = ParsePrimaryExpression();
        while (true)
        {
            var @operator = Current;
            var operatorPrecedence = SyntaxRules.GetBinaryOperatorPrecedence(@operator.Type);
            if (operatorPrecedence <= precedence)
            {
                break;
            }

            ConsumeToken();
            var right = ParseExpression(operatorPrecedence);
            left = new BinaryExpression(left, @operator, right);
        }

        return left;
    }

    private DarwinExpression ParsePrimaryExpression()
    {
        return Current.Type switch
        {
            TokenType.MinusSign => ParseUnaryExpression(),
            TokenType.Number => ParseLiteral(),
            TokenType.LeftParenthesis => ParseParenthesizedExpression(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private LiteralExpression ParseLiteral()
    {
        return new LiteralExpression(ConsumeToken());
    }

    private ParenthesizedExpression ParseParenthesizedExpression()
    {
        var leftParenthesisToken = ConsumeToken();
        var expression = ParseExpression();
        var rightParenthesisToken = AssertToken(TokenType.RightParenthesis);

        return new ParenthesizedExpression(leftParenthesisToken, expression, rightParenthesisToken);
    }

    private UnaryExpression ParseUnaryExpression()
    {
        var @operator = ConsumeToken();
        var expression = ParseExpression();

        return new UnaryExpression(@operator, expression);
    }
}