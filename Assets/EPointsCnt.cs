
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EPointsCnt : MonoBehaviour
{
    public TextMeshProUGUI EnemyPoints; 
    public GameObject ECAC;
    public GameObject EDistance;
    public GameObject ESiege;

    private int EnemyAddition = 0;
    
    void Update()
    {
        int GameAddition = 0;
        GameAddition = ECAC.GetComponent<Strip>().Plus + EDistance.GetComponent<Strip>().Plus + ESiege.GetComponent<Strip>().Plus;
        EnemyAddition = GameAddition;
        EnemyPoints.text = EnemyAddition.ToString();  
    }
     
}
