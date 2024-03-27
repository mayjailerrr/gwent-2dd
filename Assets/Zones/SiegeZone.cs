using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiegeZone : MonoBehaviour
{
    public GameObject Card31;
    public GameObject Card32;
    public GameObject Card33;
    public GameObject Card43;
    public GameObject Card55;
    public GameObject Card56;
    public GameObject WarZone;
    // Start is called before the first frame update
    void Start()
    {
        WarZone = GameObject.Find("SiegeZone");
    }
    public void PlayCard()
    {
        Card31.transform.SetParent(WarZone.transform, false);
        Card31.transform.position = WarZone.transform.position;

        Card32.transform.SetParent(WarZone.transform, false);
        Card32.transform.position = WarZone.transform.position;

        Card33.transform.SetParent(WarZone.transform, false);
        Card33.transform.position = WarZone.transform.position;

        Card43.transform.SetParent(WarZone.transform, false);
        Card43.transform.position = WarZone.transform.position;

        Card55.transform.SetParent(WarZone.transform, false);
        Card55.transform.position = WarZone.transform.position;

        Card56.transform.SetParent(WarZone.transform, false);
        Card56.transform.position = WarZone.transform.position;

    }
    // Update is called once per frame
    void Update()
    {

    }
}
