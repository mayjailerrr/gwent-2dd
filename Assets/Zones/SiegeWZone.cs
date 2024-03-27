using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiegeWZone : MonoBehaviour
{
    public GameObject Card63;
    public GameObject WarZone;
    // Start is called before the first frame update
    void Start()
    {
        WarZone = GameObject.Find("SiegeWZone");
    }
    public void PlayCard()
    {
        Card63.transform.SetParent(WarZone.transform, false);
        Card63.transform.position = WarZone.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
