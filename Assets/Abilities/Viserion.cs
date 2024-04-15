using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viserion : MonoBehaviour
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
       EDistance = GameObject.FindGameObjectWithTag("EDistanceZone").GetComponent<Strip>(); 
       ESiege = GameObject.FindGameObjectWithTag("ESiegeZone").GetComponent<Strip>();
    }


    public void Attack()
    {
        if(useful)
        {
            if(gameObject.GetComponent<CardModel>().Faction == "Cloud Of Fraternity" && gameObject.GetComponent<CardModel>().Stripe == 1)
            {
                CAC.Viserion();
            }

            if(gameObject.GetComponent<CardModel>().Faction == "Cloud Of Fraternity" && gameObject.GetComponent<CardModel>().Stripe == 2)
            {
                Distance.Viserion();
            }

             if(gameObject.GetComponent<CardModel>().Faction == "Cloud Of Fraternity" && gameObject.GetComponent<CardModel>().Stripe == 3)
            {
                Siege.Viserion();
            }

            if(gameObject.GetComponent<CardModel>().Faction == "Reign Of Punishment" && gameObject.GetComponent<CardModel>().Stripe == 1)
            {
                ECAC.Viserion();
            }

            if(gameObject.GetComponent<CardModel>().Faction == "Reign Of Punishment" && gameObject.GetComponent<CardModel>().Stripe == 2)
            {
                EDistance.Viserion();
            }

            if(gameObject.GetComponent<CardModel>().Faction == "Reign Of Punishment" && gameObject.GetComponent<CardModel>().Stripe == 3)
            {
                ESiege.Viserion();
            }
        }
    }

    
}

