using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using System.ComponentModel.Design;
using System.Diagnostics;
using UnityEditor;
using System.Data;
using System;

public abstract class Effect
{
    public static Dictionary<int, Effect> effects {get; private set;} = new()
    {
        {1, new AugmentMeleeEffect()},
        {2, new AugmentDistanceEffect()},
        {3, new AugmentSiegeEffect()},

        {4, new WeatherMeleeEffect()},
        {5, new WeatherDistanceEffect()},
        {6, new WeatherSiegeEffect()},

        {7, new DrawEffect()},

        {8, new DeleteWeakestEffect()},
        {9, new DeleteStrongestEffect()},

        {10, new CloseBondEffect()}, //??

        {11, new EliminateAZoneEffect()},

        {12, new ClearanceEffect()},

        {13, new AverageEffect()},

        {14, new DecoyEffect()}, //??

        {15, new SupremacyEffect()},

        {16, new CancelEffect()},

        {17, new VoidEffect()}
    };

    public abstract void TakeEffect(Player ActivePlayer, Player OpponentPlayer, Card card);
}

public abstract class AugmentEffect : Effect
{
   
}

public class AugmentMeleeEffect : AugmentEffect
{
    public override void TakeEffect(Player ActivePlayer, Player OpponentPlayer, Card card)
    {
       Card toInvoke = null;

       if (ActivePlayer.Battlefield.GetAugmentFromColumn(ZoneTypes.Melee) == null)
       {
            foreach (Card toFind in ActivePlayer.PlayerHand.GameDeck)
            {
                if (toFind is Augment augmentCard && augmentCard.Zone == ZoneTypes.Melee)
                {
                    toInvoke = augmentCard;
                }
            }
       }
       if (toInvoke != null)
       {
            GameManager.gameManager.InvokeCard(toInvoke, ZoneTypes.Melee);
            return;
       }
    }
}

public class AugmentDistanceEffect : AugmentEffect
{
    public override void TakeEffect(Player ActivePlayer, Player OpponentPlayer, Card card)
    {
       Card toInvoke = null;

       if (ActivePlayer.Battlefield.GetAugmentFromColumn(ZoneTypes.Distance) == null)
       {
            foreach (Card toFind in ActivePlayer.PlayerHand.GameDeck)
            {
                if (toFind is Augment augmentCard && augmentCard.Zone == ZoneTypes.Distance)
                {
                    toInvoke = augmentCard;
                }
            }
       }
       if (toInvoke != null)
       {
            GameManager.gameManager.InvokeCard(toInvoke, ZoneTypes.Distance);
            return;
       }
    }
}

public class AugmentSiegeEffect : AugmentEffect
{
    public override void TakeEffect(Player ActivePlayer, Player OpponentPlayer, Card card)
    {
       Card toInvoke = null;

       if (ActivePlayer.Battlefield.GetAugmentFromColumn(ZoneTypes.Siege) == null)
       {
            foreach (Card toFind in ActivePlayer.PlayerHand.GameDeck)
            {
                if (toFind is Augment augmentCard && augmentCard.Zone == ZoneTypes.Siege)
                {
                    toInvoke = augmentCard;
                }
            }
       }
       if (toInvoke != null)
       {
            GameManager.gameManager.InvokeCard(toInvoke, ZoneTypes.Siege);
            return;
       }
    }
}

public abstract class WeatherEffect : Effect
{
   
}

public class WeatherMeleeEffect : WeatherEffect
{
    public override void TakeEffect(Player ActivePlayer, Player OpponentPlayer, Card card)
    {
       Card toInvoke = null;

       if (PlayerBattlefield.GetWeatherCardFromZone(ZoneTypes.Melee) == null)
       {
            foreach (Card toFind in ActivePlayer.PlayerHand.GameDeck)
            {
                if (toFind is WeatherCard weatherCard && weatherCard.Zone == ZoneTypes.Melee)
                {
                    toInvoke = weatherCard;
                }
            }
       }
       if (toInvoke != null)
       {
            GameManager.gameManager.InvokeCard(toInvoke, ZoneTypes.Melee);
            return;
       }
    }
}

public class WeatherDistanceEffect : WeatherEffect
{
    public override void TakeEffect(Player ActivePlayer, Player OpponentPlayer, Card card)
    {
       Card toInvoke = null;

       if (PlayerBattlefield.GetWeatherCardFromZone(ZoneTypes.Distance) == null)
       {
            foreach (Card toFind in ActivePlayer.PlayerHand.GameDeck)
            {
                if (toFind is WeatherCard weatherCard && weatherCard.Zone == ZoneTypes.Distance)
                {
                    toInvoke = weatherCard;
                }
            }
       }
       if (toInvoke != null)
       {
            GameManager.gameManager.InvokeCard(toInvoke, ZoneTypes.Distance);
            return;
       }
    }
}

