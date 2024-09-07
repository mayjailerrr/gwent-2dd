using System;
using System.Collections.Generic;
using System.Text;
using Interpreterr;
using GameLibrary;

class Selector : Expression<object>
{
    IExpression predicate;
    IExpression source;
    IExpression parent;
    IExpression single;
    (int, int) codeLocation;
    public override (int, int) CodeLocation { get => codeLocation; protected set => codeLocation = value; }

    public Selector(IExpression predicate, IExpression source, IExpression parent, IExpression single, (int, int) codeLocation)
    {
        this.predicate = predicate;
        this.source = source;
        this.parent = parent;
        this.single = single;
        this.codeLocation = codeLocation;
    }

    public override ExpressionType Type => ExpressionType.List;
    string pos => $"in selector :{codeLocation.Item1},{codeLocation.Item2 - 1}";

    public override bool CheckSemantic(out List<string> errorsList)
    {
        string attention = "";
        errorsList = new List<string>();

        try
        {
            if (source.Type != ExpressionType.String || source.Type != ExpressionType.Object) errorsList.Add($"This source is not correct, my bro" + pos);
            else if (!source.CheckSemantic(out List<string> temp)) errorsList.AddRange(temp);
            else if ((string)source.Interpret() == "parent" && parent is null) errorsList.Add($"There's no parent, my bro, check that out" + pos);
        }
         catch (InvalidCastException)
        {
            errorsList.Add($"The type of the source is wrong, fix it" + pos);
        }

        if (single is null) 
        {
            single = new UnaryObject(codeLocation, false);
            return true;
        }
        else if (single.Type != ExpressionType.Boolean) errorsList.Add($"The type of the single is not correct, in here {pos}");
        else if (single.Type is ExpressionType.Object) attention = ($"This single at {pos} need to be a boolean type");
        else if (!single.CheckSemantic(out List<string> temp)) errorsList.AddRange(temp);
       

        if (predicate.Type != ExpressionType.Predicate) errorsList.Add($"The type of the predicate is wrong, fix it at {pos}");
        else if (predicate.CheckSemantic(out List<string> temp)) errorsList.AddRange(temp);
        
        if (attention != "") throw new Attention(attention);
       
        return errorsList.Count == 0;
    }
    public override bool CheckSemantic(out string error)
    {
        error = "";
        try
        {
            if (source.Type != ExpressionType.String) error = $"This source is not correct, my bro" + pos;
            else if (!source.CheckSemantic(out string temp)) error = temp;
            else if ((string)source.Interpret() == "parent" && parent is null) error = $"There's no parent, my bro, check that out {pos}";

            else if (!single.CheckSemantic(out temp)) error = temp;
            else if (single.Type != ExpressionType.Boolean) error = $"The type of the single is not correct, in here {pos}";
            else if (single.Type is ExpressionType.Object) throw new Attention ($"This single {codeLocation.Item1},{codeLocation.Item2 - 1} need to be a boolean type");
            else if (single is null) 
            {
                single = new UnaryObject(codeLocation, false);
                return true;
            }

            else if (!predicate.CheckSemantic(out error)) error = temp;
            else if (predicate.Type != ExpressionType.Predicate) error = $"The type of the predicate is wrong, fix it {pos}";
        }
        catch (InvalidCastException)
        {
            error = ($"The type of the source is wrong, fix it {pos}");
        }
        return false;
    }

    public override object Interpret()
    {
        GameList list;
        switch ((string)source.Interpret())
        {
            case "parent":
                list = (GameList)parent.Interpret();
                break;                             
            case "playerDeck":
                list = GameContext.Context.PlayerDeck;
                break;
            case "oponentDeck":
                list = GameContext.Context.RivalDeck;
                break;
            case "playerField":
                list = GameContext.Context.PlayerBattlefield;
                break;
            case "oponentField":
                list = GameContext.Context.RivalBattlefield;
                break;
            case "playerHand":
                list = GameContext.Context.PlayerHand;
                break;
            case "oponentHand":
                list = GameContext.Context.RivalHand;
                break;
            case "board":
                list = GameContext.Context.Board;
                break;
            default:
                throw new RunningError($"Unexpected source {pos}");   
        }
        list = list.Find((Predicate<Card>)predicate.Interpret());

        return (bool)single.Interpret() ? new GameList(new List<Card>() { list[0] }, list[0].Owner) : list;
    }
}

