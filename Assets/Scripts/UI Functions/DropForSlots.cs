using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using JetBrains.Annotations;
using UnityEngine.UI;


public class DropForSlots : MonoBehaviour, IDropHandler
{
    public CardTypes CardType;
    public ZoneTypes ZoneCorrespondence;
    public HorizontalLayoutGroup Slot;
    public GameObject CardToDrop;
    public static bool IsDragging = false;
    public AudioClip DropSound;
    public AudioSource UIAudible;

    public void OnDrop(PointerEventData eventData)
    {
        CardToDrop = DragItem.CardBeingDragged;

        UICard uICard = CardToDrop.GetComponent<UICard>();
        Card card = uICard.MotherCard;

        Debug.Log("Card dropped: " + card.Name + $"in {CardType}");

        switch (CardType)
        {
            case CardTypes.Augment:
                if (Slot.transform.childCount < 1 && card is Augment augmentCard && augmentCard.Zone == ZoneCorrespondence)
                    PutCard(card);
                return;

            case CardTypes.Weather:
                if (Slot.transform.childCount < 1 && card is WeatherCard weatherCard && weatherCard.Zone == ZoneCorrespondence)
                    PutCard(card);
                return;
            
            default:
                return;
        }
    }

    private void PutCard(Card card)
    {
        CardToDrop.GetComponent<CanvasGroup>().blocksRaycasts = true;
        CardToDrop.transform.SetParent(Slot.transform);
        CardToDrop.transform.position = Slot.transform.position;
        DragItem beingDragged = CardToDrop.GetComponent<DragItem>();
        beingDragged.enabled = false;
        IsDragging = false;

        GameManager.gameManager.PlayCard(card, ZoneCorrespondence);
        UIAudible.PlayOneShot(DropSound);
    }
}



