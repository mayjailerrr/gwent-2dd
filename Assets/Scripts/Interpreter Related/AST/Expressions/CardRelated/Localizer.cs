using System;
using System.Collections.Generic;
using System.Text;
using Interpreterr;
using GameLibrary;

class Localizer : Expression<object>
{
    (int, int) codeLocation;
    IExpression localizer;
    IExpression index;
    public override (int, int) CodeLocation {get => codeLocation; protected set => codeLocation = value;}

    public Localizer(IExpression localizer, IExpression index, (int, int) codeLocation)
    {
        this.localizer = localizer;
        this.index = index;
        this.codeLocation = codeLocation;
    }

    public override object Interpret()
    {
        object result = ((GameList)localizer.Interpret())[(Number)index.Interpret()];

        if (result is double || result is int)
            return new Number(Convert.ToDouble(result));
        else if (result is string other) 
            return new OwnValue(other);
        else return result;
    }

    public void ImportValue(IExpression expr)
    {
        ((GameList)localizer.Interpret())[(Number)index.Interpret()] = (Card)expr.Interpret();
    }

    public override ExpressionType Category => ExpressionType.Card;

    public override bool CheckSemantic(out string error)
    {
        error = "";

        if(localizer.Category is ExpressionType.Object)
            throw new Attention($"The operations you are trying to make on this object at {codeLocation.Item1},{codeLocation.Item2 - 1} are not allowed cuz is not a list");
        else if (!(localizer.Category is ExpressionType.List)) error += ($"The operation you are trying to make at {codeLocation.Item1},{codeLocation.Item2} is not valid my friend");

        if (index.Category is ExpressionType.Object) 
            throw new Attention($"This object need to be a number at {codeLocation.Item1},{codeLocation.Item2 - 1}");
        else if (!(index.Category is ExpressionType.Number)) error = ($"The operations you are trying to make on this object at {codeLocation.Item1},{codeLocation.Item2} are not allowed cuz is not a number");
        return error.Length == 0;
    }
    public override bool CheckSemantic(out List<string> errorsList)
    {
        errorsList = new List<string>();

        if (localizer.Category is ExpressionType.Object)
            throw new Attention($"The operations you are trying to make on this object at {codeLocation.Item1},{codeLocation.Item2 - 1} are not allowed cuz is not a list");
        else if (!(localizer.Category is ExpressionType.List))
            errorsList.Add($"The operation you are trying to make at {codeLocation.Item1},{codeLocation.Item2} is not valid my friend");
        
        if(index.Category is ExpressionType.Object)
            throw new Attention($"This object need to be a number at {codeLocation.Item1},{codeLocation.Item2 - 1}");
        else if (!(index.Category is ExpressionType.Number))
            errorsList.Add($"The operations you are trying to make on this object at {codeLocation.Item1},{codeLocation.Item2} are not allowed cuz is not a number");

        return errorsList.Count == 0;
    }
}

class LocalizerChanger : IStatement
{
    Localizer localizer;
    Token operation;
    IExpression value;

    public LocalizerChanger(Localizer localizer, Token operation, IExpression value = null)
    {
        this.localizer = localizer;
        this.operation = operation;
        this.value = value;
    }

    public (int, int) CodeLocation => operation.CodeLocation;

    public bool CheckSemantic(out List<string> errorsList)
    {
        errorsList = new List<string>();
        string attention = "";

        try
        {
            localizer.CheckSemantic(out errorsList);
        }
        catch (Attention a)
        {
            attention += a.Message + '\n';
        }

        if (operation.Value != "=")
            errorsList.Add($"Illegal operation in {operation.CodeLocation.Item1},{operation.CodeLocation.Item2} (=)");
        if (value is null) 
            errorsList.Add($"Illegal operation in {operation.CodeLocation.Item1},{operation.CodeLocation.Item2} (no value)");

        if (value.Category is ExpressionType.Object)
            attention += $"This object need to be a card in {value.CodeLocation.Item1},{value.CodeLocation.Item2 - 1}";
        else if (value.Category != ExpressionType.Card)
            errorsList.Add($"Illegal operation cuz the value is not a card in {operation.CodeLocation.Item1},{operation.CodeLocation.Item2}");

        try 
        {
            if (!value.CheckSemantic(out string error))
                errorsList.Add(error);
        }
        catch (Attention a)
        {
            attention += a.Message;
        }

        if (attention.Length > 0) 
            throw new Attention(attention);
        else return errorsList.Count != 0;
    }

    public void RunIt()
    {
        localizer.ImportValue(value);
    }
}
