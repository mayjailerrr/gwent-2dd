using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arya : MonoBehaviour
{
        

    public Strip pCC;
    public Strip pD;
    public Strip pS;

    public Strip eCC;
    public Strip eD;
    public Strip eS;

    public bool useful;
    
    public void Attack()
    {
        if(useful)
        {
            int p1 = pCC.Arya();
            int p2 = pD.Arya();
            int p3 = pS.Arya();
            int p4 = eCC.Arya();
            int p5 = eD.Arya();
            int p6 = eS.Arya();
            int power = (p1 + p2 + p3 + p4 + p5 + p6);

            int a = pCC.AryaStark();
            int b = pD.AryaStark();
            int c =pS.AryaStark();

            int d = eCC.AryaStark();
            int e = eD.AryaStark();
            int f = eS.AryaStark();
            int j = (a + b + c + d + e + f);

            int us = power/j;

            pCC.Replace(us);
            pD.Replace(us);
            pS.Replace(us);

            eCC.Replace(us);
            eD.Replace(us);
            eS.Replace(us);

            useful = false;
        }
    }

    void Update()
    {
       pCC = GameObject.FindGameObjectWithTag("CACZone").GetComponent<Strip>(); 
       pD = GameObject.FindGameObjectWithTag("DistanceZone").GetComponent<Strip>(); 
       pS = GameObject.FindGameObjectWithTag("SiegeZone").GetComponent<Strip>(); 

       eCC = GameObject.FindGameObjectWithTag("ECACZone").GetComponent<Strip>(); 
       eD = GameObject.FindGameObjectWithTag("EDistanceZone").GetComponent<Strip>(); 
       eS = GameObject.FindGameObjectWithTag("ESiegeZone").GetComponent<Strip>(); 

       useful = gameObject.GetComponent<MoveCard>().useful;
    }
}
