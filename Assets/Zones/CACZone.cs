using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    public GameObject Card2;
    public GameObject WarZone;
    // Start is called before the first frame update
    void Start()
    {
        WarZone = GameObject.Find("CACZone");
    }
    public void PlayCard()
    {
        Card2.transform.SetParent(WarZone.transform, false);
        Card2.transform.position = WarZone.transform.position;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
