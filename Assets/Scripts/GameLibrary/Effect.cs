using System.Collections.Generic;
using System.Collections;

namespace GameLibrary
{
    public static class Effects
    {
        static readonly Dictionary<string, Effect> effects = new()
        {
            {"1", PutsAnAugment }, 
            {"2", PutsAWeather },
            {"3", ReducePowerToAverage },
            {"4", Draw },
            {"5", DeleteWeakest },
            {"6", DeleteStrongest },
            {"7", MultiplyItself }, 
            {"8", CleanLowerZone },
            {"9", VoidEffect },
        };

        public static void AddEffect(string name, Effect effect) => effects[name] = effect;

        public static Effect GetEffect(string number) => effects.GetValueOrDefault(number, effects["9"]);

        #region 1 - Puts An Augment
        public static bool PutsAnAugment(Context context)
        {
            Player player = context.ActualPlayer;
            AugmentCard army = new("Army", Faction.Clouds, AttackType.Augment, new List<Zone> { Zone.Distance }, 1.8);
            return player.Battlefield.AllocateCard(army, player.ZoneByList[context.ActualPos]);
        }
        #endregion

        #region 2 - Puts A Weather
        public static bool PutsAWeather(Context context)
        {
            foreach (var zone in context.RivalPlayer.Battlefield.Zones)
            {
                ChangeActualPower(zone);
            }
            return true;
        }
        #endregion

        #region 3 - Reduce Power To Average
        public static bool ReducePowerToAverage(Context context)
        {
            List<UnityCard> cards = new();
            double totalPower = 0;

            foreach (var zone in context.ActualPlayer.Battlefield.Zones)
            {
                CollectCardsAndPower(zone, cards, ref totalPower);
            }

            foreach (var zone in context.RivalPlayer.Battlefield.Zones)
            {
                CollectCardsAndPower(zone, cards, ref totalPower);
            }

            double averagePower = totalPower / cards.Count;

            foreach (var card in cards)
            {
                if (card.Rank == Rank.Silver)
                    card.ChangeActualPower(averagePower, true);
            }

            return true;
        }
        #endregion

        #region 4 - Draw
        public static bool Draw(Context context) => context.ActualPlayer.GetCard();
        #endregion

        #region 5 - Delete Weakest
        public static bool DeleteWeakest(Context context)
        {
            var (weakestCard, sourceList) = context.RivalPlayer.Battlefield.HighPeekSilverCard(false);
            if (weakestCard is null) return false;

            context.RivalPlayer.Battlefield.SendToGraveyard(weakestCard, sourceList);
            return true;
        }
        #endregion

        #region 6 - Delete Strongest
        public static bool DeleteStrongest(Context context)
        {
            var actualPlayerStrongest = context.ActualPlayer.Battlefield.HighPeekSilverCard(true);
            var rivalPlayerStrongest = context.RivalPlayer.Battlefield.HighPeekSilverCard(true);

            var (strongestCard, sourceList) = actualPlayerStrongest.Item1 is null || 
                                              actualPlayerStrongest.Item1.InitialPower < rivalPlayerStrongest.Item1?.InitialPower
                                              ? rivalPlayerStrongest 
                                              : actualPlayerStrongest;

            if (strongestCard is null) return false;

            context.RivalPlayer.Battlefield.SendToGraveyard(strongestCard, sourceList);
            return true;
        }
        #endregion

        #region 7 - Multiply Itself
        public static bool MultiplyItself(Context context)
        {
            UnityCard card = (UnityCard)context.ActualCard;
            List<UnityCard> matchingCards = new();
            int multiplier = 0;

            foreach (var zone in context.ActualPlayer.Battlefield.Zones)
            {
                CountMatchingCards(zone, card, matchingCards, ref multiplier);
            }

            foreach (var matchingCard in matchingCards)
            {
                matchingCard.ChangeActualPower(matchingCard.ActualPower * multiplier, true);
            }

            return true;
        }
        #endregion

        #region 8 - Clean Lower Zone
        public static bool CleanLowerZone(Context context)
        {
            try
            {
                int minCount = int.MaxValue;
                List<Card>? targetZone = null;
                Card augment = Tools.MotherCard;
                Player targetPlayer = context.RivalPlayer;

                foreach (var zone in context.ActualPlayer.Battlefield.Zones)
                {
                    if (FindMinCountZone(zone, ref minCount, ref targetZone, context.ActualPlayer))
                    {
                        augment = context.ActualPlayer.Battlefield.Augment[Tools.IndexByZone[context.ActualPlayer.ZoneByList[zone]]];
                        targetPlayer = context.ActualPlayer;
                    }
                }

                foreach (var zone in context.RivalPlayer.Battlefield.Zones)
                {
                    if (FindMinCountZone(zone, ref minCount, ref targetZone, context.RivalPlayer))
                    {
                        augment = context.RivalPlayer.Battlefield.Augment[Tools.IndexByZone[context.RivalPlayer.ZoneByList[zone]]];
                        targetPlayer = context.RivalPlayer;
                    }
                }

                if (targetZone is null) return false;

                targetPlayer.Battlefield.SendToGraveyard(augment, targetPlayer.Battlefield.Augment);
                targetPlayer.Battlefield.SendToGraveyard(targetZone);

                return true;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        #endregion

        #region 9 - Void Effect
        public static bool VoidEffect(Context context) => true;
        #endregion

        #region Tools
        static bool FindMinCountZone(List<Card> zone, ref int minCount, ref List<Card>? targetZone, Player player)
        {
            int cardCount = zone.Count(card => !card.Equals(Tools.MotherCard));
            if (!player.Battlefield.Augment[Tools.IndexByZone[player.ZoneByList[zone]]].Equals(Tools.MotherCard))
                cardCount++;

            if (cardCount > 0 && cardCount < minCount)
            {
                minCount = cardCount;
                targetZone = zone;
                return true;
            }

            return false;
        }

        static void CountMatchingCards(List<Card> zone, UnityCard card, List<UnityCard> matchingCards, ref int count)
        {
            foreach (var zCard in zone)
            {
                if (zCard.Equals(card))
                {
                    count++;
                    matchingCards.Add((UnityCard)zCard);
                }
            }
        }

        static void ChangeActualPower(List<Card> zone, int reduction = 1)
        {
            foreach (var card in zone)
            {
                if (card is UnityCard unityCard && unityCard.Rank == Rank.Silver)
                {
                    unityCard.ChangeActualPower(unityCard.ActualPower - reduction, true);
                }
            }
        }

        static void CollectCardsAndPower(List<Card> zone, List<UnityCard> cards, ref double totalPower)
        {
            foreach (var card in zone)
            {
                if (card is UnityCard unityCard)
                {
                    cards.Add(unityCard);
                    totalPower += unityCard.ActualPower;
                }
            }
        }
        #endregion
    }
}
