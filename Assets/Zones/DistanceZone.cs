using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceZone : MonoBehaviour
{
    public GameObject Card21;
    public GameObject Card22;
    public GameObject Card23;
    public GameObject Card42;
    public GameObject Card53;
    public GameObject Card54;

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

        Card42.transform.SetParent(WarZone.transform, false);
        Card42.transform.position = WarZone.transform.position;

        Card53.transform.SetParent(WarZone.transform, false);
        Card53.transform.position = WarZone.transform.position;

        Card54.transform.SetParent(WarZone.transform, false);
        Card54.transform.position = WarZone.transform.position;

    }
    // Update is called once per frame
    void Update()
    {

    }
}
