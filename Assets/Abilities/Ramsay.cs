using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ramsay : MonoBehaviour
{
    public bool useful;

    public Strip CAC;
    public Strip Distance;
    public Strip Siege;    


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
            int cac = CAC.CardsInStripe.Count;
            int d = Distance.CardsInStripe.Count;
            int s = Siege.CardsInStripe.Count;

            if(cac != 0 && d != 0 && s != 0 && cac > d && cac > s && d > s)
            {
                int minor = s;
                Siege.Ramsay(minor);
            } 

            else if(cac != 0 && d != 0 && s != 0 && d > cac && d > s && s > cac)
            {
                int minor = cac;
                CAC.Ramsay(minor);
            }

             else if(cac != 0 && d != 0 && s != 0 && s > cac && s > d && cac > d)
            {
                int minor = d;
                Distance.Ramsay(minor);
            }

            //if some are equals
             else if(cac != 0 && d != 0 && s != 0 && cac > d && cac > s && d == s)
            {
                int minor = d;
                Distance.Ramsay(minor);
            }

             else if(cac != 0 && d != 0 && s != 0 && d > cac && d > s && cac == s)
            {
                int minor = s;
                Siege.Ramsay(minor);
            }

             else if(cac != 0 && d != 0 && s != 0 && s > cac && s > d && cac == d)
            {
                int minor = cac;
                CAC.Ramsay(minor);
            }
        }
    }    
}
