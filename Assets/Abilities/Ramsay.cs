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
            int cac = CAC.AryaStark();
            int d = Distance.AryaStark();
            int s = Siege.AryaStark();

            Siege.Ramsay();
        }
    }

    //         if(cac == 0)
    //         {
    //             int m = Mathf.Min(d,s);

    //             if(m == d)
    //             {
    //                 Distance.Ramsay(m);
    //             }

    //             if(m == s)
    //             {
    //                 Siege.Ramsay(m);
    //             }
    //         }

    //         else if(d == 0)
    //         {
    //             int n = Mathf.Min(cac,s);

    //             if(n == cac)
    //             {
    //                 CAC.Ramsay(n);
    //             }

    //             if(n == s)
    //             {
    //                 Siege.Ramsay(n);
    //             }
    //         }

    //         else if(s == 0)
    //         {
    //             int r = Mathf.Min(cac, d);

    //             if(r == cac)
    //             {
    //                 CAC.Ramsay(r);
    //             }

    //             if(r == d);
    //             {
    //                 Distance.Ramsay(r);
    //             }
    //         }

    //         else if(cac == 0 && d == 0)
    //         {
    //             Siege.Ramsay(s);
    //         }

    //         else if(cac == 0 && s == 0)
    //         {
    //             Distance.Ramsay(d);
    //         }

    //         else if(d == 0 && s == 0)
    //         {
    //             CAC.Ramsay(cac);
    //         }
           

    //     }
    // }
}  
