using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreatingDeck : MonoBehaviour
{
    private CardsCollection cardsCollection;
    public static DeckCreator actualDeck;

    public GameObject CardPrefab;
    public GridLayoutGroup CollectionShower;
    public GridLayoutGroup DeckShower;

    public Image LeaderImage;

    public TextMeshProUGUI CardsInHand;
    public TextMeshProUGUI UnityCards;
    public TextMeshProUGUI TotalForce;
    public TextMeshProUGUI HeroCards;
    public TextMeshProUGUI StringToChangeCollection;
    public TextMeshProUGUI StringToChangeDeck;
    public Button InitButton;

    void Start()
    {
        string path = "CardsCollection";
        cardsCollection = new CardsCollection(CardCreator.GetCardInfoList(path));

        InitButton.interactable = false;

        CardsInHand.text = "";
        UnityCards.text = "";
        TotalForce.text = "";
        HeroCards.text = "";
        HideImage();

        actualDeck = null;
    }

    void Update()
    {
        if (actualDeck != null)
        {
            CardsInHand.text = actualDeck.CardsTotalNumber.ToString();
            UnityCards.text = actualDeck.UnityCardsTotalNumber.ToString();
            TotalForce.text = actualDeck.UnityPowerTotalNumber.ToString();
            HeroCards.text = actualDeck.HeroCardsTotalNumber.ToString();

            if (actualDeck.CardsTotalNumber >= 25)
                InitButton.interactable = true;
            else
                InitButton.interactable = false;
        }
    }

    public void GenerateDeck(string Faction)
    {
        actualDeck = null;
        actualDeck = new DeckCreator(Faction, cardsCollection.AllFactions, cardsCollection.AllLeaders);

        LeaderImage.sprite = Resources.Load<Sprite>(actualDeck.DeckLeader.Name);
        ShowImage();
        ShowCollection("All");
        ShowDeck("All");
        StringToChangeCollection.text = "All the cards";
        StringToChangeDeck.text = "All the cards";
    }

    public void ShowCollection(string type)
    {
        if (actualDeck != null)
        {
            CleanCollection();

            List<Card> cardsToShow = new List<Card>();

            foreach (Card card in cardsCollection.Collection)
            {
                if (card.Type != CardTypes.Leader)
                {
                    if (type == "Augment")
                    {
                        if (card.Type == CardTypes.Augment)
                            cardsToShow.Add(card);
                        
                        StringToChangeDeck.text = type;
                    }

                    if (type == "Weather")
                    {
                        if (card.Type == CardTypes.Weather)
                            cardsToShow.Add(card);
                        
                        StringToChangeDeck.text = type;
                    }
                    
                    if (type == "All")
                    {
                        if (card.Faction == "Neutral" || card.Faction == actualDeck.Faction)
                            cardsToShow.Add(card);

                        StringToChangeDeck.text = "All the cards";
                    }

                    if (type == "Melee")
                    {
                        if (card is UnityCard unityCard && unityCard.Zone.Contains(ZoneTypes.Melee) && (card.Faction == "Neutral" || card.Faction == actualDeck.Faction))
                            cardsToShow.Add(card);
                        
                        StringToChangeDeck.text = "Melee attack";
                    }

                    if (type == "Distance")
                    {
                        if (card is UnityCard unityCard && unityCard.Zone.Contains(ZoneTypes.Distance) && (card.Faction == "Neutral" || card.Faction == actualDeck.Faction))
                            cardsToShow.Add(card);
                        
                        StringToChangeDeck.text = "Distance attack";
                    }

                    if (type == "Siege")
                    {
                        if (card is UnityCard unityCard && unityCard.Zone.Contains(ZoneTypes.Siege) && (card.Faction == "Neutral" || card.Faction == actualDeck.Faction))
                            cardsToShow.Add(card);
                        
                        StringToChangeDeck.text = "Siege attack";
                    }

                    if (type == "Decoy")
                    {
                        if (card.Name == "Tyrion" && (card.Faction == "Neutral" || card.Faction == actualDeck.Faction))
                            cardsToShow.Add(card);

                        StringToChangeDeck.text = "Lure";
                    }
                }
            }

            for (int i = 0; i < cardsToShow.Count; i++)
            {
                var newCard = Instantiate(CardPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                UICard uI = newCard.GetComponent<UICard>();
                uI.PrintCard(cardsToShow[i]);
                GameObject slot = CollectionShower.transform.GetChild(i).gameObject;
                newCard.transform.SetParent(slot.transform);
            }
        }
    }

    public void ShowDeck(string type)
    {
        if (actualDeck != null)
        {
            CleanDeck();

            List<Card> cardsToShow = new List<Card>();

            foreach (Card card in actualDeck.CardDeck)
            {
                if (type == "Augment")
                {
                    if (card.Type == CardTypes.Augment)
                        cardsToShow.Add(card);
                    
                    StringToChangeDeck.text = type;
                }

                if (type == "Weather")
                {
                    if (card.Type == CardTypes.Weather)
                        cardsToShow.Add(card);
                    
                    StringToChangeDeck.text = type;
                }
                
                if (type == "All")
                {
                    if (actualDeck.CardDeck.Contains(card))
                        cardsToShow.Add(card);

                    StringToChangeDeck.text = "All the cards";
                }

                if (type == "Melee")
                {
                    if (card is UnityCard unityCard && unityCard.Zone.Contains(ZoneTypes.Melee))
                        cardsToShow.Add(card);
                    
                    StringToChangeDeck.text = "Melee attack";
                }

                if (type == "Distance")
                {
                    if (card is UnityCard unityCard && unityCard.Zone.Contains(ZoneTypes.Distance))
                        cardsToShow.Add(card);
                    
                    StringToChangeDeck.text = "Distance attack";
                }

                if (type == "Siege")
                {
                    if (card is UnityCard unityCard && unityCard.Zone.Contains(ZoneTypes.Siege))
                        cardsToShow.Add(card);
                    
                    StringToChangeDeck.text = "Siege attack";
                }

                if (type == "Decoy")
                {
                    if (card.Name == "Tyrion")
                        cardsToShow.Add(card);

                    StringToChangeDeck.text = "Lure";
                }
            }

            List<string> CardsWithoutReps = new();
            foreach (Card card in cardsToShow)
            {
                if (!CardsWithoutReps.Contains(card.Name))
                    CardsWithoutReps.Add(card.Name);
            }

            for (int i = 0; i < CardsWithoutReps.Count; i++)
            {
                var newCard = Instantiate(CardPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                UICard uI = newCard.GetComponent<UICard>();
                uI.PrintCard(getCardWithTheName(CardsWithoutReps[i], cardsToShow));
                GameObject slot = DeckShower.transform.GetChild(i).gameObject;
                newCard.transform.SetParent(slot.transform);
            }
        }
    }

    private Card getCardWithTheName(string name, List<Card> cardsToShow)
    {
        foreach (Card card in cardsToShow)
        {
            if (card.Name == name)
                return card;
        }

        return null;
    }

    private void ShowImage()
    {
        LeaderImage.color = new Color(LeaderImage.color.r, LeaderImage.color.g, LeaderImage.color.b, 1);
    }

    private void HideImage()
    {
        LeaderImage.color = new Color(LeaderImage.color.r, LeaderImage.color.g, LeaderImage.color.b, 0);
    }

    private void CleanCollection()
    {
        for (int i = 0; i < CollectionShower.transform.childCount; i++)
        {
            GameObject slot = CollectionShower.transform.GetChild(i).gameObject;
            
            foreach (Transform child in slot.transform)
                Destroy(child.gameObject);
        }
    }

    private void CleanDeck()
    {
        for (int i = 0; i < DeckShower.transform.childCount; i++)
        {
            GameObject slot = DeckShower.transform.GetChild(i).gameObject;
            
            foreach (Transform child in slot.transform)
                Destroy(child.gameObject);
        }
    }

    public void SaveDeck()
    {
        GameData.SetPlayer();
    }
}
