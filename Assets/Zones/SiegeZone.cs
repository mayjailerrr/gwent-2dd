using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiegeZone : MonoBehaviour
{
    public GameObject Card3;
    public GameObject WarZone;
    // Start is called before the first frame update
    void Start()
    {
        WarZone = GameObject.Find("SiegeZone");
    }
    public void PlayCard()
    {
        Card3.transform.SetParent(WarZone.transform, false);
        Card3.transform.position = WarZone.transform.position;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
