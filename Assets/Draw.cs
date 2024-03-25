using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour
{
    public GameObject PlayerArea;
    public GameObject Card1;
    public GameObject Card2;
    public GameObject Card3;
    public GameObject Card4;

    List <GameObject> cards = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
       cards.Add(Card1); 
       cards.Add(Card2);
       cards.Add(Card3);
       cards.Add(Card4);
    }

    public void OnClick()
    {
        for(var i = 0; i < 10; i++)
        {
            GameObject playerCard = Instantiate(cards[Random.Range(0, cards.Count)], new Vector2(0,0), Quaternion.identity);
            playerCard.transform.SetParent(PlayerArea.transform, false);
        }
    }

}
