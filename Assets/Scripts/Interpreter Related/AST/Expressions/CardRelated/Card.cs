using System;
using System.Collections.Generic;
using System.Text;
using Interpreterr;
using GameLibrary;
class CardState : IStatement
{
    static List<Card> cards = new List<Card>();
    static Dictionary<string, string> declaredCards = new Dictionary<string, string>();
    public static List<Card> Cards => cards;
    IExpression name;
    IExpression power;
    List<IExpression> range;
    IExpression type;
    IExpression faction;
    OnActivation onActivation;
    (int, int) codeLocation;
    public (int, int) CodeLocation => codeLocation;
    string pos => $"at the card you are trying to declare at {codeLocation.Item1},{codeLocation.Item2}";
   

    public CardState(IExpression name, IExpression power, List<IExpression> range, IExpression type, IExpression faction, OnActivation onActivation, (int,int) codeLocation)
    {
        this.name = name;
        this.power = power;
        this.range = range;
        this.type = type;
        this.faction = faction;
        this.onActivation = onActivation;
        this.codeLocation = codeLocation;
    }

    public static void StartOver()
    {
        cards = new List<Card>();
    }

    public void RunIt()
    {
        string name = (string)this.name.Interpret();

        string _faction = (string)this.faction.Interpret();

        var factionMapping = new Dictionary<string, Faction>
        {
            { "Cloud Of Fraternity", Faction.Clouds },
            { "Reign Of Punishment", Faction.Reign }
        };

        if (!factionMapping.TryGetValue(_faction, out Faction faction))
        {
            throw new RunningError("Invalid faction");
        }

        List<Zone> zones = new List<Zone>();
        
        double power = this.power is null? 0 : ((Number)this.power.Interpret()).Value;
        foreach (var ranges in range)
        {
            switch ((string)ranges.Interpret())
            {
                case "Melee":
                    zones.Add(Zone.Melee);
                    break;
                case "Distance":
                    zones.Add(Zone.Distance);
                    break;
                case "Siege":
                    zones.Add(Zone.Siege);
                    break;
              
                default:
                    throw new RunningError("Invalid zone at:" + pos);
            }
        }
        switch ((string)type.Interpret())
        {
            case "Leader":
                cards.Add(new LeaderCard(name, faction, AttackType.Leader));
                break;
            case "Gold":
                cards.Add(new UnityCard(name, faction, AttackType.Unit, zones, Rank.Gold, power));
                break;
            case "Silver":
                cards.Add(new UnityCard(name, faction, AttackType.Unit, zones, Rank.Silver, power));
                break;
            case "Augment":
                cards.Add(new AugmentCard(name, faction, AttackType.Augment, zones, power));
                break;
            case "Weather":
                cards.Add(new WeatherCard(name, faction, AttackType.Weather, zones, power));
                break;
            case "Lure":
                cards.Add(new LureCard(name, faction,  AttackType.Lure, zones, power));
                break;
            case "Clearance":
                cards.Add(new ClearanceCard(name, faction, AttackType.Clearance, zones, power));
                break;
           
            default:
                throw new Exception("Invalid type at:" + pos);
        }
    }

    public bool CheckSemantic(out List<string> errorsList)
    {
        if (onActivation is null) errorsList = new List<string>();
        else onActivation.CheckSemantic(out errorsList);
        
        if (type.Type != ExpressionType.String)
        {
            errorsList.Add($"Expected string, found {type.Type}:" + pos);
        }
        if (name.Type != ExpressionType.String)
        {
            errorsList.Add($"Expected string, found {name.Type}" + pos);
        }
        if (faction.Type != ExpressionType.String)
        {
            errorsList.Add($"Expected string, found {faction.Type}" + pos);
        }
        if (!(power is null) && power.Type != ExpressionType.Number)
        {
            errorsList.Add($"Expected number, found {power.Type}" + pos);
        }

        for (int i = 0; i < range.Count; i++)
        {
            if (range[i].Type != ExpressionType.String)
            {
                errorsList.Add($"Expected string, found {range[i].Type}:" + pos);
            }
        }
        return errorsList.Count == 0;
    }
}

class OnActivation : IStatement
{
    List<(Activation, Activation)> effects;
    (int, int) codeLocation;
    

    public OnActivation(List<(Activation, Activation)> effects, (int, int) codeLocation)
    {
        this.effects = effects;
        this.codeLocation = codeLocation;
    }
    public (int, int) CodeLocation => codeLocation;

    public void RunIt()
    {
        foreach (var effect in effects)
        {
            effect.Item1.RunIt();
            if (effect.Item2 != null)
            {
                effect.Item2.RunIt();
            }
        }
    }
    

    public bool CheckSemantic (out List<string> errorsList)
    {
        errorsList = new List<string>();
        foreach (var effect in effects)
        {
            effect.Item1.CheckSemantic(out errorsList);
            if (effect.Item2.CodeLocation != (0,0))
            {
                effect.Item2.CheckSemantic(out List<string> temp);
                errorsList.AddRange(temp);
            }
        }
        return errorsList.Count == 0;
    } 
}

