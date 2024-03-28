using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EDistanceZone : MonoBehaviour
{
    
    public GameObject ECard21;
    public GameObject ECard22;
    public GameObject ECard23;
    public GameObject ECard42;
    public GameObject ECard53;
    public GameObject ECard54;
    public GameObject WarZone;


    // Start is called before the first frame update
    void Start()
    {
        WarZone = GameObject.Find("EDistanceZone");
    }

    public void PlayCard()
    {
        ECard21.transform.SetParent(WarZone.transform, false);
        ECard21.transform.position = WarZone.transform.position;

        ECard22.transform.SetParent(WarZone.transform, false);
        ECard22.transform.position = WarZone.transform.position;

        ECard23.transform.SetParent(WarZone.transform, false);
        ECard23.transform.position = WarZone.transform.position;

        ECard42.transform.SetParent(WarZone.transform, false);
        ECard42.transform.position = WarZone.transform.position;

        ECard53.transform.SetParent(WarZone.transform, false);
        ECard53.transform.position = WarZone.transform.position;

        ECard54.transform.SetParent(WarZone.transform, false);
        ECard54.transform.position = WarZone.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

