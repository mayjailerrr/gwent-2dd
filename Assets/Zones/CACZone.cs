using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CACZone : MonoBehaviour
{
    public GameObject Card11;
    public GameObject Card12;
    public GameObject Card13;
    public GameObject Card41;
    public GameObject Card51;
    public GameObject Card52;
    public GameObject Card61;

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

        Card41.transform.SetParent(WarZone.transform, false);
        Card41.transform.position = WarZone.transform.position;

        Card51.transform.SetParent(WarZone.transform, false);
        Card51.transform.position = WarZone.transform.position;

        Card52.transform.SetParent(WarZone.transform, false);
        Card52.transform.position = WarZone.transform.position;

        Card61.transform.SetParent(WarZone.transform, false);
        Card61.transform.position = WarZone.transform.position;

    }
    // Update is called once per frame
    void Update()
    {

    }
}
