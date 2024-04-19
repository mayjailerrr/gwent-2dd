using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tyrell : MonoBehaviour
{
    public Strip d;
    public bool useful;

    void Start()
    {
        useful = gameObject.GetComponent<MoveCard>().useful;

        d = GameObject.FindGameObjectWithTag("DistanceZone").GetComponent<Strip>();
    }

     public void Attack()
    {
        if(useful)
        {
            d.Lure();
            useful = false;
        }
    }
}
