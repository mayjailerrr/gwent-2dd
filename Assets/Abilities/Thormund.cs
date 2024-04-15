using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thormund : MonoBehaviour
{
    public bool useful;

    public Strip CAC;
    public Strip Siege;

    public Strip ECAC;
    public Strip ESiege;

    void Update()
    {
        CAC = GameObject.FindGameObjectWithTag("CACZone").GetComponent<Strip>();
        Siege = GameObject.FindGameObjectWithTag("SiegeZone").GetComponent<Strip>();

        ECAC = GameObject.FindGameObjectWithTag("ECACZone").GetComponent<Strip>();
        ESiege = GameObject.FindGameObjectWithTag("ESiegeZone").GetComponent<Strip>();

        useful = gameObject.GetComponent<MoveCard>().useful;
    }

    public void Attack()
    {
        if(useful && Siege != null && ESiege != null && CAC != null && ECAC != null)
        {
            CAC.Thormund();
            ECAC.Thormund();

            Siege.Thormund();
            ESiege.Thormund();
        }
    }
}
