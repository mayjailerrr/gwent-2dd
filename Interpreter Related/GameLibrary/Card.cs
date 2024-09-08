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
        public Faction Faction { get; }
        public CardType CardType { get; }
        public List<Zone> Ranges { get; }
        public List<Card> ActualPosition { get; private set; }
        //protected Effect effect;
        protected Func<Context, bool> effectDelegate;
        protected double initialPower;
        public int Power 
        {
            get => this is UnityCard unit ? Convert.ToInt32(unit.ActualPower) : Convert.ToInt32(initialPower);
            set 
            {
                if (this is UnityCard unit) unit.ChangeActualPower(value);
            }
        }

        public Player Owner => Tools.FactionPerPlayer[Faction];

        public void AllocatePosition(List<Card> actualPosition) => this.ActualPosition = actualPosition;
    
        public override bool Equals(object obj)
        {
            return obj is Card card && this.Name == card.Name;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public Card(string name, Faction faction, CardType type, List<Zone> ranges, double power = 0, string effectKey = null)
        {
            this.Name = name;
            this.Faction = faction;
            this.CardType = type;
            this.Ranges = ranges;
            initialPower = power;
            AllocateEffect(effectKey);
        }
        public virtual bool Effect(Context context)
        {
            return effectDelegate?.Invoke(context) ?? true;
        }

        public void AllocateEffect(string effectKey)
        {
            if (string.IsNullOrEmpty(effectKey))
            {
                effectDelegate = Effects.VoidEffect;
            }
            else
            { 
                var effect = Effects.GetEffect(effectKey);
               
                if (effect != null) effectDelegate = effect.Invoke; 
                else effectDelegate = Effects.VoidEffect; 

            }
        }
    }
}


