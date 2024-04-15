using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sparrow : MonoBehaviour
{
    public Strip CAC;
    public Strip Distance;
    public Strip Siege;

    public Strip ECAC;
    public Strip EDistance;
    public Strip ESiege;

    public bool useful;


    void Start()
    {
       useful = gameObject.GetComponent<MoveCard>().useful;

       CAC = GameObject.FindGameObjectWithTag("CACZone").GetComponent<Strip>(); 
       Distance = GameObject.FindGameObjectWithTag("DistanceZone").GetComponent<Strip>(); 
       Siege = GameObject.FindGameObjectWithTag("SiegeZone").GetComponent<Strip>(); 

       ECAC = GameObject.FindGameObjectWithTag("ECACZone").GetComponent<Strip>(); 
       EDistance = GameObject.FindGameObjectWithTag("EDistance").GetComponent<Strip>(); 
       ESiege = GameObject.FindGameObjectWithTag("ESiegeZone").GetComponent<Strip>();  
    }


    public void Attack()
    {
        if(useful)
        {
            if(gameObject.GetComponent<CardModel>().Stripe == 1)
            {
                CAC.Sparrow();
                ECAC.Sparrow();
            }

            if(gameObject.GetComponent<CardModel>().Stripe == 2)
            {
                Distance.Sparrow();
                EDistance.Sparrow();
            }

            if(gameObject.GetComponent<CardModel>().Stripe == 3)
            {
                Siege.Sparrow();
                ESiege.Sparrow();
            }
        }
    }

}
