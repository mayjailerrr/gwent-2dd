using GameLibrary;
public static class AllCards
{
    public static List<Card> cloudsCards = new List<Card>
    {
        new LeaderCard("Bran", Faction.Clouds, AttackType.Leader, false),
        new LeaderCard("Mad", Faction.Clouds, AttackType.Leader, false),

        new LureCard("Tyrion", Faction.Clouds, AttackType.Lure, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}),
        new LureCard("Margaery", Faction.Clouds, AttackType.Lure, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}),
        new LureCard("Joffrey", Faction.Clouds, AttackType.Lure, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}),

        new ClearanceCard("Sparrow", Faction.Clouds, AttackType.Clearance, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}),

        new WeatherCard("John", Faction.Clouds, AttackType.Weather, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}),
        new WeatherCard("Thormund", Faction.Clouds, AttackType.Weather, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}),
        new WeatherCard("Khal", Faction.Clouds, AttackType.Weather, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}),

        new AugmentCard("Fire", Faction.Clouds, AttackType.Augment, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}, 2),
        new AugmentCard("Huargos", Faction.Clouds, AttackType.Augment, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}, 2),
        new AugmentCard("Cersei", Faction.Clouds, AttackType.Augment, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}, 2),
        new AugmentCard("Army", Faction.Clouds, AttackType.Augment, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}, 2),
        new AugmentCard("Knights", Faction.Clouds, AttackType.Augment, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}, 2),

    
        new UnityCard("Stark", Faction.Clouds, AttackType.Unit, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}, Rank.Gold, 5, Effects.PutsAWeather),
        new UnityCard("Lannister", Faction.Clouds, AttackType.Unit, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}, Rank.Gold, 5, Effects.PutsAWeather),
        new UnityCard("Targaryen", Faction.Clouds, AttackType.Unit, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}, Rank.Gold, 5, Effects.PutsAWeather),
        new UnityCard("Baratheon", Faction.Clouds, AttackType.Unit, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}, Rank.Gold, 5, Effects.PutsAWeather),
        new UnityCard("Greyjoy", Faction.Clouds, AttackType.Unit, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}, Rank.Gold, 5, Effects.PutsAWeather),
        new UnityCard("Martell", Faction.Clouds, AttackType.Unit, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}, Rank.Gold, 5, Effects.PutsAWeather),
        new UnityCard("Tyrell", Faction.Clouds, AttackType.Unit, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}, Rank.Gold, 5, Effects.PutsAWeather),
        new UnityCard("Arryn", Faction.Clouds, AttackType.Unit, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}, Rank.Gold, 5, Effects.PutsAWeather),
        new UnityCard("Tully", Faction.Clouds, AttackType.Unit, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}, Rank.Gold, 5, Effects.PutsAWeather),
    };

    public static List<Card> reignCards = new List<Card>
    {
        new LeaderCard("King", Faction.Reign, AttackType.Leader, false),
        new LeaderCard("Robert", Faction.Reign, AttackType.Leader, false),

        new LureCard("Cersei", Faction.Reign, AttackType.Lure, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}),
        new LureCard("Jaime", Faction.Reign, AttackType.Lure, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}),
        new LureCard("Tyrion", Faction.Reign, AttackType.Lure, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}),

        new ClearanceCard("Mountain", Faction.Reign, AttackType.Clearance, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}),

        new WeatherCard("Stannis", Faction.Reign, AttackType.Weather, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}),
        new WeatherCard("Melisandre", Faction.Reign, AttackType.Weather, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}),
        new WeatherCard("Davos", Faction.Reign, AttackType.Weather, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}),

        new AugmentCard("Army", Faction.Reign, AttackType.Augment, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}, 2),
        new AugmentCard("Knights", Faction.Reign, AttackType.Augment, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}, 2),
        new AugmentCard("Huargos", Faction.Reign, AttackType.Augment, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}, 2),
        new AugmentCard("Cersei", Faction.Reign, AttackType.Augment, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}, 2),
        new AugmentCard("Fire", Faction.Reign, AttackType.Augment, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}, 2),

        new UnityCard("Stark", Faction.Reign, AttackType.Unit, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}, Rank.Gold, 5, Effects.PutsAWeather),
        new UnityCard("Lannister", Faction.Reign, AttackType.Unit, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}, Rank.Gold, 5, Effects.PutsAWeather),
        new UnityCard("Targaryen", Faction.Reign, AttackType.Unit, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}, Rank.Gold, 5, Effects.PutsAWeather),
        new UnityCard("Baratheon", Faction.Reign, AttackType.Unit, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}, Rank.Gold, 5, Effects.PutsAWeather),
        new UnityCard("Greyjoy", Faction.Reign, AttackType.Unit, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}, Rank.Gold, 5, Effects.PutsAWeather),
        new UnityCard("Martell", Faction.Reign, AttackType.Unit, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}, Rank.Gold, 5, Effects.PutsAWeather),
        new UnityCard("Tyrell", Faction.Reign, AttackType.Unit, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}, Rank.Gold, 5, Effects.PutsAWeather),
        new UnityCard("Arryn", Faction.Reign, AttackType.Unit, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}, Rank.Gold, 5, Effects.PutsAWeather),
        new UnityCard("Tully", Faction.Reign, AttackType.Unit, new List<Zone>{Zone.Melee, Zone.Distance, Zone.Siege}, Rank.Gold, 5, Effects.PutsAWeather),
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