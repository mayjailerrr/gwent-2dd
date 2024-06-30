using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    // private Board board;

    private void Start()
    {
        // board = GetComponent<Board>();

        TextAsset cardJson = Resources.Load<TextAsset>("cards");

        if (cardJson != null)
        {
            CardDatabase cardDatabase = JsonUtility.FromJson<CardDatabase>(cardJson.text);

            List<IPlayable> cards = new List<IPlayable>();

            foreach (var cardData in cardDatabase.meleeCards)
            {
                MeleeCard card = new MeleeCard
                {
                    cardName = cardData.cardName,
                    power = cardData.power,
                    cardImagePath = cardData.cardImagePath,
                    faction = cardData.faction,
                    rank = cardData.rank,
                    attackType = cardData.attackType
                };
                cards.Add(card);
            }

            foreach (var cardData in cardDatabase.rangedCards)
            {
                RangedCard card = new RangedCard
                {
                    cardName = cardData.cardName,
                    power = cardData.power,
                    cardImagePath = cardData.cardImagePath,
                    faction = cardData.faction,
                    rank = cardData.rank,
                    attackType = cardData.attackType
                };
                cards.Add(card);
            }

            foreach (var cardData in cardDatabase.siegeCards)
            {
                SiegeCard card = new SiegeCard
                {
                    cardName = cardData.cardName,
                    power = cardData.power,
                    cardImagePath = cardData.cardImagePath,
                    faction = cardData.faction,
                    rank = cardData.rank,
                    attackType = cardData.attackType
                };
                cards.Add(card);
            }

            foreach (var cardData in cardDatabase.weatherCards)
            {
                WeatherCard card = new WeatherCard
                {
                    cardName = cardData.cardName,
                    power = cardData.power,
                    cardImagePath = cardData.cardImagePath,
                    faction = cardData.faction,
                    rank = cardData.rank,
                    attackType = cardData.attackType
                };
                cards.Add(card);
            }
        }

        else
        {
            Debug.LogError("No se pudo cargar el archivo cards.json desde Resources");
        }
    }

}
