using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EDistanceWZone : MonoBehaviour
{
    public GameObject ECard62;
    public GameObject WarZone;
    // Start is called before the first frame update
    void Start()
    {
        WarZone = GameObject.Find("EDistanceWZone");
    }
    public void PlayCard()
    {
        ECard62.transform.SetParent(WarZone.transform, false);
        ECard62.transform.position = WarZone.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
