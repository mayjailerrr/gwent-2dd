using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceZone : MonoBehaviour
{
    public GameObject Card4;
    public GameObject WarZone;
    // Start is called before the first frame update
    void Start()
    {
        WarZone = GameObject.Find("DistanceZone");
    }
    public void PlayCard()
    {
        Card4.transform.SetParent(WarZone.transform, false);
        Card4.transform.position = WarZone.transform.position;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
