using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

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
                if(motherCard.Info is null) motherCard.AllocateInfo(new VisualInfo(Resources.Load<Material>("MotherCardSelected"), null, "base"));
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

    public class VisualInfo
    {
        public Sprite Main { get; private set; }
        public Material Material { get; private set; }
        public Sprite Info { get; private set; }

        public VisualInfo(Material material, Sprite info, string faction)
        {
            if (faction != "base" && (material is null || info is null)) this.Take(GetRandomInfo(faction));
            else 
            {
                this.Material= material;
                this.Info = info;
            }
        }

        public VisualInfo(Sprite main, Sprite info, string faction)
        {
            if (main is null || info is null) this.Take(GetRandomInfo(faction));
            else 
            {
                Main = main;
                Info = info;
            }
        }
        public VisualInfo(Sprite main, string faction)
        {
            if (main is null) this.Take(GetRandomInfo(faction));
            else 
            {
                Main = Info = main;
            }
        }

        static VisualInfo GetRandomInfo(string faction) => new VisualInfo(Resources.Load<Sprite>("Random/" + faction + "/" + Random.Range(1, 6)), faction);

        void Take(VisualInfo visualInfo)
        {
            this.Material = visualInfo.Material;
            this.Main = visualInfo.Main;
            this.Info = visualInfo.Info;
        }
    }


}

