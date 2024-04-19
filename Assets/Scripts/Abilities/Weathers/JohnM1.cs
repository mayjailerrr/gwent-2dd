using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JohnM1 : MonoBehaviour
{

    public Strip CAC;
    public Strip Distance;
    public Strip Siege;

    public Strip ECAC;
    public Strip EDistance;
    public Strip ESiege;

     void Start()
    {
       
        Distance = GameObject.FindGameObjectWithTag("DistanceZone").GetComponent<Strip>(); 
        CAC = GameObject.FindGameObjectWithTag("CACZone").GetComponent<Strip>();
        Siege = GameObject.FindGameObjectWithTag("SiegeZone").GetComponent<Strip>();

        EDistance = GameObject.FindGameObjectWithTag("EDistanceZone").GetComponent<Strip>(); 
        ECAC = GameObject.FindGameObjectWithTag("ECACZone").GetComponent<Strip>();
        ESiege = GameObject.FindGameObjectWithTag("ESiegeZone").GetComponent<Strip>();
    }

     public void Attack()
    {
        if( Siege != null && ESiege != null && CAC != null && ECAC != null && Distance != null && EDistance != null)
        {
            CAC.JohnM1();
            ECAC.JohnM1();

            Siege.JohnM1();
            ESiege.JohnM1();

            Distance.JohnM1();
            EDistance.JohnM1();
        }
    }
}