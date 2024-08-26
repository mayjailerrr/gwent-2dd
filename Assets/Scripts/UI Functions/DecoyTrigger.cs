using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DecoyTrigger : MonoBehaviour
{
    public void FinishDecoyEvent()
    {
        GameManager.gameManager.FinishDecoyEvent(this.GetComponent<UICard>().MotherCard);
    }
}
