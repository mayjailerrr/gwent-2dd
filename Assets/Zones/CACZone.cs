using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CACZone : MonoBehaviour
{
    public GameObject Card11;
    public GameObject Card12;
    public GameObject Card13;

    public GameObject WarZone;
    // Start is called before the first frame update
    void Start()
    {
        WarZone = GameObject.Find("CACZone");
    }
    public void PlayCard()
    {
        Card11.transform.SetParent(WarZone.transform, false);
        Card11.transform.position = WarZone.transform.position;

        Card12.transform.SetParent(WarZone.transform, false);
        Card12.transform.position = WarZone.transform.position;

        Card13.transform.SetParent(WarZone.transform, false);
        Card13.transform.position = WarZone.transform.position;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
