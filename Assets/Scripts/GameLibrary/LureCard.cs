using System.Collections.Generic;
using System.Collections;
using GameLibrary;


    public class LureCard : Card
    {
        public LureCard(string name, Faction faction, AttackType type, List<Zone> ranges, double power = 0, Effect effect = null) : base(name, faction, type, ranges, power, effect)
        {
             
            
        }

        public override bool Effect(Context context)
        {
            bool valid = false;

            try
            {
                valid = Effect(context.ActualPos, context.ActualPos.IndexOf(context.ActualCard)) & effect.Invoke(context);
            }
            catch (System.NullReferenceException)
            {
                valid = false;
            }
            Board.Instance.UpdateTotalScore();
            Board.Instance.Turn = true;
            return valid;
        }

        public bool Effect(List<Card> list, int index)
        {
            Card card = list[index];
            card.AllocatePosition(Owner.Hand);
            this.AllocatePosition(list);
            if (card is UnityCard unit) unit.InitPower();
            else if (card is ClearanceCard) Owner.Battlefield.RemoveClearance(Tools.IndexByZone[Owner.ZoneByList[list]]);
            Board.Instance.Take(new LureOperation(this, card, list, index));
            if (card is LureCard) return false;
            Owner.Hand[Owner.Hand.IndexOf(this)] = card;
            list[index] = this;
          
            return true;

        }
    }


