using GameLibrary;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
using System;


public static class CardsWarehouse
{
    public static List<Card> cloudsCards = new List<Card>
    {
        new LeaderCard("Bran", Faction.Clouds, AttackType.Leader, false),
        new LeaderCard("Mad", Faction.Clouds, AttackType.Leader, false),

        new LureCard("Tyrion", Faction.Clouds, AttackType.Lure, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}),
        new LureCard("Margaery", Faction.Clouds, AttackType.Lure, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}),
        new LureCard("Joffrey", Faction.Clouds, AttackType.Lure, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}),

        new ClearanceCard("Sparrow", Faction.Clouds, AttackType.Clearance, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}),

        new WeatherCard("John", Faction.Clouds, AttackType.Weather, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}), //5
        new WeatherCard("Thormund", Faction.Clouds, AttackType.Weather, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}),  //4
        new WeatherCard("Khal", Faction.Clouds, AttackType.Weather, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}),  //5

        new AugmentCard("Fire", Faction.Clouds, AttackType.Augment, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}, 2),
        new AugmentCard("Huargos", Faction.Clouds, AttackType.Augment, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}, 2),
        new AugmentCard("Cersei", Faction.Clouds, AttackType.Augment, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}, 2),
        new AugmentCard("Army", Faction.Clouds, AttackType.Augment, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}, 5),
        new AugmentCard("Knights", Faction.Clouds, AttackType.Augment, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}, 4),
        new AugmentCard("Wildlings", Faction.Clouds, AttackType.Augment, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}, 4),

    
        new UnityCard("Arya", Faction.Clouds, AttackType.Unit, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}, Rank.Gold, 8, Effects.ReducePowerToAverage),
        new UnityCard("Jaime", Faction.Clouds, AttackType.Unit, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}, Rank.Silver, 7, Effects.MultiplyItself),
        new UnityCard("Brienne", Faction.Clouds, AttackType.Unit, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}, Rank.Silver, 6),
        new UnityCard("Melisandre", Faction.Clouds, AttackType.Unit, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}, Rank.Gold, 7, Effects.PutsAWeather),
        new UnityCard("Dragonglass", Faction.Clouds, AttackType.Unit, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}, Rank.Silver, 6),
        new UnityCard("Drogon", Faction.Clouds, AttackType.Unit, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}, Rank.Silver, 5),
        new UnityCard("Gigants", Faction.Clouds, AttackType.Unit, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}, Rank.Gold, 8, Effects.Draw),
        new UnityCard("Dragonstone", Faction.Clouds, AttackType.Unit, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}, Rank.Silver, 6),
        new UnityCard("Ariete", Faction.Clouds, AttackType.Unit, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}, Rank.Silver, 3),
    };

    public static List<Card> reignCards = new List<Card>
    {
        new LeaderCard("King", Faction.Reign, AttackType.Leader, false),
        new LeaderCard("Robert", Faction.Reign, AttackType.Leader, false),

        new LureCard("Tyrion", Faction.Clouds, AttackType.Lure, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}),
        new LureCard("Margaery", Faction.Clouds, AttackType.Lure, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}),
        new LureCard("Joffrey", Faction.Clouds, AttackType.Lure, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}),

        new ClearanceCard("Sparrow", Faction.Clouds, AttackType.Clearance, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}),

        new WeatherCard("John", Faction.Clouds, AttackType.Weather, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}), //5
        new WeatherCard("Thormund", Faction.Clouds, AttackType.Weather, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}),  //4
        new WeatherCard("Khal", Faction.Clouds, AttackType.Weather, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}),  //5

        new AugmentCard("Fire", Faction.Clouds, AttackType.Augment, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}, 2),
        new AugmentCard("Huargos", Faction.Clouds, AttackType.Augment, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}, 2),
        new AugmentCard("Cersei", Faction.Clouds, AttackType.Augment, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}, 2),
        new AugmentCard("Army", Faction.Clouds, AttackType.Augment, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}, 5),
        new AugmentCard("Knights", Faction.Clouds, AttackType.Augment, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}, 4),
        new AugmentCard("Wildlings", Faction.Clouds, AttackType.Augment, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}, 4),

        new UnityCard("Ramsay", Faction.Clouds, AttackType.Unit, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}, Rank.Gold, 7, Effects.CleanLowerZone),
        new UnityCard("Hound", Faction.Clouds, AttackType.Unit, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}, Rank.Silver, 6),
        new UnityCard("Greyjoy", Faction.Clouds, AttackType.Unit, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}, Rank.Silver, 5),
        new UnityCard("Viserion", Faction.Clouds, AttackType.Unit, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}, Rank.Gold, 9, Effects.DeleteStrongest),
        new UnityCard("Rhaegal", Faction.Clouds, AttackType.Unit, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}, Rank.Silver, 7, Effects.PutsAnAugment),
        new UnityCard("Ygritte", Faction.Clouds, AttackType.Unit, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}, Rank.Silver, 5),
        new UnityCard("RedKeep", Faction.Clouds, AttackType.Unit, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}, Rank.Gold, 8, Effects.DeleteWeakest),
        new UnityCard("Ships", Faction.Clouds, AttackType.Unit, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}, Rank.Silver, 4),
        new UnityCard("Catapult", Faction.Clouds, AttackType.Unit, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}, Rank.Silver, 3),
    };

    static List<Card> FullDeck(List<Card> cards)
    {
        List<Card> temp = new List<Card>();
        List<LeaderCard> leaders = new List<LeaderCard>();

        foreach (var card in cards)
        {
            if (card is UnityCard unityCard && unityCard.Rank is Rank.Silver)
            {
                temp.AddRange(Enumerable.Repeat(unityCard, 2));

            }
            else if (card is LeaderCard leaderCard)
            {
                leaders.Add(leaderCard);
            }
        }

        temp.AddRange(cards);
        foreach (var leader in leaders)
        {
            temp.Remove(leader);
        }
        return temp;
    }

    public static List<Card> GetDeck(string name) => name == "Reign" ? FullDeck(reignCards) : FullDeck(cloudsCards);
}