using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Interpreter;
using GameLibrary;


    class UnaryOperation : Expression<object>
    {
        protected Token _operator;
        protected IExpression value;

        public UnaryOperation(Token _operator, IExpression value)
        {
            this._operator = _operator;
            this.value = value;
        }

        public override bool CheckSemantic(out List<string> errorsList)
        {
            errorsList = new List<string>();

            if(value.Return is ExpressionType.Object)
                throw new Attention($"The operations you are trying to make on this object at {_operator.CodeLocation.Item1},{_operator.CodeLocation.Item2} are not allowed");
            
            if(value.Return is ExpressionType.Number || value.Return is ExpressionType.Boolean) 
                return true;

            errorsList.Add($"The operations you are trying to make on this object at {_operator.CodeLocation.Item1},{_operator.CodeLocation.Item2} are not allowed");
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
                    throw new Exception($"Unexpected operator at {_operator.CodeLocation.Item1},{_operator.CodeLocation.Item2}");
        }
        }

        public override ExpressionType Return => value.Return;
        public override (int, int) CodeLocation { get => _operator.CodeLocation; protected set => throw new NotImplementedException(); }
        public override bool CheckSemantic(out string error)
        {
            error = "";

            if(value.Return is ExpressionType.Object)
                throw new Attention($"The operations you are trying to make on this object at {_operator.CodeLocation.Item1},{_operator.CodeLocation.Item2} are not allowed");

            if(value.Return is ExpressionType.Number || value.Return is ExpressionType.Boolean) return true;

            error = $"The operations you are trying to make on this object at {_operator.CodeLocation.Item1},{_operator.CodeLocation.Item2} are not allowed";
            return false;
        }
        public override string ToString() => _operator + value.ToString();
    }

    abstract class UnaryExpression<T> : Expression<T>
    {
        public override (int, int) CodeLocation { get; protected set; }
        protected T? value;
        public override string ToString() => value.ToString();
        public override bool CheckSemantic(out string error)
        {
            error = "";

            return true;
        }
        public override bool CheckSemantic(out List<string> errorsList)
        {
            errorsList = new List<string>();

            return true;
        }
    }

    class UnaryCallable : UnaryExpression<Callable>
    {
        public override object Interpret() => value.Interpret();
        public override ExpressionType Return => value.Return;
        public UnaryCallable( Callable value)
        {
            this.value = value;
            this.CodeLocation = value.CodeLocation;
        }
    }

    class UnaryDeclaration : UnaryExpression<Declaration>
    {
        public override ExpressionType Return => value.Return;

        public override object Interpret()
        {
            return value.ValueGiver().Interpret();
        }
        public UnaryDeclaration(Declaration value)
        {
            this.value = value;
            this.CodeLocation = value.CodeLocation;
        }

       
    }

    class UnaryObject : UnaryExpression<object>
    {
        public override ExpressionType Return => (value is int || value is double) ? ExpressionType.Number : 
                                                value is string ? ExpressionType.String :
                                                value is GameList ? ExpressionType.List :
                                                value is Card ? ExpressionType.Card : ExpressionType.Object;
        
        public override object Interpret() => value;
        public UnaryObject((int, int) codeLocation, object value)
        {
            this.value = value;
            this.CodeLocation = codeLocation;
        }
    }

    class UnaryValue : UnaryExpression<Token>
    {
        public override object Interpret()
        {
            switch (value.Type)
            {
                case TokenType.True:
                    return true;
                case TokenType.False:
                    return false;
                case TokenType.Number:
                    return new Number(Convert.ToDouble(value.Value));
                case TokenType.String:
                    return value.Value.Substring(1, value.Value.Length -2);
               
                default:
                    throw new Exception($"Unexpected token type at {value.CodeLocation.Item1},{value.CodeLocation.Item2 + value.Value.Length + 1}");
            }
        }
         public override ExpressionType Return
        {
            get
            {
                switch (value.Type)
                {
                     case TokenType.True:
                        return ExpressionType.Boolean;
                    case TokenType.False:
                        return ExpressionType.Boolean;
                    case TokenType.Number:
                        return ExpressionType.Number;
                    case TokenType.String:
                        return ExpressionType.String;
                   
                    default:
                        throw new Exception($"Unexpected token type at {value.CodeLocation.Item1},{value.CodeLocation.Item2 + value.Value.Length + 1}");
                }
            }
        }
        public UnaryValue(Token value)
        {
            this.value = value;
            this.CodeLocation = CodeLocation;
        }

      
}








