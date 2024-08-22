using System;
using System.Collections.Generic;
using System.Text;
using Interpreter;

//COMMON CONDITIONALS STATEMENTS
//IF
class If : IStatement
{
    (int, int) codeLocation;
    IExpression conditional;
    IStatement? elseContent;
    IStatement content;

    public (int, int) CodeLocation => codeLocation;

    public If(IExpression conditional, IStatement content, (int, int) codeLocation, IStatement? elseContent = null)
    {
        this.conditional = conditional;
        this.content = content;
        this.elseContent = elseContent;
        this.codeLocation = codeLocation;
    }

    public bool CheckSemantic(out List<string> errorsList)
    {
        bool valid = true;

        valid = content.CheckSemantic(out errorsList);
        if (!(elseContent is null)) { valid = elseContent.CheckSemantic(out List<string> temperrorsList) && valid; errorsList.AddRange(temperrorsList); }

        if(!(conditional.Return is ExpressionType.Boolean))
        {
            errorsList.Add($" This expression you have at {codeLocation.Item1},{codeLocation.Item2} need to be boolean, buddy");
            return false;
        }
        else if (!conditional.CheckSemantic(out string error))
        {
            errorsList.Add(error);
            return false;
        }

        return valid;
    }
     public void RunIt()
    {
        if (conditional.Interpret() is bool conditionalValue && conditionalValue) content.RunIt();
        else if (!(elseContent is null)) elseContent.RunIt();
    }
}

//FOR
class For : IStatement
{
    public (int, int) CodeLocation => (token.CodeLocation.Item1, token.CodeLocation.Item2 - 3);
    Environment environment;
    Token token;
    IExpression collection;
    IStatement content;

    public For(Token token, IExpression collection, Environment environment, IStatement content)
    {
        this.token = token;
        this.collection = collection;
        this.environment = environment;
        this.content = content;
    }

     public bool CheckSemantic(out List<string> errorsList)
    {
        bool valid = true;

        valid = content.CheckSemantic(out errorsList) && valid;

        if (!collection.CheckSemantic(out string error))
        {
            errorsList.Add(error);
            return false;
        }
        if (!(collection.Return is ExpressionType.List)) throw new Attention($"You must make sure object at {token.CodeLocation.Item1},{token.CodeLocation.Item2 + token.Value.Length + 4} is a list or a compile time error may occur");

        return valid;
    }

    public void RunIt()
    {
        IEnumerator<object> collection;
        try
        {
            collection = ((IEnumerable<object>)this.collection.Interpret()).GetEnumerator();
        }
        catch (InvalidCastException)
        {
            throw new RunningError($"This expression at {token.CodeLocation.Item1},{token.CodeLocation.Item2 + token.Value.Length + 2}: need to be a collection for the for statement to work");
        }

        if (environment.Contains(token.Value)) throw new RunningError($"Wrong for Statement at {token.CodeLocation.Item1},{token.CodeLocation.Item2} that's in use already");

        while (collection.MoveNext())
        {
            environment.Set(new UnaryObject((-1,-1), collection.Current), token);
            content.RunIt();
        }
    }
}

class While : IStatement
{
    IStatement content;
    IExpression conditional;
    (int, int) codeLocation;

    public (int, int) CodeLocation => codeLocation;


    public While(IExpression conditional, IStatement content, (int, int) codeLocation)
    {
         this.content = content;
        this.conditional = conditional;
        this.codeLocation = codeLocation;
    }

     public bool CheckSemantic(out List<string> errorsList)
    {
        bool valid = true;

        valid = content.CheckSemantic(out errorsList);

        if (!(conditional.Return is ExpressionType.Boolean))
        {
            errorsList.Add($"The expression you wrote at {codeLocation.Item1},{codeLocation.Item2 + 2} need to be boolean, buddy");
            return false;
        }
        else if (!conditional.CheckSemantic(out string error))
        {
            errorsList.Add(error);
            return false;
        }

        return valid;
    }

    public void RunIt()
    {
        try
        {
            while ((bool)conditional.Interpret()) content.RunIt();
        }
        catch (InvalidCastException)
        {
            throw new RunningError("I cannot turn a conditional into a boolean expression, we don't do that in here.");
        }
    }

   
}
