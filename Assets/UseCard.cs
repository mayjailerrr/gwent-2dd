using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UseCard : MonoBehaviour
{
    //this is for the player cards
    //CACZone
    public GameObject Card1;
    public GameObject Card2;
    public GameObject Card3;
   

    //variables for setting the position
    public GameObject PCACZone;
    public GameObject PDistanceZone;
    public GameObject PSiegeZone;

    public bool canPlayCard = true;



    //this is for the enemy cards
    //ECACZone

    //EDistanceZone
    //ESiegeZone

    // Start is called before the first frame update
    void Start()
    {
        PCACZone = GameObject.Find("CACZone");
        PDistanceZone = GameObject.Find("DistanceZone");
        PSiegeZone = GameObject.Find("SiegeZone");
    }
    void Update()
    {
        
    }
    public void PlayCard()
    {   
        //CACZone
        if(canPlayCard)
        {
            Card1.transform.SetParent(PCACZone.transform, false);
            Card1.transform.position = PCACZone.transform.position;
            canPlayCard = false;
        }


        //DistanceZone
        else if(canPlayCard)
        {
            Card2.transform.SetParent(PDistanceZone.transform, false);
            Card2.transform.position = PDistanceZone.transform.position;
            canPlayCard = false;
        }


        //SiegeZone
        else if(canPlayCard)
        {
            Card3.transform.SetParent(PSiegeZone.transform, false);
            Card3.transform.position = PSiegeZone.transform.position;
            canPlayCard = false;
        }
        
        
        
    }
        

        

}