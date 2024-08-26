using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;

public class DeckInteraction : MonoBehaviour 
{
    public GridLayoutGroup Collection;
    public GridLayoutGroup Deck;

    private void Start()
    {
        Deck = transform.root.Find("DeckContainer").transform.GetChild(0).GetComponent<GridLayoutGroup>();
        Collection = transform.root.Find("CollectionContainer").transform.GetChild(0).GetComponent<GridLayoutGroup>();
    }

    public void AddOrRemoveCard()
    {
        Debug.Log("Iy got into the method");

        GameObject manager = transform.root.Find("CreateDeckManager").gameObject;
        CreatingDeck creatingDeck = manager.GetComponent<CreatingDeck>();

        GameObject prefab = creatingDeck.CardPrefab;

        UICard uICard = this.GetComponent<UICard>();
        Card card = uICard.MotherCard;

        if (this.transform.parent.parent.parent.name == "DeckContainer")
        {
            if (CreatingDeck.actualDeck.CardActualAppearances(card) == 1)
            {
                CreatingDeck.actualDeck.RemoveCardOfDeck(card);
                Destroy(transform.parent.transform.GetChild(0).gameObject);
            }

            else CreatingDeck.actualDeck.RemoveCardOfDeck(card);
        }
       
        else if (this.transform.parent.parent.parent.name == "CollectionContainer")
        {
            if (CreatingDeck.actualDeck.CardActualAppearances(card) > 0)
                CreatingDeck.actualDeck.DuplicateCard(card);
            else 
            {
                var newCard = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
                UICard uI = newCard.GetComponent<UICard>();
                uI.PrintCard(card);

                GameObject slot = GetEmptySlot();

                newCard.transform.SetParent(slot.transform);
                newCard.transform.localScale = slot.transform.position;
                Debug.Log(card.Name + "Card added to collection");
            }
        }
    }

    private GameObject GetEmptySlot()
    {
        GameObject slot;

        for (int i = 0; i < Deck.transform.childCount; i++)
        {
           if (Deck.transform.GetChild(i).childCount == 0)
           {
               slot = Deck.transform.GetChild(i).gameObject;
               return slot;
           }
        }

        return null;
    }
}

