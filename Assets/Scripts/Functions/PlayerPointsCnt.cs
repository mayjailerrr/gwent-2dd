using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerPointsCnt : MonoBehaviour
{
    public TextMeshProUGUI PlayerPoints;
    public GameObject CAC;
    public GameObject Distance;
    public GameObject Siege;
   
    private int PlayerAddition = 0;

    void Update()
    {
        int GameAddition = 0;
        GameAddition = CAC.GetComponent<Strip>().Plus + Distance.GetComponent<Strip>().Plus + Siege.GetComponent<Strip>().Plus;
        PlayerAddition = GameAddition;
        PlayerPoints.text = PlayerAddition.ToString();
    }
}
