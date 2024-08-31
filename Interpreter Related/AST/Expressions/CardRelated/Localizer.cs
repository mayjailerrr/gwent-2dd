using System;
using System.Collections.Generic;
using System.Text;
using Interpreter;

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

    public override object Interpret() => ((GameList)localizer.Interpret())[(Number)localizer.Interpret()];
    public override ExpressionType Return => ExpressionType.Card;

    public override bool CheckSemantic(out string error)
    {
        error = "";

        if(localizer.Return is ExpressionType.Object)
            throw new Attention($"The operations you are trying to make on this object at {codeLocation.Item1},{codeLocation.Item2 - 1} are not allowed cuz is not a list");
        else if (!(localizer.Return is ExpressionType.List)) error += ($"The operation you are trying to make at {codeLocation.Item1},{codeLocation.Item2} is not valid my friend");

        if (index.Return is ExpressionType.Object) 
            throw new Attention($"This object need to be a number at {codeLocation.Item1},{codeLocation.Item2 - 1}");
        else if (!(index.Return is ExpressionType.Number)) error = ($"The operations you are trying to make on this object at {codeLocation.Item1},{codeLocation.Item2} are not allowed cuz is not a number");
        return error.Length == 0;
    }
    public override bool CheckSemantic(out List<string> errorsList)
    {
        errorsList = new List<string>();

        if (localizer.Return is ExpressionType.Object)
            throw new Attention($"The operations you are trying to make on this object at {codeLocation.Item1},{codeLocation.Item2 - 1} are not allowed cuz is not a list");
        else if (!(localizer.Return is ExpressionType.List))
            errorsList.Add($"The operation you are trying to make at {codeLocation.Item1},{codeLocation.Item2} is not valid my friend");
        
        if(index.Return is ExpressionType.Object)
            throw new Attention($"This object need to be a number at {codeLocation.Item1},{codeLocation.Item2 - 1}");
        else if (!(index.Return is ExpressionType.Number))
            errorsList.Add($"The operations you are trying to make on this object at {codeLocation.Item1},{codeLocation.Item2} are not allowed cuz is not a number");

        return errorsList.Count == 0;
    }
}