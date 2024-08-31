using System.Collections.Generic;
using System.Collections;
//using UnityEngine;
using System;

namespace GameLibrary
{
    public class WeatherCard : Card
    {
        public WeatherCard(string name, Faction faction, CardType type, List<Zone> ranges, double power = 0, string effectKey = null) : base(name, faction, type, ranges, power)
        {
             if (!string.IsNullOrEmpty(effectKey))
            {
                AllocateEffect(effectKey);
            }
        }

        public bool WeatherEffect(Context context)
        {
            ReduceSilverCard(Zone.Melee);
            ReduceSilverCard(Zone.Distance);
            ReduceSilverCard(Zone.Siege);
            return true;
        }

        private void ReduceSilverCard(Zone range)
        {
            if (this.Ranges.Contains(range))
            {
                List<Card> clouds = Player.Clouds.ListByZone[range];
                List<Card> reign = Player.Reign.ListByZone[range];

                for (int i = 0; i < Math.Min(clouds.Count, reign.Count); i++)
                {
                    switch (reign[i], clouds[i])
                    {
                        case (UnityCard reignUnit, _) when reignUnit.Rank == Rank.Silver && !Player.Reign.Battlefield.UsedClearance[Tools.IndexByZone[range]]:
                            reignUnit.Powerr -= reignUnit.Power < initialPower ? reignUnit.Power : initialPower;
                            break;

                        case (_, UnityCard cloudUnit) when cloudUnit.Rank == Rank.Silver && !Player.Clouds.Battlefield.UsedClearance[Tools.IndexByZone[range]]:
                            cloudUnit.Powerr -= cloudUnit.Power < initialPower ? cloudUnit.Power : initialPower;
                            break;
                    }

                }
            }
        }
    }
}
