using System.Collections.Generic;
using System.Collections;
//using UnityEngine;


namespace GameLibrary
{
    public class UnityCard : Card
    {
        double actualPower = 0;
        double powerCount = 0;

        public Rank Rank { get; private set; }
        public double InitialPower { get => initialPower; }
        public double ActualPower { get => actualPower; }
        public double Powerr { get => powerCount; set => powerCount = value; } 

        public UnityCard(string name, Faction faction, CardType type, List<Zone> ranges, Rank level, double power = 0, string effectKey = null) : base(name, faction, type, ranges, power)
        {
            this.Rank = level;
            this.actualPower = this.powerCount = initialPower;
           
            if (!string.IsNullOrEmpty(effectKey))
            {
                AllocateEffect(effectKey);
            }
        }

          public void InitPower()
        {
            this.actualPower = this.initialPower;
            ResetPower();
        }
        public void ResetPower()
        {
            this.Powerr = this.actualPower;
        }

        public void ChangeActualPower(double newPower, bool changed = true)
        {
            if (changed)
            {
                double adjustedPower = newPower - (this.actualPower - this.Power);
                this.Powerr = adjustedPower > 0 ? adjustedPower : 0;
                this.actualPower = newPower;

            }
        }
    }
}
