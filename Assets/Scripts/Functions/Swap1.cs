using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Swap1 : MonoBehaviour
{
    public GameObject swap1;
    public GameObject swap2;

    public bool player1 = false;
    public bool player2 = false;


    public void Button1()
    {
        if(player1 == false)
        {
            player1 = true;
            swap1.gameObject.SetActive(false);
        }
    }

    public void Button2()
    {
        if(player2 == false)
        {
            player2 = true;
            swap2.gameObject.SetActive(false);
        }
    }

   
}
