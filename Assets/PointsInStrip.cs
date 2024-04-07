using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Text;
using TMPro;

public class PointsInStrip : MonoBehaviour
{
    public Text EnemyPoints; 
    public GameObject ECAC1;
    public GameObject EDistance1;
    public GameObject ESiege1;

    private int EnemyAddition = 0;
    
    void Update()
    {
        int GameAddition = 0;
        GameAddition = ECAC1.GetComponent<Strip>().Plus + EDistance1.GetComponent<Strip>().Plus + ESiege1.GetComponent<Strip>().Plus;
        EnemyAddition = GameAddition;
        EnemyPoints.text = EnemyPoints.ToString();  
    }
     
}
