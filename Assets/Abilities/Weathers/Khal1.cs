using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Khal1 : MonoBehaviour
{
    public Strip Distance;

    public Strip EDistance;


    void Start()
    {
        Distance = GameObject.FindGameObjectWithTag("DistanceZone").GetComponent<Strip>(); 

        EDistance = GameObject.FindGameObjectWithTag("EDistanceZone").GetComponent<Strip>(); 
    }

    public void Attack()
    {
        if( Distance != null && EDistance != null)
        {
            Distance.Khal1();
            EDistance.Khal1();
        }
    }
}