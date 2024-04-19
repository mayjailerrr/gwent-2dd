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

    private bool useful;

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
            useful = false;
            CAC.Viserion();
            Distance.Viserion();
            Siege.Viserion();

            ECAC.Viserion();
            EDistance.Viserion();
            ESiege.Viserion();
        }
    }
    
}

