using System;
using System.Collections.Generic;
using System.Text;

namespace Interpreter.Expressions
{
    public class UnaryExpression : Expression<object>
    {
        protected Token _operator;
        protected IExpression value;

        public UnaryExpression (Token _operator, IExpression value)
        {
            this._operator = _operator;
            this.value = value;
        }

        public override bool CheckSemantic(out List<string> errors)
        {
            errors = new List<string>();

            if(value.Return is ExpressionType.Object)
                throw new Warning($"The operations you are trying to make on this object at {Location} are not allowed");
            
            if(value.Return is ExpressionType.Number || value.Return is ExpressionType.Boolean) return true;

            errors.Add($"The operations you are trying to make on this object at {_operator.Location} are not allowed");
            return false;
        }

        public override object Interpret()
        {
        switch (_operator.Value)
        {
            case "-":
                    return new Number(-((Number)value.Interpret()).Value);
            case "!":
                    return !(bool)value.Interpret();
            default:
                    throw new Exception($"Unexpected operator at {_operator.Location}");
        }
        }

        public override ExpressionType Return => value.Return;
        public override CodeLocation Location { get => _operator.Location; protected set => throw new NotImplementedException(); }
        public override bool CheckSemantic(out string error)
        {
            error = "";

            if(value.Return is ExpressionType.Object)
                throw new Warning($"The operations you are trying to make on this object at {_operator.Location} are not allowed");

            if(value.Return is ExpressionType.Number || value.Return is ExpressionType.Boolean) return true;

            error = $"The operations you are trying to make on this object at {_operator.Location} are not allowed";
            return false;
        }
        public override string ToString() => _operator + value.ToString();
    }

}
