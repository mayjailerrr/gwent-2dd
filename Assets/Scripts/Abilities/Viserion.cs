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
    public int highest;

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

            int val1 = CAC.Viserion();
            int val2 = Distance.Viserion();
            int val3 = Siege.Viserion();

            int val4 = ECAC.Viserion();
            int val5 = EDistance.Viserion();
            int val6 = ESiege.Viserion();

            highest = Mathf.Max(val1, Mathf.Max(val2, Mathf.Max(val3, Mathf.Max(val4, Mathf.Max(val5, val6)))));

            if(highest == val1)
            {
                CAC.Viserion2(highest);
                return;
            }
            else if(highest == val2)
            {
                Distance.Viserion2(highest);
                return;
            }
            else if(highest == val3)
            {
                Siege.Viserion2(highest);
                return;
            }
            else if(highest == val4)
            {
                ECAC.Viserion2(highest);
                return;
            }
            else if(highest == val5)
            {
                EDistance.Viserion2(highest);
                return;
            }
            else if(highest == val6)
            {
                ESiege.Viserion2(highest);
                return;
            }
        }
    }
    
}

