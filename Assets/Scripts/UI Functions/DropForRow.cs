using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using JetBrains.Annotations;
using UnityEngine.UI;

public class DropForZone : MonoBehaviour, IDropHandler
{
    public ZoneTypes CardType;
    public HorizontalLayoutGroup Zone;
    public GameObject CardToDrop;
    public static bool IsDragging = false;
    public AudioClip DropSound;
    public AudioSource UIAudible;

    public void OnDrop(PointerEventData eventData)
    {
        CardToDrop = DragItem.CardBeingDragged;

        DragItem drag = CardToDrop.GetComponent<DragItem>();

        if (drag.originalParent.transform.parent.name == this.transform.parent.parent.name)
        {
            UICard uICard = CardToDrop.GetComponent<UICard>();
            Card card = uICard.MotherCard;

            Debug.Log("Card dropped: " + card.Name + $"in {CardType}");

            if (card is UnityCard unityCard && unityCard.Zone.Contains(CardType) && Zone.transform.childCount < 8)
            {
                CardToDrop.GetComponent<CanvasGroup>().blocksRaycasts = true;
                CardToDrop.transform.SetParent(Zone.transform);
                CardToDrop.transform.position = Zone.transform.position;
               
                drag.enabled = false;
                IsDragging = true;

                GameManager.gameManager.PlayCard(card, CardType);
                UIAudible.PlayOneShot(DropSound);
            }
        }
    }
}