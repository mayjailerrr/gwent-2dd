using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rhaegal : MonoBehaviour
{
    public Strip distance;
    public bool useful;

    public void Attack()
    {
        if(useful)
        {
            distance.Rhaegal();
        }
    }

    void Update()
    {
        distance = GameObject.FindGameObjectWithTag("EDistanceZone").GetComponent<Strip>();

        useful = gameObject.GetComponent<MoveCard>().useful;
    }
}
