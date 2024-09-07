using System.Collections.Generic;
using System.Collections;
//using UnityEngine;
using System;
using GameLibrary;

namespace GameLibrary
{
    public interface IEffect
    {
        bool Effect(Context context);
    }

    public interface IPlayer
    {
        Player Owner { get; }
    }

    
    public class Card : IEffect, IPlayer
    {
        public string Name { get; }
        public Faction Factions { get; }
        public AttackType AttackType { get; }
        protected double initialPower;
        protected Effect effect;
        public List<Zone> Ranges { get; }
        public List<Card> ActualPosition { get; private set; }
       
       
        public double Power 
        {
            get => this is UnityCard unit ? unit.ActualPower : initialPower;
            set 
            {
                if (this is UnityCard unit && unit.Rank is Rank.Silver) unit.ChangeActualPower(value);
            }
        }

        public string Faction => Tools.FactionName[Factions];

        public string Type
        {
            get
            {
                switch (AttackType)
                {
                    case AttackType.Clearance : return "Clearance";
                    case AttackType.Weather : return "Weather";
                    case AttackType.Leader : return "Leader";
                    case AttackType.Unit : return ((UnityCard)this).Rank is Rank.Silver ? "Silver" : "Gold";
                    case AttackType.Augment : return "Augment";
                    case AttackType.Lure : return "Lure";
                  
                    default: return "";
                }
            }
        }

        public Player Owner => Tools.GetPlayerByFaction(Factions);

        public double InitialPower { get => initialPower; }

        public Card(string name, Faction faction, AttackType type, List<Zone> ranges, double power = 0, Effect effect = null)
        {
            this.Name = name;
            this.Factions = faction;
            this.AttackType = type;
            this.Ranges = ranges;
            initialPower = power;
            AllocateEffect(effect);
        }

        public void AllocatePosition(List<Card> actualPosition) => this.ActualPosition = actualPosition is null ? Owner.Hand : actualPosition;
        public void AllocateEffect(Effect effect) => this.effect = effect is null ? Effects.VoidEffect : effect;
       public override bool Equals(object obj)
        {
            if (obj is not Card card) return false;

            return Name == card.Name
                && AttackType == card.AttackType
                && Faction == card.Faction
                && (ActualPosition?.Equals(card.ActualPosition) ?? true);
        }



        public override int GetHashCode()
        {
            return HashCode.Combine<string, string, AttackType>(Name, Faction, AttackType);
        }

        public override string ToString()
        {
            return Name + " (" + Faction + ")";
        }

        public virtual bool Effect(Context context)
        {
            try
            {
                return effect is null ? true : effect.Invoke(context);
            }

            catch
            {
                return false;
            }
        }
    
    }
}


