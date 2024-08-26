using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using System.Diagnostics;
using System;

public static class Utils
{
    public static Dictionary<int, ZoneTypes> ZoneForIndex = new()
    {
        {0, ZoneTypes.Melee},
        {1, ZoneTypes.Distance},
        {2, ZoneTypes.Siege}
    };

    public static int NumberOfSilverInZone(List<UnityCard> zone)
    {
        int count = 0;

        foreach (UnityCard unity in zone)
        {
            if (unity is Silver)
            {
                count++;
            }
        }

        return count;
    }

    public static List<ZoneTypes> GetZoneTypes(string zones)
    {
        List<ZoneTypes> zoneTypes = new();

        for (int i = 0; i < zones.Length; i++)
        {
           switch (zones[i])
           {
               case 'M':
                   zoneTypes.Add(ZoneTypes.Melee);
                   continue;
               case 'D':
                   zoneTypes.Add(ZoneTypes.Distance);
                   continue;
               case 'S':
                   zoneTypes.Add(ZoneTypes.Siege);
                   continue;
           }
        }
        return zoneTypes;
    }

    public static ZoneTypes GetZoneForSpecialCards(string zone)
    {
        for (int i = 0; i < zone.Length; i++)
        {
            switch (zone[i])
            {
                case 'M':
                    return ZoneTypes.Melee;
                case 'D':
                    return ZoneTypes.Distance;
                case 'S':
                    return ZoneTypes.Siege;
            }
        }
        throw new ArgumentException("The zone is not valid.");
        
    }
}

 public enum ZoneTypes
{
    Melee,
    Distance,
    Siege
}

public enum CardTypes
{
    Leader,
    Hero,
    Silver,
    Lure,
    Augment,
    Weather,
    Clearance
}
