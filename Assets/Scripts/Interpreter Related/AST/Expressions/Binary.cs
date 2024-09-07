using System.Collections.Generic;
using System.Text;
using System;


namespace Interpreterr.Expressions
{
abstract class BinaryExpression<T> : Expression<T>
{
    protected IExpression leftValue;
    protected Token _operator;
    protected IExpression rightValue;

    public BinaryExpression(IExpression leftValue, Token _operator, IExpression rightValue)
    {
        this.leftValue = leftValue;
        this._operator = _operator;
        this.rightValue = rightValue;
    }

    public override bool CheckSemantic(out string error)
    {
        error = "";

        if(!this.CheckSemantic(out List<string> errorsList))
            for (int i = 0; i < errorsList.Count; i++)
            {
                error += errorsList[i];
                if (i != errorsList.Count - 1) error += "\n";
            }
        else return true;

        return false;
    }

    public override string ToString() => leftValue.ToString() + " " + _operator + " " + rightValue.ToString();
    public override (int, int) CodeLocation { get => _operator.CodeLocation; protected set => throw new NotImplementedException();}

}

class MathExpression : BinaryExpression<Number>
{
    public MathExpression (IExpression leftValue, Token _operator, IExpression rightValue) : base(leftValue, _operator, rightValue) { }

    public override Number Accept (IVisitor<Number> visitor) => base.Accept(visitor);
    public override ExpressionType Type => ExpressionType.Number;
    public override bool CheckSemantic(out List<string> errorsList)
    {
        errorsList = new List<string>();

        if (!matches.Contains(_operator.Value)) errorsList.Add($"Very illegal operation at {_operator.CodeLocation.Item1},{_operator.CodeLocation.Item2}");
        if(leftValue.Type is ExpressionType.Object || rightValue.Type is ExpressionType.Object)
            throw new Attention($"You need to check the objects at {_operator.CodeLocation.Item1},{_operator.CodeLocation.Item2 - 1} cuz' they aren't numbers and this won't work");
        if(!(leftValue.Type is ExpressionType.Number)) errorsList.Add($"Left value is not a number at {_operator.CodeLocation.Item1},{_operator.CodeLocation.Item2}");
        if(!(rightValue.Type is ExpressionType.Number)) errorsList.Add($"Right value is not a number at {_operator.CodeLocation.Item1},{_operator.CodeLocation.Item2}");
        return errorsList.Count == 0;
    }
    static List<string> matches = new List<string> { "+", "-", "*", "/", "^" };

    public override object Interpret()
    {
        try
        {
            switch (_operator.Value)
            {
                case "+":
                    return ((Number)leftValue.Interpret()).Plus((Number)rightValue.Interpret());
                case "-":
                    return ((Number)leftValue.Interpret()).Minus((Number)rightValue.Interpret());
                case "*":
                    return ((Number)leftValue.Interpret()).Multiply((Number)rightValue.Interpret());
                case "/":
                    return ((Number)leftValue.Interpret()).Divide((Number)rightValue.Interpret());
                case "^":
                    return ((Number)leftValue.Interpret()).PowerTo((Number)rightValue.Interpret());
                default:
                    return null;
            }
        }
        catch (Exception)
        {
            throw new RunningError($"Error at {_operator.CodeLocation.Item1},{_operator.CodeLocation.Item2}. Invalid operation");

        }
    }
}

class BooleanExpression : BinaryExpression<bool>
{
    public BooleanExpression (IExpression leftValue, Token _operator, IExpression rightValue) : base(leftValue, _operator, rightValue) { }

    public override bool Accept (IVisitor<bool> visitor) => base.Accept(visitor);
    //weird boolean
    public override ExpressionType Type => ExpressionType.Boolean;
    static List<string> matches = new List<string> { "&&", "||" };
    public override bool CheckSemantic(out List<string> errorsList)
    {
        errorsList = new List<string>();

        if (!matches.Contains(_operator.Value)) errorsList.Add($"Very illegal operation at {_operator.CodeLocation.Item1},{_operator.CodeLocation.Item2}");
        if(leftValue.Type is ExpressionType.Object || rightValue.Type is ExpressionType.Object)
            throw new Attention($"You need to check the objects at {_operator.CodeLocation.Item1},{_operator.CodeLocation.Item2 - 1} cuz' they aren't booleans and this won't work");
        if(!(leftValue.Type is ExpressionType.Boolean)) errorsList.Add($"Left value is not a boolean at {_operator.CodeLocation.Item1},{_operator.CodeLocation.Item2}");
        if(!(rightValue.Type is ExpressionType.Boolean)) errorsList.Add($"Right value is not a boolean at {_operator.CodeLocation.Item1},{_operator.CodeLocation.Item2}");
        return errorsList.Count == 0;
    }

