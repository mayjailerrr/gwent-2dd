using System.Collections.Generic;
using System.Collections;
//using UnityEngine;

namespace GameLibrary
{
    public class AugmentCard : Card
    {
        public double Bonus { get; private set; }

        public AugmentCard(string name, Faction faction, CardType type, List<Zone> ranges, double bonus = 2, string effectKey = null) : base(name, faction, type, ranges, bonus)
        {
            Bonus = bonus switch
            {
                0 => 2,
                >= 10 => bonus / 10,
                _ => bonus
            }; 
        }
    }
}

