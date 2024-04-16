using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rhaegal : MonoBehaviour
{
    public bool useful;

    public Strip d;

    public GameObject Card53;

    public GameObject eWarZone;

    void Start()
    {
        useful = gameObject.GetComponent<MoveCard>().useful;

        d = GameObject.FindGameObjectWithTag("EDistanceZone").GetComponent<Strip>();

        eWarZone = GameObject.Find("EDistanceZone");
    }

    public void Attack()
    {
        if(useful)
        {
            GameObject newArmy = Instantiate(Card53, eWarZone.transform);
            newArmy.transform.SetParent(eWarZone.transform, false);
            newArmy.transform.position = eWarZone.transform.position;

            d.Targaryen();

            useful = false;

            gameObject.GetComponent<CardModel>().Power += 5;
        }
    }
}
