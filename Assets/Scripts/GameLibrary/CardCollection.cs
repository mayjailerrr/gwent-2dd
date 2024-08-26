using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Dynamic;
using System;
using System.Linq;

public class CardsCollection 
{
   public int NumberOfCards { get; private set; }
   public List<Card> Collection { get; private set; } = new();
   public Dictionary<string, List<Card>> AllFactions {get; private set;} = new();
   public Dictionary<string, Card> AllLeaders { get; private set; } = new();

   public CardsCollection(List<string[]> CardInfoArray)
   {
        NumberOfCards = CardInfoArray.Count;

        int count = 0;

        foreach (string[] infoArray in CardInfoArray)
        {
            Card card = TypeCreator(infoArray[0], infoArray);
            Collection.Add(card);
            Debug.Log($"{card.Name} is in the battlefield now");
            count++;

            if (card.Type != CardTypes.Leader)
            {
                if (!AllFactions.Keys.Contains(card.Faction))
                {
                    List<Card> Temp = new();
                    AllFactions.Add(card.Faction, Temp);
                    Temp.Add(card);
                }
                else AllFactions[card.Faction].Add(card);
            }

            else
            {
                if (!AllLeaders.ContainsKey(card.Faction))
                    AllLeaders.Add(card.Faction, card);
            }
        }

        Debug.Log($"{count} cards have been charged");

        if(NumberOfCards == count)
            Debug.Log("All cards have been charged");
   }


   private static Card TypeCreator(string TypeLetter, string[] CardInfoArray)
   {
        switch (TypeLetter)
        {
            case "L":
                return new Leader(CardInfoArray);
            case "H":
                return new Hero(CardInfoArray);
            case "S":
                return new Silver(CardInfoArray);
            case "A":
                return new Augment(CardInfoArray);
            case "W":
                return new WeatherCard(CardInfoArray); 
            case "B":
                return new Hero(CardInfoArray);
            // case "B":
            //     return new Lure(CardInfoArray); //my ver
            case "C":
                return new Clearance(CardInfoArray);
            
            default: throw new ArgumentException("Type not defined");
        }
   }
}
