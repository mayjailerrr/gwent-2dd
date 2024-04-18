using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jaime : MonoBehaviour
{
    public bool useful;

    private int power;

    public Strip CAC;

    void Update()
    {
        useful = gameObject.GetComponent<MoveCard>().useful;

        CAC = GameObject.FindGameObjectWithTag("CACZone").GetComponent<Strip>();
    }

    public void Attack()
    {
        if (useful && CAC != null)
        {
            if(gameObject.GetComponent<CardModel>().Name == "Jaime Lannister")
            {
                power = CAC.Jaime();
            }

            gameObject.GetComponent<CardModel>().Power = gameObject.GetComponent<CardModel>().PurePower * power;
            useful = false;
        }
    }

   
}
