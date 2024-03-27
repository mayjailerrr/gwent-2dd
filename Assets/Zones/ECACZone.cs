using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ECACZone : MonoBehaviour
{
    public GameObject CardE11;
    public GameObject WarZone;


    // Start is called before the first frame update
    void Start()
    {
        WarZone = GameObject.Find("ECACZone");
    }

    public void PlayCard()
    {
        CardE11.transform.SetParent(WarZone.transform, false);
        CardE11.transform.position = WarZone.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
