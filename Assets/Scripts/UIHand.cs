using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEditor;

public class UIHand : MonoBehaviour
{
    [SerializeField] Subject gameManager;
    public GameObject Hand;
    public GameObject CardPrefab;
    public CanvasGroup Interaction;
    public Button LeaderInteraction;
    public bool LeaderAbilityActive = true;
    public Button PassButton;

    async void Start()
    {
        Hand hand;

        if (this.transform.parent.name == "Player1")
        {
            hand = GameData.Player1.PlayerHand;

            foreach (Card card in hand.PlayerHand)
            {
                var newCard = Instantiate(CardPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                newCard.transform.SetParent(Hand.transform, false);
                UICard uI = newCard.GetComponent<UICard>();
                uI.PrintCard(card);

                Debug.Log(card.Name);

                await Task.Delay(400);
            }
        }
        else if (this.transform.parent.name == "Player2")
        {
            hand = GameData.Opponent.PlayerHand;

            foreach (Card card in hand.PlayerHand)
            {
                var newCard = Instantiate(CardPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                newCard.transform.SetParent(Hand.transform, false);
                UICard uI = newCard.GetComponent<UICard>();
                uI.PrintCard(card);

                Debug.Log(card.Name);

                await Task.Delay(400);
            }
        }
        else Debug.Log("Error: Hand not found" + this.transform.parent.name);
    }

    void Update()
    {
        if (this.transform.parent.name == "Player1")
        {
            if (GameManager.player.IsActive == true)
            {
                Interaction.blocksRaycasts = true;
                PassButton.interactable = true;

                if (LeaderAbilityActive)
                    LeaderInteraction.interactable = true;

                else LeaderInteraction.interactable = false;
            }
            else
            {
                Interaction.blocksRaycasts = false;
                LeaderInteraction.interactable = false;
                PassButton.interactable = false;
            }
        }

        if (this.transform.parent.name == "Player2")
        {
            if (GameManager.opponent.IsActive == true)
            {
                Interaction.blocksRaycasts = true;
                PassButton.interactable = true;

                if (LeaderAbilityActive)
                    LeaderInteraction.interactable = true;

                else LeaderInteraction.interactable = false;
            }
            else
            {
                Interaction.blocksRaycasts = false;
                LeaderInteraction.interactable = false;
                PassButton.interactable = false;
            }
        }
    }

    async public void DrawCardUI(Card card)
    {
        await Task.Delay(400);

        if (Hand.GetComponent<HorizontalLayoutGroup>().transform.childCount < 10)
        {
            var newCard = Instantiate(CardPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            newCard.transform.SetParent(Hand.transform, false);
            UICard uI = newCard.GetComponent<UICard>();
            uI.PrintCard(card);

            Debug.Log($"Card Drawn: {card.Name}");
        }
        else Debug.Log("Hand is full");
    }
}