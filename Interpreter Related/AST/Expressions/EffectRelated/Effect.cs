using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Mail;
using System.Text;
using Interpreter;
using GameLibrary;

class EffectState : IStatement
{
    IExpression name;
    IStatement action;
    static Dictionary<string, EffectState> effects = new Dictionary<string, EffectState>();
    static Dictionary<string, string> declaredEffects =  new Dictionary<string, string>();
    public static Dictionary<string, EffectState> Effects => effects;
    public static Dictionary<string, string> DeclaredEffects => declaredEffects;
    Dictionary<string, ExpressionType> _parameters;
    (int, int) codeLocation;
    public string Code { get; }
    public (int, int) CodeLocation => codeLocation;
    Token context = new Token(TokenType.Identifier, "context", 0,0);
    Token targets = new Token(TokenType.Identifier, "targets", 0,0);
    Environment environment;
    bool taken = false;
   
    public EffectState(string effectCode, IExpression name, IStatement action, List<(Token, Token)> prms, Environment environment, (int, int) codeLocation, Token targets, Token contxt)
    {
        Code = effectCode;
        this.name = name;
        this.action = action;
        this._parameters = new Dictionary<string, ExpressionType>();
        this.targets = targets is null? this.targets : targets;
        this.context = context is null ? this.context : context;
        this.environment = environment;
        this.codeLocation = codeLocation;

        foreach (var parameters in prms)
        {
            ExpressionType temp = ExpressionType.Object;
            string type = parameters.Item2.Value.ToString();

            switch (type)
            {
                case "Boolean":
                    temp = ExpressionType.Boolean;
                    break;
                case "String":
                    temp = ExpressionType.String;
                    break;
                case "Number":
                    temp = ExpressionType.Number;
                    break;
                case "List":
                    temp = ExpressionType.List;
                    break;
                case "Card":
                    temp = ExpressionType.Card;
                    break;
                case "Object":
                    break;
               
                default:
                    throw new ParsingError($"Invalid type {type} at {parameters.Item1.CodeLocation}");
            }
            _parameters.Add(parameters.Item1.Value, temp);
            environment.Set(null, parameters.Item1);
        }
    }

    public static void StartOver()
    {
        effects = new Dictionary<string, EffectState>();
        declaredEffects = new Dictionary<string, string>();
    }
    public bool CheckSemantic(out List<string> errors)
    {
        errors = new List<string>();
        string name = "";
        string attention = "";

        try
        {
             if (this.name.Type == ExpressionType.String)
            {
                if (!this.name.CheckSemantic(out string error))
                {
                    errors.Add(error);
                }

                name = (string)this.name.Interpret();
               
                if (effects.ContainsKey(name))
                {
                    attention += $"Effect {name} already declared: {codeLocation.Item1},{codeLocation.Item2}";
                }
            }
            else errors.Add($"The name of the effect is not a name");
        }
        catch (ParsingError p)
        {
            errors.Add(p.Message);
        }


        try
        {
              if (!action.CheckSemantic(out List<string> temp))
            {
                errors.AddRange(temp);
            }
        }
        catch (Attention a)
        {
            attention += a.Message;
        }

        if (errors.Count == 0)
        {
            if (!effects.ContainsKey(name))
            {
                effects.Add(name, this);
                declaredEffects.Add(name, Code);
            }
            else
            {
                effects[name] = this;
                declaredEffects[name] = Code;
            }
            if (attention != "") throw new Attention(attention);
            else return true;
        }
        else return false;;

    }
    public void RunIt()
    {
        if (!taken) throw new RunningError($"Effect {name} with wrong parameters declared and cannot run it effectivelly");
        else action.RunIt();
    }

    public void Take(List<(Token, IExpression)> _parameters, IExpression targets)
    {
        if(this.context.Value != "context") environment.Set(GameContext.Context, this.context);

        environment.Set(targets, this.targets);

        string attentions = "";
        string errors = "";

        foreach (var parameter in _parameters)
        {
            try
            {
                if(this._parameters[parameter.Item1.Value] is ExpressionType.Object)
                {
                    attentions += $"Parameter {parameter.Item1.Value} is not required\n";
                }
                else if (!(parameter.Item2.Type is ExpressionType.Object || parameter.Item2.Type == this._parameters[parameter.Item1.Value]))
                {
                    errors += $"Expected {this._parameters[parameter.Item1.Value]}, found {parameter.Item2.Type}\n";
                } 
            }
            catch (KeyNotFoundException)
            {
                errors += $"Parameter {parameter.Item1.Value} not declared\n";
            }
            environment.Set(parameter.Item2, parameter.Item1);
        }

        if (errors != "")
        {
            throw new ParsingError(errors);
        }
        if (attentions != "")
        {
            throw new Attention(attentions);
        }
         taken = true;
    }
}

