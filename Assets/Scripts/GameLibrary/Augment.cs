using System.Collections.Generic;
using System.Collections;
//using UnityEngine;

namespace GameLibrary
{
    public class AugmentCard : Card
    {
        public double Bonus { get; private set; }

        public AugmentCard(string name, Faction faction, AttackType type, List<Zone> ranges, double bonus, Effect effect = null) : base(name, faction, type, ranges, bonus, effect)
        {
            Bonus = bonus;
        }
    }
}

