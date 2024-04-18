using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tyrion : MonoBehaviour
{
    public Strip s;
    public bool useful;

    void Start()
    {
        useful = gameObject.GetComponent<MoveCard>().useful;

        s = GameObject.FindGameObjectWithTag("SiegeZone").GetComponent<Strip>(); 
    }

     public void Attack()
    {
        if(useful)
        {
            s.Lure();
            useful = false;
        }
    }
}
