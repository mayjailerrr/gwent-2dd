using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ECACWZone : MonoBehaviour
{
    public GameObject ECard61;
    public GameObject WarZone;
    // Start is called before the first frame update
    void Start()
    {
        WarZone = GameObject.Find("ECACWZone");
    }
    public void PlayCard()
    {
        ECard61.transform.SetParent(WarZone.transform, false);
        ECard61.transform.position = WarZone.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