    public override object Interpret()
    {
        try
        {
            switch (_operator.Value)
            {
                case "&&":
                    return (bool)leftValue.Interpret() && (bool)rightValue.Interpret();
                case "||":
                    return (bool)leftValue.Interpret() || (bool)rightValue.Interpret();
                default:
                    return null;
            }
        }
        catch (Exception)
        {
            throw new RunningError($"Error at {_operator.CodeLocation.Item1},{_operator.CodeLocation.Item2}. Invalid operation");

        }
    }
}

class LiteralExpression : BinaryExpression<string>
{
    public LiteralExpression (IExpression leftValue, Token _operator, IExpression rightValue) : base(leftValue, _operator, rightValue) { }

    public override string Accept (IVisitor<string> visitor) => base.Accept(visitor);
    public override ExpressionType Type => ExpressionType.String;
    static List<string> matches = new List<string> { "@", "@@" };
    public override bool CheckSemantic(out List<string> errorsList)
    {
        errorsList = new List<string>();

        if (!matches.Contains(_operator.Value)) errorsList.Add($"Very illegal operation at {_operator.CodeLocation.Item1},{_operator.CodeLocation.Item2}");
        if(leftValue.Type is ExpressionType.Object || rightValue.Type is ExpressionType.Object)
            throw new Attention($"You need to check the objects at {_operator.CodeLocation.Item1},{_operator.CodeLocation.Item2 - 1} cuz' they aren't strings and this won't work");
        if(!(leftValue.Type is ExpressionType.String)) errorsList.Add($"Left value is not a string at {_operator.CodeLocation.Item1},{_operator.CodeLocation.Item2}");
        if(!(rightValue.Type is ExpressionType.String)) errorsList.Add($"Right value is not a string at {_operator.CodeLocation.Item1},{_operator.CodeLocation.Item2}");
        return errorsList.Count == 0;
    }

    public override object Interpret()
    {
        try
        {
            switch (_operator.Value)
            {
                case "@":
                    return (string)leftValue.Interpret() + (string)rightValue.Interpret();
                case "@@":
                    return (string)leftValue.Interpret() + " " + (string)rightValue.Interpret();
                default:
                    return null;
            }
        }
        catch (Exception)
        {
            throw new RunningError($"Error at {_operator.CodeLocation.Item1},{_operator.CodeLocation.Item2}. Invalid operation");

        }
    }
}

class ComparisonExpression : BinaryExpression<bool>
{
    public ComparisonExpression (IExpression leftValue, Token _operator, IExpression rightValue) : base(leftValue, _operator, rightValue) { }

    public override bool Accept (IVisitor<bool> visitor) => base.Accept(visitor);
    public override ExpressionType Type => ExpressionType.Boolean;
    static List<string> matches = new List<string> { "<", ">", "<=", ">=", "==", "!=" };
    public override bool CheckSemantic(out List<string> errorsList)
    {
        errorsList = new List<string>();

        if (!matches.Contains(_operator.Value)) errorsList.Add($"Very illegal operation at {_operator.CodeLocation.Item1},{_operator.CodeLocation.Item2}");
        if(leftValue.Type is ExpressionType.Object || rightValue.Type is ExpressionType.Object)
            throw new Attention($"You need to check the objects at {_operator.CodeLocation.Item1},{_operator.CodeLocation.Item2 - 1} cuz' they aren't numbers and this won't work");
        if(!(leftValue.Type is ExpressionType.Number)) errorsList.Add($"Left value is not a number at {_operator.CodeLocation.Item1},{_operator.CodeLocation.Item2}");
        if(!(rightValue.Type is ExpressionType.Number)) errorsList.Add($"Right value is not a number at {_operator.CodeLocation.Item1},{_operator.CodeLocation.Item2}");
        return errorsList.Count == 0;
    }

    public override object Interpret()
    {
        try
        {
            switch (_operator.Value)
            {
                case "<":
                    return ((Number)leftValue.Interpret()).Less((Number)rightValue.Interpret());
                case ">":
                    return ((Number)leftValue.Interpret()).Greater((Number)rightValue.Interpret());
                case "<=":
                    return ((Number)leftValue.Interpret()).LessEqual((Number)rightValue.Interpret());
                case ">=":
                    return ((Number)leftValue.Interpret()).GreaterEqual((Number)rightValue.Interpret());
                case "==":
                    return leftValue.Interpret().Equals(rightValue.Interpret());
                case "!=":
                    return !leftValue.Interpret().Equals(rightValue.Interpret());
                default:
                    return null;
            }
        }
        catch (Exception)
        {
            throw new RunningError($"Error at {_operator.CodeLocation.Item1},{_operator.CodeLocation.Item2}. Invalid operation");

        }
    }
}

}
