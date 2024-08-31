using System.Collections.Generic;
using System.Collections;
//using UnityEngine;
using GameLibrary;

namespace GameLibrary
{
    public class LureCard : Card
    {
        public LureCard(string name, Faction faction, CardType type, List<Zone> ranges, double power = 0, string effectKey = "9") : base(name, faction, type, ranges, power)
        {
             
            AllocateEffect(effectKey);
            
        }

        public override bool Effect(Context context)
        {
            try
            {
                return Effect(context.ActualPos, context.ActualPos.IndexOf(context.ActualCard));
            }
            catch (System.NullReferenceException)
            {
                return false;
            }
        }

        public bool Effect(List<Card> list, int index)
        {
            Card card = list[index];
            if (card is LureCard)
                return false;
            list[index] = this;
            Owner.Hand[Owner.Hand.IndexOf(this)] = card;
            if (card is UnityCard unit) 
                unit.InitPower();
            else if (card is ClearanceCard) 
                Owner.Battlefield.RemoveClearance(Tools.IndexByZone[Owner.ZoneByList[list]]);
            return true;

        }
    }
}

