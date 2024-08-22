using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Interpreter;


    class UnaryOperation : Expression<object>
    {
        protected Token _operator;
        protected IExpression value;

        public UnaryOperation (Token _operator, IExpression value)
        {
            this._operator = _operator;
            this.value = value;
        }

        public override bool CheckSemantic(out List<string> errors)
        {
            errors = new List<string>();

            if(value.Return is ExpressionType.Object)
                throw new Attention($"The operations you are trying to make on this object at {Location} are not allowed");
            
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
        public override (int, int) CodeLocation { get => _operator.CodeLocation; protected set => throw new NotImplementedException(); }
        public override bool CheckSemantic(out string error)
        {
            error = "";

            if(value.Return is ExpressionType.Object)
                throw new Attention($"The operations you are trying to make on this object at {_operator.Location} are not allowed");

            if(value.Return is ExpressionType.Number || value.Return is ExpressionType.Boolean) return true;

            error = $"The operations you are trying to make on this object at {_operator.Location} are not allowed";
            return false;
        }
        public override string ToString() => _operator + value.ToString();
    }

    abstract class UnaryExpression<T> : Expression<T>
    {
        public override CodeLocation Location { get; protected set; }
        protected T value;

        public override bool CheckSemantic(out List<string> errors)
        {
            errors = new List<string>();

            return true;
        }
        public override bool CheckSemantic(out string error)
        {
            error = "";

            return true;
        }

        public override string ToString() => value.ToString();
    }

    class UnaryValue : UnaryExpression<Token>
    {
        public UnaryValue(Token value)
        {
            this.value = value;
            this.Location = Location;
        }

        public override ExpressionType Return
        {
            get
            {
                switch (value.Type)
                {
                    case TokenType.Number:
                        return ExpressionType.Number;
                    case TokenType.String:
                        return ExpressionType.String;
                    case TokenType.True:
                        return ExpressionType.Boolean;
                    case TokenType.False:
                        return ExpressionType.Boolean;
                    default:
                        throw new Exception($"Unexpected token type at {Location}");
                }
            }
        }

    public override object Interpret()
    {
        //check string later
        switch (value.Type)
        {
            case TokenType.Number:
                return new Number(double.Parse(value.Value));
            case TokenType.String:
                return new String(value.Value);
            case TokenType.True:
                return true;
            case TokenType.False:
                return false;
            default:
                throw new Exception($"Unexpected token type at {Location}");
        }
    }
}

class UnaryEnunciation : UnaryExpression<Enunciation>
{
    public UnaryEnunciation(Enunciation value)
    {
        this.value = value;
        this.Location = value.Location;
    }

    public override ExpressionType Return => value.Return;

    public override object Interpret()
    {
        return value.ValueGiver().Interpret();
    }
}

class UnaryCallable : UnaryExpression<Callable>
{
    public UnaryCallable(Callable value)
    {
        this.value = value;
        this.Location = value.Location;
    }

    public override object Interpret() => value.Interpret();
    public override ExpressionType Return => value.Return;
}

class UnaryObject : UnaryExpression<object>
{
    public UnaryObject(object value, CodeLocation Location)
    {
        this.value = value;
        this.Location = Location;
    }
    public override object Interpret() => value;
    public override ExpressionType Return => (value is int || value is double) ? ExpressionType.Number : 
                                            value is string ? ExpressionType.String :
                                            value is GameList ? ExpressionType.List :
                                            value is Card ? ExpressionType.Card : ExpressionType.Object;
}


