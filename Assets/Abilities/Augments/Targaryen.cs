using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targaryen : MonoBehaviour
{
    public Strip Distance;
    public Strip EDistance;

    public bool useful;

    void Update()
    {
        useful = gameObject.GetComponent<MoveCard>().useful;

        Distance = GameObject.FindGameObjectWithTag("DistanceZone").GetComponent<Strip>();
        EDistance = GameObject.FindGameObjectWithTag("EDistanceZone").GetComponent<Strip>();
    } 


    public void Attack()
    {
        if(useful && gameObject.GetComponent<CardModel>().Faction == "Cloud Of Fraternity")
        {
            Distance.Targaryen();
        }

        if(useful && gameObject.GetComponent<CardModel>().Faction == "Reign Of Punishment")
        {
            EDistance.Targaryen();
        }
    }
}
