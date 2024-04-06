using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsinFrange : MonoBehaviour
{
    public Text Player2Points; 
    public GameObject ECAC;
    public GameObject EDistance;
    public GameObject ESiege;

    private int Player2Addition = 0;
    
    void Update()
    {
        int GameAddition = 0;
        GameAddition = ECAC.GetComponent<ClaseFranja>().Suma + eD.GetComponent<ClaseFranja>().Suma + ESiege.GetComponent<ClaseFranja>().Suma;
        Player2Addition = GameAddition;
        Player2Points.Text = Player1Points.ToString();  
    }
     
}
