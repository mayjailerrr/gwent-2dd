using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESiegeWZone : MonoBehaviour
{
    public GameObject ECard63;
    public GameObject WarZone;
    // Start is called before the first frame update
    void Start()
    {
        WarZone = GameObject.Find("ESiegeWZone");
    }
    public void PlayCard()
    {
        ECard63.transform.SetParent(WarZone.transform, false);
        ECard63.transform.position = WarZone.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
