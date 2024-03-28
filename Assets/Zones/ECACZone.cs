using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ECACZone : MonoBehaviour
{
    public GameObject ECard11;
    public GameObject ECard12;
    public GameObject ECard13;
    public GameObject ECard41;
    public GameObject ECard51;
    public GameObject ECard52;
    public GameObject WarZone;


    // Start is called before the first frame update
    void Start()
    {
        WarZone = GameObject.Find("ECACZone");
    }

    public void PlayCard()
    {
        ECard11.transform.SetParent(WarZone.transform, false);
        ECard11.transform.position = WarZone.transform.position;

        ECard12.transform.SetParent(WarZone.transform, false);
        ECard12.transform.position = WarZone.transform.position;

        ECard13.transform.SetParent(WarZone.transform, false);
        ECard13.transform.position = WarZone.transform.position;

        ECard41.transform.SetParent(WarZone.transform, false);
        ECard41.transform.position = WarZone.transform.position;

        ECard51.transform.SetParent(WarZone.transform, false);
        ECard51.transform.position = WarZone.transform.position;

        ECard52.transform.SetParent(WarZone.transform, false);
        ECard52.transform.position = WarZone.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
