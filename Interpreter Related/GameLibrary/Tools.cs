using System.Collections.Generic;
//using Unity.VisualScripting;
using System.Diagnostics;
using System;

namespace GameLibrary
{
    public enum Zone
    {
        Melee,
        Distance,
        Siege
    }

    public enum CardType
    {
        Leader,
        Unit,
        Augment,
        Weather,
        Clearance,
        Lure
    }

    public enum Rank
    {
        Gold,
        Silver
    }

    public enum Faction 
    {
        Clouds,
        Reign
    }
    public static class Tools
    {
        static Card baseCard = new Card("", Faction.Clouds, CardType.Unit, new List<Zone>());
        public static Card MotherCard { get => baseCard; }
        public static Player GetRivalByName(string name) => name == "Reign" ? Player.Clouds : Player.Reign;
        public static Dictionary<Faction, Player> FactionPerPlayer = new Dictionary<Faction, Player>
        {
                { Faction.Clouds, Player.Clouds },
                { Faction.Reign, Player.Reign }
        };

        public static string[] ZoneName = { "Weather", "Reign Augment", "Reign Melee", "Reign Distance", "Reign Siege", "Clouds Augment", "Clouds Melee", "Clouds Distance", "Clouds Siege"};
        public static Dictionary<Zone, int> IndexByZone = new Dictionary<Zone, int>
        {
                { Zone.Melee, 0 },
                { Zone.Distance, 1 },
                { Zone.Siege, 2}
        };

        public static Player GetPlayerByName(string name) => name == "Clouds" ? Player.Clouds : Player.Reign;
     
    }

}

