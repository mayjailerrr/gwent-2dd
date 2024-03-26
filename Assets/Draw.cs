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
    public GameObject Card41;
    public GameObject Card42;
    public GameObject Card43;
    public GameObject Card44;
    public GameObject Card51;
    public GameObject Card52;
    public GameObject Card53;
    public GameObject Card54;
    public GameObject Card55;
    public GameObject Card56;
    public GameObject Card61;
    public GameObject Card62;
    public GameObject Card63;
    public GameObject Card64;


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
       cards.Add(Card41); 
       cards.Add(Card42);
       cards.Add(Card43);
       cards.Add(Card44);
       cards.Add(Card51);
       cards.Add(Card52);
       cards.Add(Card53);
       cards.Add(Card54);
       cards.Add(Card55);
       cards.Add(Card56);
       cards.Add(Card61);
       cards.Add(Card62);
       cards.Add(Card63);
       cards.Add(Card64);
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