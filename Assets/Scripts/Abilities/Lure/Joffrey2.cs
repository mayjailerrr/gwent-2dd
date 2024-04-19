using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joffrey2 : MonoBehaviour
{
    public Strip ecac;
    public bool useful;

    void Start()
    {
        useful = gameObject.GetComponent<MoveCard>().useful;

        ecac = GameObject.FindGameObjectWithTag("ECACZone").GetComponent<Strip>(); 
    }

     public void Attack()
    {
        if(useful)
        {
            ecac.Lure();
            useful = false;
        }
    }
}
