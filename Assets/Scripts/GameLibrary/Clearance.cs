using System.Collections.Generic;
using System.Collections;

namespace GameLibrary
{
    public class ClearanceCard : Card
    {
        public ClearanceCard(string name, Faction faction, AttackType type, List<Zone> ranges, double power = 0, Effect effect = null) : base(name, faction, type, ranges, power, effect)
        {
           
        }
    }
}

