using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedKeep : MonoBehaviour
{
    public Strip CAC;
    public Strip Distance;
    public Strip Siege;

    private bool useful;
    public int lowest;

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

            int val1 = CAC.RedKeep();
            int val2 = Distance.RedKeep();
            int val3 = Siege.RedKeep();

            lowest = Mathf.Min(val1, Mathf.Min(val2, val3));

            if(lowest == val1)
            {
                CAC.RedKeep2(lowest);
                return;
            }
            else if(lowest == val2)
            {
                Distance.RedKeep2(lowest);
                return;
            }
            else if(lowest == val3)
            {
                Siege.RedKeep2(lowest);
                return;
            }
        }
    }
}
