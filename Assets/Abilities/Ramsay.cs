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

            if(cac != 0 && (d== 0 || cac <= d) && (s == 0 || cac <= s))
            {
                CAC.Ramsay();
            }

            else if(d != 0 && (cac == 0 || d <= cac) && (s == 0 || d <= s))
            {
                Distance.Ramsay();
            }

            else
            {
                Siege.Ramsay();
            }
            useful = false;
        }
    }
}  
