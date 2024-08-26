
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System;

public class Leader : Card
{
    public Leader(string[] CardInfoArray) : base(CardInfoArray)
    {
       // type = CardTypes.Leader;
    }
}

public abstract class UnityCard : Card
{
    public List<ZoneTypes> Zone {get; private set;}
    public int Power {get; private set;}
    public string ZoneString {get; private set;}

    public UnityCard(string[] CardInfoArray) : base(CardInfoArray)
    {
        ZoneString = CardInfoArray[3];
        Zone = Utils.GetZoneTypes(CardInfoArray[3]);
        Power = int.Parse(CardInfoArray[4]);
    }

    public UnityCard(Card card) : base(card)
    {
        if (card is UnityCard unityCard)
        {
            Zone = unityCard.Zone;
            Power = unityCard.Power;
            ZoneString = unityCard.ZoneString;
        }
    }
}

 public class Hero : UnityCard
{
    public Hero(string[] CardInfoArray) : base(CardInfoArray)
    {
        //type = CardTypes.Hero;
    }
}

public class Silver : UnityCard
{
    public int initialPower {get; set;}
    public static int possibleZones = 3;
    public Silver(string[] CardInfoArray) : base(CardInfoArray)
    {
        initialPower = Power;
    }

    public Silver(Card card) : base(card)
    {
        if (card is Silver silverCard)
        {
            initialPower = silverCard.Power;
        }
    }
}

public abstract class Special : Card
{
    public Special(string[] CardInfoArray) : base(CardInfoArray)
    {

    }
}

public class Augment : Special
{
    public ZoneTypes Zone { get; private set; }
    public Augment(string[] CardInfoArray) : base(CardInfoArray)
    {
        Zone = Utils.GetZoneForSpecialCards(CardInfoArray[3]);

    }
}

public class Clearance : Special
{
    public Clearance(string[] CardInfoArray) : base(CardInfoArray)
    {

    }
}

public class WeatherCard : Special 
{
    public ZoneTypes Zone { get; private set; }

    public WeatherCard(string[] CardInfoArray) : base(CardInfoArray)
    {
        Zone = Utils.GetZoneForSpecialCards(CardInfoArray[3]);
    }
}