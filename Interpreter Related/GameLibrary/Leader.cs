using System.Collections.Generic;
using System.Collections;
//using UnityEngine;
using GameLibrary;

namespace GameLibrary
{
    public class LeaderCard : Card
    {
        public bool Selection { get; private set; }

        public LeaderCard(string name, Faction faction, CardType type, List<Zone> ranges, double power = 0, string effectKey = null, bool selection = false) : base(name, faction, type, new List<Zone>(), power)
        {
            Selection = selection;

            if (!string.IsNullOrEmpty(effectKey))
            {
                AllocateEffect(effectKey);
            }
        }

        private bool DrawCard(Player player)
        {
            if (player.LeaderEffectUsed || this.Selection || !player.GetCard()) 
                return false;
            
            player.LeaderEffectUsed = true;
            return true;
        }

        public override bool Effect(Context context)
        {
            try
            {

                bool effectResult = Selection ? KeepInBattle(context.ActualPlayer, context.ActualCard, context.ActualPos) : DrawCard(context.ActualPlayer);
                return effectResult;

            }
            catch
            {
                return false;
            }
        }

        private bool KeepInBattle(Player player, Card card, List<Card> list)
        {
            if (player.LeaderEffectUsed || !this.Selection || !player.Battlefield.RestAtBattleChanger(card, list))
                return false;

            player.LeaderEffectUsed = true;
            return true;
        }
    }
}
