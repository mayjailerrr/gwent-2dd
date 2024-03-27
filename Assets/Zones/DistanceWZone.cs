using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceWZone : MonoBehaviour
{
    public GameObject Card62;
    public GameObject WarZone;
    // Start is called before the first frame update
    void Start()
    {
        WarZone = GameObject.Find("DistanceWZone");
    }
    public void PlayCard()
    {
        Card62.transform.SetParent(WarZone.transform, false);
        Card62.transform.position = WarZone.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
