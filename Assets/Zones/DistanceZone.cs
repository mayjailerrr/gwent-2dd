using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceZone : MonoBehaviour
{
    public GameObject Card21;
    public GameObject Card22;
    public GameObject Card23;
    public GameObject WarZone;
    // Start is called before the first frame update
    void Start()
    {
        WarZone = GameObject.Find("DistanceZone");
    }
    public void PlayCard()
    {
        Card21.transform.SetParent(WarZone.transform, false);
        Card21.transform.position = WarZone.transform.position;

        Card22.transform.SetParent(WarZone.transform, false);
        Card22.transform.position = WarZone.transform.position;

        Card23.transform.SetParent(WarZone.transform, false);
        Card23.transform.position = WarZone.transform.position;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
