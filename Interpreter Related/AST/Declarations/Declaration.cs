using System;
using System.Collections.Generic;
using System.Text;
using Interpreter.Expressions;
using Interpreter;

public interface IStatement
{
    bool CheckSemantic(out List<string> errorsList);
    (int, int) CodeLocation { get; }
    void RunIt();
}

class Declaration : IStatement
{
    Environment environment;
    IExpression? value;
    Token token;
    Token operation;
    

    public Declaration(Token token, Environment environment = null, Token operation = null, IExpression value = null)
    {
        this.environment = environment ?? Environment.Global;
        this.value = value;
        this.token = token;
        this.operation = operation;
       
        this.RunIt();
    }

    public (int, int) CodeLocation => operation is null? token.CodeLocation : operation.CodeLocation;
    public ExpressionType Return => value is null? environment[token.Value].Return : value.Return;
    //to check
    public override string ToString()
    {
        return token.Value + " " + operation.Value + " " + value;
    }
    public bool CheckSemantic(out List<string> errorsList)
    {
        errorsList = new List<string>();
        if (!(value is null) && !value.CheckSemantic(out string error))
        {
            errorsList.Add(error);
            return false;
        }
        else if (!(environment[token.Value] is null) && !value.CheckSemantic(out error))
        {
            errorsList.Add(error);
            return false;
        }
        return true;
    }
    public IExpression ValueGiver()
    {
        RunIt();
        return environment[token.Value];
    }

    public void RunIt()
    {
        try
        {
        
            switch (operation.Type)
            {
                case TokenType.IncreaseOne:
                    environment.Set(new MathExpression(environment[token.Value], new Token(operation, "+"), new UnaryValue(new Token(TokenType.Number, "1", operation.CodeLocation.Item1, operation.CodeLocation.Item2))), token);
                    break;
                case TokenType.DecreaseOne:
                    environment.Set(new MathExpression(environment[token.Value], new Token(operation, "-"), new UnaryValue(new Token(TokenType.Number, "1", operation.CodeLocation.Item1, operation.CodeLocation.Item2))), token);
                    break;
                case TokenType.Assign:
                    environment.Set(value, token);
                    break;
                case TokenType.Increase:
                    environment.Set(new MathExpression(environment[token.Value], new Token(TokenType.Minus, "+", operation.CodeLocation.Item1, operation.CodeLocation.Item2), value), token);
                    break;
                case TokenType.Decrease:
                    environment.Set(new MathExpression(environment[token.Value], new Token(TokenType.Minus, "-", operation.CodeLocation.Item1, operation.CodeLocation.Item2), value), token);
                    break;
                
                default:
                    throw new ParsingError($"Invalid Declaration at {token.CodeLocation.Item1},{token.CodeLocation.Item2}");
            }
        }
        catch (NullReferenceException)
        {
        
        }
    }

}

    