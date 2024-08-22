namespace Interpreter
{
    public interface IStatement
    {
        bool CheckSemantic(out List<string> errors);
        (int, int) CodeLocation { get; }
        void RunIt();
    }

    class Declaration : IStatement
    {
        Context context;
        IExpression value;
        Token variable;
        Token operation;
        

        public Declaration(Token variable, Context context = null, Token operation = null, IExpression value = null)
        {
            this.variable = variable;
            this.operation = operation;
            this.value = value;
            this.context = context ?? Context.Global;
            this.RunIt();
        }

        public CodeLocation Location => operation is null? variable.Location : operation.Location;
        public ExpressionType Return => value is null? context[variable.Value].Return : value.Return;
        //to check
        public override string ToString()
        {
            return variable.Value + " " + operation.Value + " " + value;
        }
        public bool CheckSemantic(out List<string> errors)
        {
            errors = new List<string>();
            if (!(value is null) && !value.CheckSemantic(out string error))
            {
                errors.Add(error);
                return false;
            }
            else if (!(context[variable.Value] is null) && !value.CheckSemantic(out error))
            {
                errors.Add(error);
                return false;
            }
            return true;
        }
        public IExpression ValueGiver()
        {
            RunIt();
            return context[variable.Value];
        }

        public void RunIt()
        {
            try
            {
                //check this!!!
                switch (operation.Type)
                {
                    case TokenType.Assign:
                        context.Set(variable, value);
                        break;
                    case TokenType.Plus:
                        context.Set(variable, new BinaryExpression(context[variable.Value], value, TokenType.Plus));
                        break;
                    case TokenType.Minus:
                        context.Set(variable, new BinaryExpression(context[variable.Value], value, TokenType.Minus));
                        break;
                    default:
                        throw new ParsingError("Invalid Declaration at " + variable.Location);
                }
            }
            catch (NullReferenceException)
            {
                //if the variable is not declared
            }
        }

    }
}
    