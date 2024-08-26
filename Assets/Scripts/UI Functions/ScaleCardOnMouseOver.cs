using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using System.Numerics;

public class ScaleCardOnMouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{ 
    public static UnityEngine.Vector3 vector = new UnityEngine.Vector3(1.2f, 1.2f, 1.2f);
    public static UnityEngine.Vector3 originalScale;

    public void OnPointerEnter(PointerEventData eventData)
    {
       originalScale = transform.localScale;
       transform.localScale = vector;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
       transform.localScale = originalScale;
    }
}
