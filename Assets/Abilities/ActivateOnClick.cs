using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActivateOnClick : MonoBehaviour
{
   public GameObject Deck;

   public void EnableEventTrigger()
   {

    if(Deck != null)
    {
        EventTrigger eventTrigger = Deck.GetComponent<EventTrigger>();

        if(eventTrigger != null)
        {
             eventTrigger.enabled = true;
        }
    }

   }
}
