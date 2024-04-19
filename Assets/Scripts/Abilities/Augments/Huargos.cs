using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Huargos : MonoBehaviour
{
    public bool useful;

    public Strip CAC;
    public Strip ECAC;
   
    void Update()
    {
        useful = gameObject.GetComponent<MoveCard>().useful;

        CAC = GameObject.FindGameObjectWithTag("CACZone").GetComponent<Strip>(); 
        ECAC = GameObject.FindGameObjectWithTag("ECACZone").GetComponent<Strip>(); 
    }

    public void Attack()
    {
        if(useful && gameObject.GetComponent<CardModel>().Faction == "Cloud Of Fraternity")
        {
            CAC.Huargos();
        }

        if(useful && gameObject.GetComponent<CardModel>().Faction == "Reign Of Punishment")
        {
            ECAC.Huargos();
        }
    }
}
