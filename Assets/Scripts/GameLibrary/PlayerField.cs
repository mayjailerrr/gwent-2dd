using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;
using System.ComponentModel.Design;
using Unity.VisualScripting;


public class PlayerBattlefield
{
    private List<UnityCard>[] battlefield = new List<UnityCard>[3];
    private Augment[] augmentColumn = new Augment[3];
    private static WeatherCard[] weatherZone = new WeatherCard[3];
    public int MeleeZoneScore;
    public int DistanceZoneScore;
    public int SiegeZoneScore;
    public int TotalScore { get; private set; }
    public static Dictionary<ZoneTypes, int> ZoneCorrespondence {get; private set; } = new()
    {
        {ZoneTypes.Melee, 0},
        {ZoneTypes.Distance, 1},
        {ZoneTypes.Siege, 2}
    };

    public PlayerBattlefield()
    {
        battlefield[0] = new();
        battlefield[1] = new();
        battlefield[2] = new();

        UpdateBattlefield();
    }

    public UnityCard GetCardFromBattlefield(ZoneTypes zone, int position)
    {
        return battlefield[ZoneCorrespondence[zone]][position];
    }

    public Augment GetAugmentFromColumn(ZoneTypes zone)
    {
        return augmentColumn[ZoneCorrespondence[zone]];
    }

    public static WeatherCard GetWeatherCardFromZone(ZoneTypes zone)
    {
        return weatherZone[ZoneCorrespondence[zone]];
    }

    public List<UnityCard> GetZoneFromBattlefield(ZoneTypes zone)
    {
        return battlefield[ZoneCorrespondence[zone]];
    }

    public void AddCardToAugmentColumn(Augment augmentCard, ZoneTypes zone)
    {
        augmentColumn[ZoneCorrespondence[zone]] = augmentCard;
    }

    public static void AddWeatherCardToZone(WeatherCard weatherCard, ZoneTypes zone)
    {
        weatherZone[ZoneCorrespondence[zone]] = weatherCard;
    }

    public void AddCardToBattlefield(UnityCard card, ZoneTypes zone)
    {
        battlefield[ZoneCorrespondence[zone]].Add(card);
    }

    public void RemoveCardFromBattlefield(UnityCard card, ZoneTypes zone)
    {
        battlefield[ZoneCorrespondence[zone]].Remove(card);
    }



    private static int ScoreCalculator(List<UnityCard> zone)
    {
        int score = 0;

        foreach (UnityCard card in zone)
        {
            if (card is Silver silverCard)
            {
                score += silverCard.initialPower;
            }
            else
            {
                score += card.Power;
            }

        }

        return score;
    }

    private static int TotalScoreCalculator(int MeleeZoneScore, int DistanceZoneScore, int SiegeZoneScore)
    {
        int totalScore = MeleeZoneScore + DistanceZoneScore + SiegeZoneScore;
        return totalScore;
    }

    public void UpdateBattlefield()
    {
        MeleeZoneScore = ScoreCalculatorWithModifiers(ZoneTypes.Melee);
        DistanceZoneScore = ScoreCalculatorWithModifiers(ZoneTypes.Distance);
        SiegeZoneScore = ScoreCalculatorWithModifiers(ZoneTypes.Siege);
       
        TotalScore = TotalScoreCalculator(MeleeZoneScore, DistanceZoneScore, SiegeZoneScore);
    }

    private int ScoreCalculatorWithModifiers(ZoneTypes zone)
    {
        if (GetZoneFromBattlefield(zone).Count > 0)
        {
            if (GetWeatherCardFromZone(zone) == null && GetAugmentFromColumn(zone) == null)
            {
                Debug.Log("No weather card or augment card in this zone" + zone.ToString());
                return ScoreCalculator(GetZoneFromBattlefield(zone));
            }
           
            else
            {
                if (GetWeatherCardFromZone(zone) != null)
                {
                    WeatherStatus(zone);
                }
                if (GetAugmentFromColumn(zone) != null)
                {
                    AugmentStatus(zone);
                }

                return ScoreCalculator(GetZoneFromBattlefield(zone));
            }
        }

        else return 0;
    }

    private void WeatherStatus(ZoneTypes zone)
    {
        foreach (UnityCard card in GetZoneFromBattlefield(zone))
        {
            if (card is Silver silverCard)
            {
                silverCard.initialPower = 1;
            }
        }
        Debug.Log("Weather card applied to the zone " + zone.ToString());
    }

    private void AugmentStatus(ZoneTypes zone)
    {
        foreach (UnityCard card in GetZoneFromBattlefield(zone))
        {
            if (card is Silver silverCard)
            {
                if (silverCard.initialPower == 1 || silverCard.initialPower == silverCard.Power)
                {
                    silverCard.initialPower++;
                }
            }
        }
        Debug.Log("Augment card applied to the zone " + zone.ToString());
    }

    public void ClearBattlefield()
    {
        for (int i = 0; i <= 2; i++)
        {
            battlefield[i] = null;
            battlefield[i] = new();
        }
        ClearWeatherZone();
        ClearAugmentColumn();
    }

    public static void ClearWeatherZone()
    {
        weatherZone = null;
        weatherZone = new WeatherCard[3];
    }

    public void ClearAugmentColumn()
    {
        augmentColumn = null;
        augmentColumn = new Augment[3];
    }

    public int NumberOfUnityCardsInBattlefield()
    {
        int count = 0;

        foreach (List<UnityCard> cardList in battlefield)
        {
           count += cardList.Count;
        }
        return count;
    }
}