public class WeatherSiegeEffect : WeatherEffect
{
    public override void TakeEffect(Player ActivePlayer, Player OpponentPlayer, Card card)
    {
       Card toInvoke = null;

       if (PlayerBattlefield.GetWeatherCardFromZone(ZoneTypes.Siege) == null)
       {
            foreach (Card toFind in ActivePlayer.PlayerHand.GameDeck)
            {
                if (toFind is WeatherCard weatherCard && weatherCard.Zone == ZoneTypes.Siege)
                {
                    toInvoke = weatherCard;
                }
            }
       }
       if (toInvoke != null)
       {
            GameManager.gameManager.InvokeCard(toInvoke, ZoneTypes.Siege);
            return;
       }
    }
}

public class DrawEffect : Effect
{
    public override void TakeEffect(Player ActivePlayer, Player OpponentPlayer, Card card)
    {
        ActivePlayer.PlayerHand.DrawCard();
    }
}

public abstract class DeleteEffect : Effect
{
   
}

public class DeleteWeakestEffect : DeleteEffect
{
    public override void TakeEffect(Player ActivePlayer, Player OpponentPlayer, Card card)
    {
        int minPower = int.MaxValue;
        int minPowerIndex = -1;
        int pos = -1;
        Silver cardToDelete = null; //??

        for (int i = 0; i < 3; i++)
        {
            foreach (var unit in OpponentPlayer.Battlefield.GetZoneFromBattlefield(Utils.ZoneForIndex[i]))
            {
                if (unit is Silver silverUnityCard && silverUnityCard.initialPower < minPower)
                {
                    cardToDelete = silverUnityCard;
                    minPower = silverUnityCard.initialPower;
                    minPowerIndex = i;
                    pos = 1;
                }
            }
        }

        if (pos != -1)
        {
            if (pos == 0)
            {
                if (cardToDelete != null)
                {
                    ActivePlayer.Battlefield.RemoveCardFromBattlefield(cardToDelete, Utils.ZoneForIndex[minPowerIndex]);
                }
            }
            else
            {
                if (cardToDelete != null)
                {
                    OpponentPlayer.Battlefield.RemoveCardFromBattlefield(cardToDelete, Utils.ZoneForIndex[minPowerIndex]);
                }
            }
        }
    }
}

public class DeleteStrongestEffect : DeleteEffect
{
    public override void TakeEffect(Player ActivePlayer, Player OpponentPlayer, Card card)
    {
        int maxPower = int.MinValue;
        int maxPowerIndex = -1;
        int pos = -1;
        Silver cardToDelete = null; //??

        for (int i = 0; i < 3; i++)
        {
            foreach (var unit in OpponentPlayer.Battlefield.GetZoneFromBattlefield(Utils.ZoneForIndex[i]))
            {
                if (unit is Silver silverUnityCard && silverUnityCard.initialPower > maxPower)
                {
                    cardToDelete = silverUnityCard;
                    maxPower = silverUnityCard.initialPower;
                    maxPowerIndex = i;
                    pos = 0;
                }
            }
        }

        for (int i = 0; i < 3; i++)
        {
            foreach (var unit in ActivePlayer.Battlefield.GetZoneFromBattlefield(Utils.ZoneForIndex[i]))
            {
                if (unit is Silver silverUnityCard && silverUnityCard.initialPower > maxPower)
                {
                    cardToDelete = silverUnityCard;
                    maxPower = silverUnityCard.initialPower;
                    maxPowerIndex = i;
                    pos = 1;
                }
            }
        }

        if (pos != -1)
        {
            if (pos == 0)
            {
                if (cardToDelete != null)
                {
                    OpponentPlayer.Battlefield.RemoveCardFromBattlefield(cardToDelete, Utils.ZoneForIndex[maxPowerIndex]);
                }
            }
            else
            {
                if (cardToDelete != null)
                {
                    ActivePlayer.Battlefield.RemoveCardFromBattlefield(cardToDelete, Utils.ZoneForIndex[maxPowerIndex]);
                }
            }
        }
    }
}

public class CloseBondEffect : Effect
{
    public override void TakeEffect(Player ActivePlayer, Player OpponentPlayer, Card card)
    {
        int n = 0;

        for (int i = 0; i < 3; i++)
        {
            List<UnityCard> cardList = ActivePlayer.Battlefield.GetZoneFromBattlefield(Utils.ZoneForIndex[i]);

            if (cardList.Count > 0)
            {
                foreach (var unityCard in cardList)
                {
                    if (unityCard.Name == card.Name)
                        n++;
                }
            }
        }

        for (int i = 0; i < 3; i++)
        {
            List<UnityCard> cardList = ActivePlayer.Battlefield.GetZoneFromBattlefield(Utils.ZoneForIndex[i]);

            if (cardList.Count > 0)
            {
                foreach (var unityCard in cardList)
                {
                    if (unityCard.Name == card.Name && unityCard is Silver silverUnityCard) 
                        silverUnityCard.initialPower *= n;
                }
            }
        }
    }
}

