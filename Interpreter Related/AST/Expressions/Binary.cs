using System;
using System.Collections.Generic;
using System.Text;

namespace Interpreter.Expressions
{
    public abstract class BinaryExpression<T> : Expression<T>
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

        if(!this.CheckSemantic(out List<string> errors))
            for (int i = 0; i < errors.Count; i++)
            {
                error += errors[i];
                if (i != errors.Count - 1) error += "\n";
            }
        else return true;

        return false;
    }

    public override string ToString() => leftValue.ToString() + " " + _operator + " " + rightValue.ToString();
    public override CodeLocation Location { get => _operator.Location; protected set => throw new NotImplementedException();}

}

class ArithmeticExpression : BinaryExpression<Number>
{
    public ArithmeticExpression (IExpression leftValue, Token _operator, IExpression rightValue) : base(leftValue, _operator, rightValue) { }

    public override Number Accept (IVisitor<Number> visitor) => base.Accept(visitor);
    public override ExpressionType Return => ExpressionType.Number;
    public override bool CheckSemantic(out List<string> errors)
    {
        errors = new List<string>();

        if (!possibleOperations.Contains(_operator.Value)) errors.Add($"Very illegal operation at {_operator.Location}");
        if(leftValue.Return is ExpressionType.Object || rightValue.Return is ExpressionType.Object)
            throw new Warning($"You need to check the objects at {_operator.Location} cuz' they aren't numbers and this won't work");
        if(!(leftValue.Return is ExpressionType.Number)) errors.Add($"Left value is not a number at {_operator.Location}");
        if(!(rightValue.Return is ExpressionType.Number)) errors.Add($"Right value is not a number at {_operator.Location}");
        return errors.Count == 0;
    }
    static List<string> possibleOperations = new List<string> { "+", "-", "*", "/", "^" };

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
                    return ((Number)leftValue.Interpret()).Pow((Number)rightValue.Interpret());
                default:
                    return null;
            }
        }
        catch (Exception)
        {
            throw new InterpretationError($"Error at {_operator.Location}. Invalid operation");

        }
    }
}

class BooleanExpression : BinaryExpression<bool>
{
    public BooleanExpression (IExpression leftValue, Token _operator, IExpression rightValue) : base(leftValue, _operator, rightValue) { }

    public override bool Accept (IVisitor<bool> visitor) => base.Accept(visitor);
    //weird boolean
    public override ExpressionType Return => ExpressionType.Boolean;
    static List<string> possibleOperations = new List<string> { "&&", "||" };
    public override bool CheckSemantic(out List<string> errors)
    {
        errors = new List<string>();

        if (!possibleOperations.Contains(_operator.Value)) errors.Add($"Very illegal operation at {_operator.Location}");
        if(leftValue.Return is ExpressionType.Object || rightValue.Return is ExpressionType.Object)
            throw new Warning($"You need to check the objects at {_operator.Location} cuz' they aren't booleans and this won't work");
        if(!(leftValue.Return is ExpressionType.Boolean)) errors.Add($"Left value is not a boolean at {_operator.Location}");
        if(!(rightValue.Return is ExpressionType.Boolean)) errors.Add($"Right value is not a boolean at {_operator.Location}");
        return errors.Count == 0;
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
            throw new InterpretationError($"Error at {_operator.Location}. Invalid operation");

        }
    }
}

class StringExpression : BinaryExpression<string>
{
    public StringExpression (IExpression leftValue, Token _operator, IExpression rightValue) : base(leftValue, _operator, rightValue) { }

    public override string Accept (IVisitor<string> visitor) => base.Accept(visitor);
    public override ExpressionType Return => ExpressionType.String;
    static List<string> possibleOperations = new List<string> { "@", "@@" };
    public override bool CheckSemantic(out List<string> errors)
    {
        errors = new List<string>();

        if (!possibleOperations.Contains(_operator.Value)) errors.Add($"Very illegal operation at {_operator.Location}");
        if(leftValue.Return is ExpressionType.Object || rightValue.Return is ExpressionType.Object)
            throw new Warning($"You need to check the objects at {_operator.Location} cuz' they aren't strings and this won't work");
        if(!(leftValue.Return is ExpressionType.String)) errors.Add($"Left value is not a string at {_operator.Location}");
        if(!(rightValue.Return is ExpressionType.String)) errors.Add($"Right value is not a string at {_operator.Location}");
        return errors.Count == 0;
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
            throw new InterpretationError($"Error at {_operator.Location}. Invalid operation");

        }
    }
}

class ComparisonExpression : BinaryExpression<bool>
{
    public ComparisonExpression (IExpression leftValue, Token _operator, IExpression rightValue) : base(leftValue, _operator, rightValue) { }

    public override bool Accept (IVisitor<bool> visitor) => base.Accept(visitor);
    public override ExpressionType Return => ExpressionType.Boolean;
    static List<string> possibleOperations = new List<string> { "<", ">", "<=", ">=", "==", "!=" };
    public override bool CheckSemantic(out List<string> errors)
    {
        errors = new List<string>();

        if (!possibleOperations.Contains(_operator.Value)) errors.Add($"Very illegal operation at {_operator.Location}");
        if(leftValue.Return is ExpressionType.Object || rightValue.Return is ExpressionType.Object)
            throw new Warning($"You need to check the objects at {_operator.Location} cuz' they aren't numbers and this won't work");
        if(!(leftValue.Return is ExpressionType.Number)) errors.Add($"Left value is not a number at {_operator.Location}");
        if(!(rightValue.Return is ExpressionType.Number)) errors.Add($"Right value is not a number at {_operator.Location}");
        return errors.Count == 0;
    }

    public override object Interpret()
    {
        try
        {
            switch (_operator.Value)
            {
                case "<":
                    return ((Number)leftValue.Interpret()).LessThan((Number)rightValue.Interpret());
                case ">":
                    return ((Number)leftValue.Interpret()).GreaterThan((Number)rightValue.Interpret());
                case "<=":
                    return ((Number)leftValue.Interpret()).LessOrEqual((Number)rightValue.Interpret());
                case ">=":
                    return ((Number)leftValue.Interpret()).GreaterOrEqual((Number)rightValue.Interpret());
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
            throw new InterpretationError($"Error at {_operator.Location}. Invalid operation");

        }
    }
}

}
