using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tyrion2 : MonoBehaviour
{
    public Strip es;
    public bool useful;

    void Start()
    {
        useful = gameObject.GetComponent<MoveCard>().useful;

        es = GameObject.FindGameObjectWithTag("ESiegeZone").GetComponent<Strip>();
    }

     public void Attack()
    {
        if(useful)
        {
            es.Lure();
            useful = false;
        }
    }
}