public class EliminateAZoneEffect : Effect
{
    public override void TakeEffect(Player ActivePlayer, Player OpponentPlayer, Card card)
    {
        List<UnityCard> listToClear = null;
        int minCount = int.MaxValue;

        int zone = UnityEngine.Random.Range(0, 3);

        for (int i = 0; i < 3; i++)
        {
            List<UnityCard> cardList = ActivePlayer.Battlefield.GetZoneFromBattlefield(Utils.ZoneForIndex[i]);

            if (cardList.Count > 0 && cardList.Count < minCount && Utils.NumberOfSilverInZone(cardList) > 0)
            {
                minCount = cardList.Count;
                listToClear = cardList;
            }
        }
        // for (int i = 0; i < 3; i++)
        // {
        //     List<Unit> cardList = ActivePlayer.Battlefield.GetZoneFromBattlefield(Utils.ZoneForIndex[i]);

        //     if (cardList.Count > 0 && cardList.Count < minCount && Utils.NumberOfSilverInZone(cardList) > 0)
        //     {
        //         minCount = cardList.Count;
        //         listToClear = cardList;
        //     }
        // }

        if (listToClear != null)
        {
            for (int i = listToClear.Count; i >= 0; i--)
            {
                if (listToClear[i] is Silver)
                    listToClear.RemoveAt(i);
            }
        }
    }
}

public class ClearanceEffect : Effect
{
    public override void TakeEffect(Player ActivePlayer, Player OpponentPlayer, Card card)
    {
        PlayerBattlefield.ClearWeatherZone();

        for (int i = 0; i < 3; i++)
        {
            foreach (UnityCard unityCard in ActivePlayer.Battlefield.GetZoneFromBattlefield(Utils.ZoneForIndex[i]))
            {
                if (unityCard is Silver silverUnityCard)
                    silverUnityCard.initialPower = silverUnityCard.Power;
            }

            foreach (UnityCard unityCard in OpponentPlayer.Battlefield.GetZoneFromBattlefield(Utils.ZoneForIndex[i]))
            {
                if (unityCard is Silver silverUnityCard)
                    silverUnityCard.initialPower = silverUnityCard.Power;
            }
        }
    }
}

public class AverageEffect : Effect
{
    public override void TakeEffect(Player ActivePlayer, Player OpponentPlayer, Card card)
    {
        //check if this is correct
        int average;
        int cardsInField = ActivePlayer.Battlefield.NumberOfUnityCardsInBattlefield() + OpponentPlayer.Battlefield.NumberOfUnityCardsInBattlefield();
        average = (ActivePlayer.Battlefield.TotalScore + OpponentPlayer.Battlefield.TotalScore) / cardsInField;

        for (int i = 0; i < 3; i++)
        {
            for ( int j = 0; j < ActivePlayer.Battlefield.GetZoneFromBattlefield(Utils.ZoneForIndex[i]).Count; j++)
            {
                if (ActivePlayer.Battlefield.GetCardFromBattlefield(Utils.ZoneForIndex[i], j) is Silver silverUnityCard)
                    silverUnityCard.initialPower = average;
            }

            for ( int k = 0; k < OpponentPlayer.Battlefield.GetZoneFromBattlefield(Utils.ZoneForIndex[i]).Count; k++)
            {
                if (OpponentPlayer.Battlefield.GetCardFromBattlefield(Utils.ZoneForIndex[i], k) is Silver silverUnityCard)
                    silverUnityCard.initialPower = average;
            }
        }
    }
}

public class DecoyEffect : Effect
{
    public override void TakeEffect(Player ActivePlayer, Player OpponentPlayer, Card card)
    {

    }
}

public class SupremacyEffect : Effect
{
    public override void TakeEffect(Player ActivePlayer, Player OpponentPlayer, Card card)
    {
        for (int i = 0; i < 3; i++)
        {
            foreach (UnityCard unityCard in OpponentPlayer.Battlefield.GetZoneFromBattlefield(Utils.ZoneForIndex[i]))
            {
                if (unityCard is Silver silverUnityCard)
                    silverUnityCard.initialPower = 1;
            }
        }
    }
}

public class CancelEffect : Effect
{
    public override void TakeEffect(Player ActivePlayer, Player OpponentPlayer, Card card)
    {
        if (ActivePlayer.PlayerLeader.EffectNumber == 16 && OpponentPlayer.PlayerLeader.EffectNumber == 16)
        {
            ActivePlayer.PlayerLeader.CancelCardEffect();
            OpponentPlayer.PlayerLeader.CancelCardEffect();
        }
        else OpponentPlayer.PlayerLeader.CancelCardEffect();
    }
}

public class VoidEffect : Effect
{
    public override void TakeEffect(Player ActivePlayer, Player OpponentPlayer, Card card)
    {
       
    }
}
