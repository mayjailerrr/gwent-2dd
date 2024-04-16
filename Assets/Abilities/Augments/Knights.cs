using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knights : MonoBehaviour
{
    public Strip Siege;
    public Strip ESiege;

    public bool useful;

    void Update()
    {
        useful = gameObject.GetComponent<MoveCard>().useful;

        Siege = GameObject.FindGameObjectWithTag("SiegeZone").GetComponent<Strip>();
        ESiege = GameObject.FindGameObjectWithTag("ESiegeZone").GetComponent<Strip>(); 
    }

    public void Attack()
    {
        if(useful && gameObject.GetComponent<CardModel>().Faction == "Cloud Of Fraternity")
        {
            Siege.Knights();
        }

        if(useful && gameObject.GetComponent<CardModel>().Faction == "Reign Of Punishment")
        {
            ESiege.Knights();
        }
    }
}
