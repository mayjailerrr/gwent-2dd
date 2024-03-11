using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour
{
    public GameObject Card1;
    public GameObject Card2;
    public GameObject MyHand;
    public GameObject EnemyHand;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnClick()
    {
        for(var i = 0; i < 10; i++)
        {
            GameObject playerCard = Instantiate(Card1, new Vector3(0, 0, 0), Quaternion.identity);
            playerCard.transform.SetParent(MyHand.transform, false);
        }
    }
}
