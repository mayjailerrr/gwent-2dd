using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lure : MonoBehaviour
{
    public Strip cac;
    public bool useful;

    void Start()
    {
        useful = gameObject.GetComponent<MoveCard>().useful;

        cac = GameObject.FindGameObjectWithTag("CACZone").GetComponent<Strip>(); 
    }

    public void Attack()
    {
        if(useful)
        {
            cac.Lure();
            useful = false;
        }
    }
   
}
