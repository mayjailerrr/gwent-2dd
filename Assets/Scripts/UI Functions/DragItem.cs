using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using UnityEngine.UI;
using TMPro;

public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Vector3 originalPosition;
    public Transform originalParent;
    public static GameObject CardBeingDragged;

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = transform.position;
        originalParent = transform.parent;

        transform.SetParent(transform.root);
        transform.SetAsLastSibling();

        CardBeingDragged = gameObject;

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (CardBeingDragged.GetComponent<UICard>().MotherCard.Type == CardTypes.Clearance)
        {
            GameManager.gameManager.PlayCard(CardBeingDragged.GetComponent<UICard>().MotherCard, ZoneTypes.Distance);
            Destroy(CardBeingDragged.gameObject);
            CardBeingDragged = null;
        }

        else 
        {
           CardBeingDragged = null;

           transform.position = originalPosition;
           transform.SetParent(originalParent); 

           GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
    }
}
