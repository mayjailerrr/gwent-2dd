using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CACZone : MonoBehaviour
{
    public GameObject Card1;
    public GameObject Card2;
    public GameObject WarZone;
    // Start is called before the first frame update
    void Start()
    {
        WarZone = GameObject.Find("CACZone");
    }
    public void PlayCard()
    {
        Card1.transform.SetParent(WarZone.transform, false);
        Card1.transform.position = WarZone.transform.position;

        Card2.transform.SetParent(WarZone.transform, false);
        Card2.transform.position = WarZone.transform.position;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
