using System.Security.Cryptography;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;
using TMPro;

public abstract class Card 
{
    public string type;

    public CardTypes Type
    {
        get { return TypeClassifier(type); }
        private set { type = TypeClassifier(type).ToString().Replace('_', ' '); }
    }

    public string Name { get; private set; }
    public string Faction { get; private set; }
    public string EffectDescription { get; private set; }
    public int EffectNumber { get; private set; }
    public string CharacterDescription { get; private set; }
    public string Quote { get; private set; }

    public Card(string[] CardInfoArray)
    {
        type = CardInfoArray[0];
        Name = CardInfoArray[1];
        Faction = CardInfoArray[2];;
        EffectDescription = CardInfoArray[5];
        EffectNumber = int.Parse(CardInfoArray[6]);
        CharacterDescription = CardInfoArray[7];
        Quote = CardInfoArray[8];
    }

    public Card(Card card)
    {
        type = card.type;
        Type = card.Type;
        Name = card.Name;
        Faction = card.Faction;
        EffectDescription = card.EffectDescription;
        EffectNumber = card.EffectNumber;
        CharacterDescription = card.CharacterDescription;
        Quote = card.Quote;
    }

    public void ActivateEffect(Player ActivePlayer, Player RivalPlayer, Card card)
    {
        Effect effect = Effect.effects[EffectNumber];
        effect.TakeEffect(ActivePlayer, RivalPlayer, card);
    }

    public void CancelCardEffect()
    {
        EffectNumber = 17;
    }

    private static CardTypes TypeClassifier(string TypeLetter)
    {
        switch (TypeLetter)
        {
            case "L":
                return CardTypes.Leader;
            case "H":
                return CardTypes.Hero;
            case "S":
                return CardTypes.Silver;
            case "A":
                return CardTypes.Augment;
            case "W":
                return CardTypes.Weather;
            case "B":
                return CardTypes.Lure;
            case "C":
                return CardTypes.Clearance;
            
            default:
                throw new ArgumentException("This type is not defined, sorry");
        }
    }
}
