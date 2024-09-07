using System;
using System.Collections.Generic;
using System.Text;
using Interpreterr;

class Activation : IStatement
{
    IExpression effect;
    EffectState statement;
    IExpression selector;
    List<(Token, IExpression)> _params;
    (int, int) codeLocation;
    public (int, int) CodeLocation => codeLocation;

    public Activation(IExpression effect, IExpression selector, List<(Token, IExpression)> @params, (int, int) codeLocation)
    {
        this.effect = effect;
        this.selector = selector;
        this.codeLocation = codeLocation;
        _params = @params;
    }

    private string ErrorsCheck(IExpression expression, List<string> errors)
    {
        try
        {
            if (!expression.CheckSemantic(out string error))
            {
                errors.Add(error);
            }
        }
        catch (Attention a)
        {
            return a.Message + "\n";
        }
        return "";
    }

    public bool CheckSemantic(out List<string> errorsList)
    {
        errorsList = new List<string>();

        string attention = "";

        attention += ErrorsCheck(effect, errorsList);

        if (effect.Type != ExpressionType.String)
        {
            errorsList.Add($"The effect you have at {codeLocation.Item1},{codeLocation.Item2} need to be a string, buddy");
        }

        attention += ErrorsCheck(selector, errorsList);

        if (!(selector is null) && selector.Type != ExpressionType.List)
        {
            errorsList.Add($"The selector you have at {codeLocation.Item1},{codeLocation.Item2} is wrong, my bro");
        }

        foreach (var unit in _params)
        {
            attention += ErrorsCheck(unit.Item2, errorsList);
        }

        try
        {
            statement = EffectState.DeclaredEffects[(string)effect.Interpret()];     //todo: figure out the value managing
            statement.Take(_params, selector);
        }
        catch (KeyNotFoundException)
        {
            errorsList.Add($"The effect {codeLocation.Item1},{codeLocation.Item2} is not defined");
        }
        catch (RunningError e)
        {
            errorsList.Add(e.Message);
        }

        if (attention != "")
            throw new Attention(attention);

        return errorsList.Count == 0;
    }

    public void RunIt()
    {
        statement.RunIt();
    }
}