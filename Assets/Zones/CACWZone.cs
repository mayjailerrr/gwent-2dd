using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CACWZone : MonoBehaviour
{
    public GameObject Card61;
    public GameObject WarZone;
    // Start is called before the first frame update
    void Start()
    {
        WarZone = GameObject.Find("CACWZone");
    }
    public void PlayCard()
    {
        Card61.transform.SetParent(WarZone.transform, false);
        Card61.transform.position = WarZone.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
