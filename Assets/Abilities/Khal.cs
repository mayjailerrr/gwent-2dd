using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Khal : MonoBehaviour
{
    public Strip Distance;

    public Strip EDistance;

    public bool useful;


    void Start()
    {
        useful = gameObject.GetComponent<MoveCard>().useful;
       
        Distance = GameObject.FindGameObjectWithTag("DistanceZone").GetComponent<Strip>(); 

        EDistance = GameObject.FindGameObjectWithTag("EDistanceZone").GetComponent<Strip>(); 
    }

    public void Attack()
    {
        if(useful && Distance != null && EDistance != null)
        {
            Distance.Khal();
            EDistance.Khal();
        }
    }
}