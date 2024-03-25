using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour
{
    public GameObject PlayerArea;
    public GameObject Card11;
    public GameObject Card12;
    public GameObject Card13;
    public GameObject Card21;
    public GameObject Card22;
    public GameObject Card23;
    public GameObject Card31;
    public GameObject Card32;
    public GameObject Card33;

    List <GameObject> cards = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
       cards.Add(Card11); 
       cards.Add(Card12);
       cards.Add(Card13);
       cards.Add(Card21);
       cards.Add(Card22);
       cards.Add(Card23);
       cards.Add(Card31);
       cards.Add(Card32);
       cards.Add(Card33);
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
