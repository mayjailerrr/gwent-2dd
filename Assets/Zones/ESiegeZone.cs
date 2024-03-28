using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESiegeZone : MonoBehaviour
{
    public GameObject ECard31;
    public GameObject ECard32;
    public GameObject ECard33;
    public GameObject ECard43;
    public GameObject ECard55;
    public GameObject ECard56;

    public GameObject WarZone;


    // Start is called before the first frame update
    void Start()
    {
        WarZone = GameObject.Find("ESiegeZone");
    }

    public void PlayCard()
    {
        ECard31.transform.SetParent(WarZone.transform, false);
        ECard31.transform.position = WarZone.transform.position;

        ECard32.transform.SetParent(WarZone.transform, false);
        ECard32.transform.position = WarZone.transform.position;

        ECard33.transform.SetParent(WarZone.transform, false);
        ECard33.transform.position = WarZone.transform.position;

        ECard43.transform.SetParent(WarZone.transform, false);
        ECard43.transform.position = WarZone.transform.position;

        ECard55.transform.SetParent(WarZone.transform, false);
        ECard55.transform.position = WarZone.transform.position;

        ECard56.transform.SetParent(WarZone.transform, false);
        ECard56.transform.position = WarZone.transform.position;


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
