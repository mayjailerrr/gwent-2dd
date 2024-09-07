using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Interpreterr;
using GameLibrary;

class Predicate : Expression<object>
{
    Environment environment;
    Token token;
    IExpression expression;
    public Predicate(Environment environment, Token token, IExpression expression)
    {
        this.environment = environment;
        this.token = token;
        this.expression = expression;
    }
    bool Interpret(Card card)
    {
        environment?.Set(new UnaryObject((-1,-1), card), token);
        return (bool)expression.Interpret();
    }

    public override ExpressionType Type => ExpressionType.Predicate;
    public override (int, int) CodeLocation { get => (token.CodeLocation.Item1, token.CodeLocation.Item2 + token.Value.Length + 2) ; protected set => throw new NotImplementedException(); }


    public override object Interpret() => new Predicate<Card>(Interpret);

    public override bool CheckSemantic(out string error)
    {
        error = "";
        if (!(expression.Type is ExpressionType.Boolean))
        {
            error = $"The expression at the right {token.CodeLocation.Item1},{token.CodeLocation.Item2 + token.Value.Length} is not a boolean, check it";
        }
        if (expression.Type is ExpressionType.Object)
        {
            throw new Attention($"The expression at the right {token.CodeLocation.Item1},{token.CodeLocation.Item2 + token.Value.Length + 2} is an object, check it");
        }
        else return true;
    
    }
    public override bool CheckSemantic(out List<string> errorsList)
    {
        errorsList = new List<string>();

        if(!(expression.Type is ExpressionType.Boolean))
        {
             errorsList.Add($"The expression at the right {token.CodeLocation.Item1},{token.CodeLocation.Item2 + token.Value.Length} is not boolean, check it");
        }
        if(expression.Type is ExpressionType.Object)
           throw new Attention($"The expression at the right {token.CodeLocation.Item1},{token.CodeLocation.Item2 + token.Value.Length + 2} is not boolean, check it");
        else return true;

        
    }
}