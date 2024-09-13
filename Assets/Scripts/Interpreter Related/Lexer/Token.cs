
using Interpreterr;
using System.Collections;
using System.Collections.Generic;
public class Token
{
    public TokenType Type { get; private set; }
    public string Value { get; private set; }
    public (int, int) CodeLocation { get; private set; }

    public Token(TokenType type, string value, int line, int column)
    {
        Type = type;
        Value = value;
        CodeLocation = (line, column);
    }

    public Token(Token token, object value)
    {
        Type = token.Type;
        CodeLocation = (token.CodeLocation.Item1, token.CodeLocation.Item2);
        Value = value.ToString();
    }

    public override string ToString()
    {
        return $"{Type}: {Value} at {CodeLocation.Item1}, {CodeLocation.Item2}{(this.Type == TokenType.String? "" : $"-{CodeLocation.Item2 + Value.Length - 1}")}";
    }

    public static Dictionary<string, TokenType> allTypes = new Dictionary<string, TokenType>
    {
        //keywords
        { "card", TokenType.Card },
        { "Name", TokenType.Name },
        { "Type", TokenType.Type },
        { "Faction", TokenType.Faction },
        { "Power", TokenType.Power },
        { "Range", TokenType.Range },
        { "OnActivation", TokenType.OnActivation },
        { "effect", TokenType.Effect },
        { "Effect", TokenType.EffectParam },
        { "Selector", TokenType.Selector },
        { "Postaction", TokenType.PostAction },
        { "Source", TokenType.Source },
        { "Single", TokenType.Single },
        { "Predicate", TokenType.Predicate },
        { "Action", TokenType.Action },
        { "Params", TokenType.Params },
        { "Amount", TokenType.Amount },

        //common ones
        { "if", TokenType.If },
        { "in", TokenType.In },
        { "while", TokenType.While },
        { "for", TokenType.For },
        { "else", TokenType.Else },
        { "log", TokenType.Log },
        { "=>", TokenType.Lambda },

        //operators
        { "=", TokenType.Assign },
        { "+=", TokenType.Increase },
        { "-=", TokenType.Decrease },
        { ">", TokenType.Greater },
        { "<", TokenType.Less },
        { ">=", TokenType.GreaterEqual },
        { "<=", TokenType.LessEqual },
        { "==", TokenType.Equal },
        { "!=", TokenType.NotEqual },
        { "+", TokenType.Plus },
        { "-", TokenType.Minus },
        { "*", TokenType.Multiply },
        { "/", TokenType.Divide },
        { "^", TokenType.PowerTo },
        { "++", TokenType.IncreaseOne },
        { "--", TokenType.DecreaseOne },
        { "%", TokenType.Module },

        //punctuation
        { "{", TokenType.OpenBrace },
        { "}", TokenType.CloseBrace },
        { "[", TokenType.OpenBracket },
        { "]", TokenType.CloseBracket },
        { "(", TokenType.OpenParen },
        { ")", TokenType.CloseParen },
        { ":", TokenType.DoubleDot },
        { ".", TokenType.Dot },
        { ",", TokenType.Comma },
        { ";", TokenType.SemiColon },

        //booleans
        { "true", TokenType.True },
        { "false", TokenType.False },
        { "&&", TokenType.And },
        { "||", TokenType.Or },
        { "!", TokenType.Not },

        { "@", TokenType.JoinString },
        { "@@", TokenType.SpacedString },
        { "$", TokenType.Sign },


    };
}

public enum TokenType
{
    //keywords
    Card, Name, Type, Faction, Power, Range, OnActivation,
    Effect, EffectParam, Selector, PostAction,
    Source, Single, Predicate,
    Action, Params, Amount,

    //common ones
    Number, String, If, In, While, For, Else, Log, Lambda,

    // operators
    Assign, Increase, Decrease,
    Greater, Less, GreaterEqual, LessEqual, Equal, NotEqual,
    Plus, Minus, Multiply, Divide, PowerTo, Module,
    IncreaseOne, DecreaseOne,

    //punctuation
    OpenBrace, CloseBrace, OpenBracket, CloseBracket, OpenParen, CloseParen,
    DoubleDot, Dot, Comma, SemiColon,

    //booleans
    True, False, And, Or, Not, Sign,

    JoinString, SpacedString,
    Identifier,
    EOF,
    
}
