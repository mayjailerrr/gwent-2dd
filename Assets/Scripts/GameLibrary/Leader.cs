using System.Collections.Generic;
using System.Collections;
using GameLibrary;

namespace GameLibrary
{
    public class LeaderCard : Card
    {
        public bool Selection { get; private set; }

        public LeaderCard(string name, Faction faction, AttackType type, bool selection = false, double power = 0, Effect effect = null) : base(name, faction, type, new List<Zone>(), power, effect)
        {
            Selection = selection;
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
            if (effect is null)
            {
                return Selection 
                    ? KeepInBattle(context.ActualPlayer, context.ActualCard, context.ActualPos) 
                    : DrawCard(context.ActualPlayer);
            }

           return effect.Invoke(context);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error en la ejecución del efecto: {ex.Message}");
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
