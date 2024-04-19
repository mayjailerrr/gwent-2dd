using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedKeep : MonoBehaviour
{
    public Strip CAC;
    public Strip Distance;
    public Strip Siege;

    private bool useful;

    void Start()
    {
        
       useful = gameObject.GetComponent<MoveCard>().useful;

       CAC = GameObject.FindGameObjectWithTag("CACZone").GetComponent<Strip>(); 
       Distance = GameObject.FindGameObjectWithTag("DistanceZone").GetComponent<Strip>(); 
       Siege = GameObject.FindGameObjectWithTag("SiegeZone").GetComponent<Strip>(); 
    }


    public void Attack()
    {
        if(useful)
        {
            useful = false;
            CAC.RedKeep();
            Distance.RedKeep();
            Siege.RedKeep();
        }
    }
}
