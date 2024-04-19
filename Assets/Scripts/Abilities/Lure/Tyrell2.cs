using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tyrell2 : MonoBehaviour
{
    public Strip ed;
    public bool useful;

    void Start()
    {
        useful = gameObject.GetComponent<MoveCard>().useful;

        ed = GameObject.FindGameObjectWithTag("EDistanceZone").GetComponent<Strip>(); 
    }

     public void Attack()
    {
        if(useful)
        {
            ed.Lure();
            useful = false;
        }
    }
}
