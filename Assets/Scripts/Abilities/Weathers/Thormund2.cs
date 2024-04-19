using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thormund2 : MonoBehaviour
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
    }

    public void Attack()
    {
        if(Siege != null && ESiege != null && CAC != null && ECAC != null)
        {
            CAC.Thormund2();
            ECAC.Thormund2();

            Siege.Thormund2();
            ESiege.Thormund2();
        }
    }
}
