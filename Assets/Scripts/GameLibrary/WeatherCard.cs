using System.Collections.Generic;
using System.Collections;
using System;

namespace GameLibrary
{
    public class WeatherCard : Card
    {
        public WeatherCard(string name, Faction faction, AttackType type, List<Zone> ranges, double power = 0, Effect effect = null) : 
        base(name, faction, type, ranges, power, effect)
        {
           
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
                            reignUnit.InitialPower -= reignUnit.Power < initialPower ? reignUnit.Power : initialPower;
                            break;

                        case (_, UnityCard cloudUnit) when cloudUnit.Rank == Rank.Silver && !Player.Clouds.Battlefield.UsedClearance[Tools.IndexByZone[range]]:
                            cloudUnit.InitialPower -= cloudUnit.Power < initialPower ? cloudUnit.Power : initialPower;
                            break;
                    }

                }
            }
        }
    }
}
