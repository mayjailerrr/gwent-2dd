using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rhaegal : MonoBehaviour
{
    public Strip siege;
    public bool useful;

    public void Attack()
    {
        if(useful)
        {
            siege.Rhaegal();
        }
    }

    void Update()
    {
        siege = GameObject.FindGameObjectWithTag("SiegeZone").GetComponent<Strip>();
        useful = gameObject.GetComponent<MoveCard>().useful;
    }
}
