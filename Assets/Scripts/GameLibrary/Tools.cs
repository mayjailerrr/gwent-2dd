using System.Collections.Generic;
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

    public enum AttackType
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

    public delegate bool Effect(Context context);

    public static class Tools
    {
        static Card motherCard = new Card("", Faction.Clouds, AttackType.Unit, new List<Zone>());
       
        public static Card MotherCard
        { 
            get
            {
                return motherCard;
            }
        }

        public static string[] ZoneName = { "Weather", "Reign Augment", "Reign Melee", "Reign Distance", "Reign Siege", "Clouds Augment", "Clouds Melee", "Clouds Distance", "Clouds Siege"};
        public static Dictionary<Zone, int> IndexByZone = new Dictionary<Zone, int>
        {
                { Zone.Melee, 0 },
                { Zone.Distance, 1 },
                { Zone.Siege, 2}
        };
        
        public static Dictionary<Faction, string> FactionName = new Dictionary<Faction, string> 
        {
            { Faction.Clouds, "Clouds" },
            { Faction.Reign, "Reign" }
        };


        public static Player GetPlayerByFaction(Faction faction) => faction is Faction.Clouds ? Player.Clouds : Player.Reign;
        public static Player GetPlayerByName(string name) => name == "Clouds" ? Player.Clouds : Player.Reign;
        public static Player GetRivalByName(string name) => name == "Reign" ? Player.Clouds : Player.Reign;
        public static Player GetEnemyOf(Player player) => Player.Clouds.Equals(player) ? Player.Reign : Player.Clouds;

        public static void ShuffleList(List<Card> list, bool saveOperation = false)
        {
            List<Card> newList = new List<Card>();
            foreach (var card in list) newList.Add(card);

            int random;
            Card swapCard;

            for (int i = list.Count - 1; i >= 0; i-- )
            {
                random = (new System.Random()).Next(list.Count - 1);
                swapCard = list[random];
                list[random] = list[i];
                list[i] = swapCard;
            }

            if(saveOperation) Board.Instance.Take(new ShuffleOperation(newList, list));
        }
       
    }


}

