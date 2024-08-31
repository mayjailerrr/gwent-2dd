using System.Collections.Generic;
using System.Collections;
//using UnityEngine;

namespace GameLibrary
{
    public class ClearanceCard : Card
    {
        public ClearanceCard(string name, Faction faction, CardType type, List<Zone> ranges, double power = 0, string effectKey = null) : base(name, faction, type, ranges, power)
        {
             if (!string.IsNullOrEmpty(effectKey))
            {
                AllocateEffect(effectKey);
            }
        }
    }
}

