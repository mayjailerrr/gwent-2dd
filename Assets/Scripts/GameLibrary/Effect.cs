using System.Collections.Generic;
using System.Collections;
using GameLibrary;
using UnityEngine;


namespace GameLibrary
{
    public static class Effects
    {

        static Dictionary<string, Effect> effects = new Dictionary<string, Effect>
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

        public static void AddEffect(string name, Effect effect) => effects.Add(name, effect);

        public static Effect GetEffect(string number) => effects.ContainsKey(number) ? effects[number] : effects["9"];



        #region 1
        public static bool PutsAnAugment(Context context)
        {
            List<Card> actualPos = context.ActualPos;        
            Player player = context.ActualPlayer;

            AugmentCard army = new AugmentCard("Army", Faction.Clouds, AttackType.Augment, new List<Zone> { Zone.Distance }, 1.8);
            
            return player.Battlefield.AllocateCard(army, player.ZoneByList[actualPos]);
        }
        #endregion



        #region 2
        public static bool PutsAWeather(Context context)
        {
            Battlefield enemy = context.RivalPlayer.Battlefield;

            for (int i = 0; i < enemy.Zones.Length; i++)
            {
                ChangeActualPower(enemy.Zones[i]);
            }
            return true;
        }
        #endregion



        #region 3
        public static bool ReducePowerToAverage(Context context)
        {
            List<UnityCard> list = new List<UnityCard>();
            double originalPower = 0;
            double average = 0;

            for (int i = 0; i < context.ActualPlayer.Battlefield.Zones.Length; i++)
            {
                KeepCardsAndCountPower(context.ActualPlayer.Battlefield.Zones[i], list, ref originalPower);
                KeepCardsAndCountPower(context.RivalPlayer.Battlefield.Zones[i], list, ref originalPower);
            }

            average = originalPower / list.Count;

            foreach (UnityCard card in list)
            {
                if (card.Rank == Rank.Silver)
                    card.ChangeActualPower(average, true);
            }
            return true;
        }
        #endregion


        #region 4
        public static bool Draw(Context context) => context.ActualPlayer.GetCard();
    
        #endregion



        #region 5
        public static bool DeleteWeakest(Context context)
        {
            (Card, List<Card>) weakest = context.RivalPlayer.Battlefield.HighPeekSilverCard(false);
            if (weakest.Item1 == null) 
                return false;

            context.RivalPlayer.Battlefield.SendToGraveyard(weakest.Item1, weakest.Item2);
            return true;
        }
        #endregion



        #region 6
        public static bool DeleteStrongest(Context context)
        {
            (UnityCard, List<Card>) actualPlayerList = context.ActualPlayer.Battlefield.HighPeekSilverCard(true);
            (UnityCard, List<Card>) rivalPlayerList = context.RivalPlayer.Battlefield.HighPeekSilverCard(true);

            if (actualPlayerList.Item1 == null && rivalPlayerList.Item1 == null) 
                return false;
            
            if(actualPlayerList.Item1 == null || actualPlayerList.Item1.InitialPower < rivalPlayerList.Item1.InitialPower)
            {
                context.RivalPlayer.Battlefield.SendToGraveyard(rivalPlayerList.Item1, rivalPlayerList.Item2);
                return true;
            }

            else if (rivalPlayerList.Item1 == null || actualPlayerList.Item1.InitialPower > rivalPlayerList.Item1.InitialPower)
            {
                context.ActualPlayer.Battlefield.SendToGraveyard(actualPlayerList.Item1, actualPlayerList.Item2);
            }
else if (actualPlayerList.Item1.InitialPower == rivalPlayerList.Item1.InitialPower)
            {
                context.ActualPlayer.Battlefield.SendToGraveyard(actualPlayerList.Item1, actualPlayerList.Item2);
                context.RivalPlayer.Battlefield.SendToGraveyard(rivalPlayerList.Item1, rivalPlayerList.Item2);
            }

            else return false;

            return true;
        }
        #endregion



        #region 7
        public static bool MultiplyItself(Context context)
        {
            Player player = context.ActualPlayer;
            UnityCard card = (UnityCard)context.ActualCard;
            int count = 0;
            List<UnityCard> list = new List<UnityCard>();

            for (int i = 0; i < player.Battlefield.Zones.Length; i++)
            {
                CountCardsInList(player.Battlefield.Zones[i], card, list, ref count);
            }

                foreach (UnityCard cards in list)
                {
                    cards.ChangeActualPower(cards.ActualPower * count, true);
                }

            return true;
        }
        #endregion


        #region 8
        public static bool CleanLowerZone(Context context)
        {
            try
            {
                int minCount = 7;
                List<Card> list = null;
                Card augment = Tools.MotherCard;
                Player player = context.RivalPlayer;

                for (int i = 0; i < context.ActualPlayer.Battlefield.Zones.Length; i++)
                {
                    if (CountCardsAndCheckMinCount(context.ActualPlayer.Battlefield.Zones[i], context.ActualPlayer, ref minCount, ref list, context.ActualPlayer))
                    {
                        augment = context.ActualPlayer.Battlefield.Augment[i];
                        player = context.ActualPlayer;
                    }
                    if (CountCardsAndCheckMinCount(context.RivalPlayer.Battlefield.Zones[i], context.RivalPlayer, ref minCount, ref list, context.RivalPlayer))
                    {
                        augment = context.RivalPlayer.Battlefield.Augment[i];
                        player = context.RivalPlayer;
                    }
                }

                if (list is null) return false;

                player.Battlefield.SendToGraveyard(augment, player.Battlefield.Augment);
                player.Battlefield.SendToGraveyard(list);

                return true;
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
                return false;
            }
        }
        #endregion

    

        #region 9
        public static bool VoidEffect(Context context) => true;
        #endregion




        #region Tools
        static bool CountCardsAndCheckMinCount(List<Card> forCount, Player player, ref int minCount, ref List<Card> list, Player actualPlayer)
        {
            int count = 0;

            foreach (Card card in forCount)
            {
            if(!card.Equals(Tools.MotherCard)) 
                    count++;
            }

            if (!actualPlayer.Battlefield.Augment[Tools.IndexByZone[actualPlayer.ZoneByList[forCount]]].Equals(Tools.MotherCard))
                count++;
            
            if (count > 0 && (count < minCount || (count == minCount && !player.Equals(actualPlayer))))
            {
                minCount = count;
                list = forCount;
                return true;
            }
            return false;
        }

        static void CountCardsInList(List<Card> forCount, UnityCard card, List<UnityCard> list, ref int count)
        {
            foreach (Card cards in forCount)
            {
                if (cards.Equals(card))
                {
                    count++;
                    list.Add((UnityCard)cards);
                }
            }
        }

        static void ChangeActualPower(List<Card> list, int eliminate = 1)
        {
            foreach (Card card in list)
            {
                if (card is UnityCard unityCard && unityCard.Rank == Rank.Silver)
                {
                    unityCard.ChangeActualPower(unityCard.ActualPower - eliminate, true);
                }
            }
        }

        static void KeepCardsAndCountPower(List<Card> forCount, List<UnityCard> list, ref double power)
        {
            foreach (Card card in forCount)
            {
                if (card is UnityCard unityCard)
                {
                    list.Add(unityCard);
                    power += unityCard.ActualPower;
                }
            }
        }
        #endregion

    }
}