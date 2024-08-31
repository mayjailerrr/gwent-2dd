using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Interpreter;
using GameLibrary;

abstract class Callable : Expression<object>
{
    protected Token? caller;
    protected IExpression? callee;
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
    public override ExpressionType Type => ExpressionType.Object;
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
        MethodInfo? method = null;
        Type type;
        if(callee is GameList) type = typeof(GameList);
        else if (callee is Number) type = typeof(Number);
        else if(callee is Card) type = typeof(Card);
        else if (callee is string) type = typeof(string);
        else type = typeof(object);

        try
        {
            method = type.GetMethod(caller.Value);
        }
        catch (AmbiguousMatchException)
        {
            method = type.GetMethod(caller.Value, new Type[0]);
        }

        if (method != null)
        {
            try
            {
                return method.Invoke(callee, this.arguments);
            }
            catch (IndexOutOfRangeException)
            {
                throw new RunningError($"This arguments are wrong, buddy {caller.CodeLocation.Item1},{caller.CodeLocation.Item2}");
            }
        }
        else throw new RunningError($"This method doesn't exist, buddy {caller.CodeLocation.Item1},{caller.CodeLocation.Item2}");
    }

    public override bool CheckSemantic(out List<string> errorsList) => throw new Attention($"Make the item in {caller.CodeLocation.Item1},{caller.CodeLocation.Item2 - 1} a method properly defined and later we can talk, my friend");

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

        Type type;

        if(callee is GameList) type = typeof(GameList);
        else if (callee is Number) type = typeof(Number);
        else if(callee is Card) type = typeof(Card);
        else if (callee is string) type = typeof(string);
        else type = typeof(object);

        if (type.GetProperty(caller.Value) != null)
        {
            return type.GetProperty(caller.Value).GetValue(callee);
        }
        else throw new RunningError($"This property doesn't exist, buddy {caller.CodeLocation.Item1},{caller.CodeLocation.Item2}");
    }

    public override bool CheckSemantic(out List<string> errorsList) => throw new Attention($"Make the item in {caller.CodeLocation.Item1},{caller.CodeLocation.Item2 - 1} a property properly defined and later we can talk, my friend");
}