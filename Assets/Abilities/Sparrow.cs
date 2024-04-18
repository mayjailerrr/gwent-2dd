using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sparrow : MonoBehaviour
{
    public Strip cac;
    public Strip d;
    public Strip s;

    public Strip ecac;
    public Strip ed;
    public Strip es;

    public Weather cacW;
    public Weather dW;
    public Weather sW;

    public Weather ecacW;
    public Weather edW;
    public Weather esW;

    public bool useful;


    void Start()
    {
       useful = gameObject.GetComponent<MoveCard>().useful;

       cac = GameObject.FindGameObjectWithTag("CACZone").GetComponent<Strip>(); 
       d = GameObject.FindGameObjectWithTag("DistanceZone").GetComponent<Strip>(); 
       s = GameObject.FindGameObjectWithTag("SiegeZone").GetComponent<Strip>(); 

       ecac = GameObject.FindGameObjectWithTag("ECACZone").GetComponent<Strip>(); 
       ed = GameObject.FindGameObjectWithTag("EDistanceZone").GetComponent<Strip>(); 
       es = GameObject.FindGameObjectWithTag("ESiegeZone").GetComponent<Strip>();  

       cacW = GameObject.FindGameObjectWithTag("CACWZone").GetComponent<Weather>(); 
       dW = GameObject.FindGameObjectWithTag("DistanceWZone").GetComponent<Weather>(); 
       sW = GameObject.FindGameObjectWithTag("SiegeWZone").GetComponent<Weather>(); 

       ecacW = GameObject.FindGameObjectWithTag("ECACWZone").GetComponent<Weather>(); 
       edW = GameObject.FindGameObjectWithTag("EDistanceWZone").GetComponent<Weather>(); 
       esW = GameObject.FindGameObjectWithTag("ESiegeWZone").GetComponent<Weather>(); 
    }


    public void Attack()
    {
        if(useful)
        {
            cac.Sparrow();
            ecac.Sparrow();
            cacW.Clean();
            ecacW.Clean();
        
            d.Sparrow();
            ed.Sparrow();
            dW.Clean();
            edW.Clean();
        
            s.Sparrow();
            es.Sparrow();
            sW.Clean();
            esW.Clean();

            useful = false;
        }
    }

}
