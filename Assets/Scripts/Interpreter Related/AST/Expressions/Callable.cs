using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Interpreterr;
using GameLibrary;

abstract class Callable : Expression<object>
{
    public Token caller;
    public IExpression callee;
    public override (int, int) CodeLocation { get => caller.CodeLocation; protected set => throw new NotImplementedException(); }

    public override bool CheckSemantic(out string error)
    {
        error = "";

        if (!this.CheckSemantic(out List<string> errorsList))
            for (int i = 0; i < errorsList.Count; i++)
            {
                error += errorsList[i];
                if (i != errorsList.Count - 1) error += "\n";
            }
        else return true;
        return false;
    }
    public override ExpressionType Category => ExpressionType.Object;
}

class Methods : Callable
{
    public IExpression[] arguments;
    public Methods(Token caller, IExpression callee, IExpression[] arguments = null)
    {
        this.arguments = arguments;
        this.caller = caller;
        this.callee = callee;
    }

    public override object Interpret()
    {
        object callee = this.callee.Interpret();
        object[] arguments = new object[this.arguments is null? 0 : this.arguments.Length];

        for (int i = 0; i < arguments.Length; i++)
        {
            arguments[i] = this.arguments[i].Interpret();
        }

    
        System.Type type;
        if(callee is GameList) type = typeof(GameList);
        else if (callee is Number) type = typeof(Number);
        else if(callee is Card) type = typeof(Card);
        else if (callee is OwnValue) type = typeof(OwnValue);
        else if (callee is GameContext) type = typeof(GameContext);
        else type = typeof(object);

        MethodInfo method = null;

        try
        {
            method = type.GetMethod(caller.Value);
        }
        catch (AmbiguousMatchException)
        {
            method = type.GetMethod(caller.Value, new System.Type[0]);
        }

        if (method is null)
            throw new RunningError($"This method does not exit {caller.Value} in {caller.CodeLocation.Item1},{caller.CodeLocation.Item2}");
        else
        {
            try
            {
                if (method.ReturnType != typeof(void))
                {
                    object result = method.Invoke(callee, arguments);

                    if (result is double || result is int)
                        return new Number(Convert.ToDouble(result));
                    else if (result is string other) 
                        return new OwnValue(other);
                    else return result;
                }
                else method.Invoke(callee, this.arguments);

                return null;
            }
            catch (ArgumentException)
            {
                throw new RunningError($"This arguments are wrong, buddy {caller.CodeLocation.Item1},{caller.CodeLocation.Item2}");
            }
        }
    }

    public override bool CheckSemantic(out List<string> errorsList)
    {
        errorsList = new List<string>();

        string attention = $"This object does not contains the called method in {caller.CodeLocation.Item1},{caller.CodeLocation.Item2 - 1}\n";

        try 
        {
            callee.CheckSemantic(out errorsList);
        }
        catch (Attention)
        {

        }

        if (!(arguments is null))
            foreach (var arg in arguments)
            {
                try
                {
                    if (!arg.CheckSemantic(out string error))
                        errorsList.Add(error);
                }
                catch (Attention a)
                {
                    attention += a.Message + '\n';
                }
            }
        if (errorsList.Count > 0) 
            return false;
        else throw new Attention(attention);
    }

    public override string ToString() => caller + callee.ToString();
}

class Property : Callable
{
    public Property(Token caller, IExpression callee)
    {
        this.caller = caller;
        this.callee = callee;
    }

    public override object Interpret()
    {
        object callee = this.callee.Interpret();

        System.Type type;

        if(callee is GameList) type = typeof(GameList);
        else if (callee is Number) type = typeof(Number);
        else if(callee is Card) type = typeof(Card);
        else if (callee is OwnValue) type = typeof(OwnValue);
        else if (callee is GameContext) type = typeof(GameContext);
        else type = typeof(object);

        if (type.GetProperty(caller.Value) != null)
        {
            object result = type.GetProperty(caller.Value).GetValue(callee);
           
            if (result is double || result is int)
            return new Number(Convert.ToDouble(result));
            else if (result is string other) 
                return new OwnValue(other);
            else return result;
        }
        else throw new RunningError($"This property doesn't exist, buddy {caller.CodeLocation.Item1},{caller.CodeLocation.Item2}");
    }

    public override bool CheckSemantic(out List<string> errorsList) => throw new Attention($"Make the item in {caller.CodeLocation.Item1},{caller.CodeLocation.Item2 - 1} a property properly defined and later we can talk, my friend");
}

class PropertyChanger : IStatement
{
    Property property;
    Token operation;
    IExpression value;

    static List<TokenType> operations = new List<TokenType> 
    {
        TokenType.Assign, 
        TokenType.Increase, 
        TokenType.Decrease,
        TokenType.IncreaseOne,
        TokenType.DecreaseOne
    };

    public PropertyChanger(Property property, Token operation, IExpression value = null)
    {
        this.property = property;
        this.operation = operation;
        this.value = value;
    }

    public (int, int) CodeLocation => operation.CodeLocation;

    public bool CheckSemantic(out List<string> errorsList)
    {
        errorsList = new List<string>();
        if (!(value is null)) 
            value.CheckSemantic(out errorsList);
        if (!operations.Contains(operation.Type))
            errorsList.Add($"Incorrect declaration in {operation.CodeLocation.Item1},{operation.CodeLocation.Item2}");
        
        if (errorsList.Count > 0)
            return false;
        else return property.CheckSemantic(out string temp);
    }

     public void RunIt()
    {
        object callee = property.callee.Interpret();

        System.Type type;

        if(callee is GameList) type = typeof(GameList);
        else if (callee is Number) type = typeof(Number);
        else if(callee is Card) type = typeof(Card);
        else if (callee is OwnValue) type = typeof(OwnValue);
        else if (callee is GameContext) type = typeof(GameContext);
        else type = typeof(object);

        if (type.GetProperty(property.caller.Value) != null)
        {
            object value = this.value is null? null : this.value.Interpret();

            try
            {
                 switch (operation.Type)
                {
                    case TokenType.Assign:
                        type.GetProperty(property.caller.Value).SetValue(callee, value);
                        break;
                    case TokenType.Increase:
                        type.GetProperty(property.caller.Value).SetValue(callee, ((Number)property.Interpret()).Plus((Number)value).Value);
                        break;
                    case TokenType.Decrease:
                        type.GetProperty(property.caller.Value).SetValue(callee, ((Number)property.Interpret()).Minus((Number)value).Value);
                        break;
                    case TokenType.IncreaseOne:
                        type.GetProperty(property.caller.Value).SetValue(callee, ((Number)property.Interpret()).Plus(new Number(1)).Value);
                        break;
                    case TokenType.DecreaseOne:
                        type.GetProperty(property.caller.Value).SetValue(callee, ((Number)property.Interpret()).Minus(new Number(1)).Value);
                        break;
                    default:
                        throw new RunningError($"Incorrect declaration in {operation.CodeLocation.Item1},{operation.CodeLocation.Item2}");
                }
            }
            catch (InvalidCastException)
            {
                throw new RunningError($"This is not a number {type.GetProperty(property.caller.Value).Name}");
            }  
        }
        else throw new RunningError($"This property doesn't exist, buddy {CodeLocation.Item1},{CodeLocation.Item2}");
    }
}