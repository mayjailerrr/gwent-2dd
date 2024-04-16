using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Strip : MonoBehaviour
{
    private GameObject CardEntry;
    public List<GameObject> CardsInStripe;
    public int Plus = 0;
    public TextMeshProUGUI punctuation;
    public string Faction;
    public int Stripe;

    public GameObject PlayerGraveyard;
    public GameObject EnemyGraveyard;
    public GameObject PlayerArea;
    public GameObject EnemyArea;

    public GameObject Card12;
    public GameObject Card53;
    public GameObject Card61;

    public bool used = false;

    // private int RoundChecker = 1;
    private int Round = 1;
    private int PartialPlus = 0;


    void Start()
    {
        // GameObject cc = GameObject.FindGam("CACZone");
        // GameObject d = GameObject.Find("DistanceZone");
        // GameObject s = GameObject.Find("SiegeZone");

        // GameObject ecc = GameObject.Find("ECACZone");
        // GameObject ed = GameObject.Find("EDistanceZone");
        // GameObject es = GameObject.Find("ESiegeZone");
    }

    private void OnCollisionEnter2D(Collision2D collision) //when collision it sends the cards in the list of the strip
    {
        CardEntry = collision.gameObject;
        CardsInStripe.Add(CardEntry);
    }


    


    public void Update()
    {
        Round = GameObject.Find("GameManager").GetComponent<GameManager>().Round;
        PlayerArea = GameObject.Find("PlayerArea");
        EnemyArea = GameObject.Find("EnemyArea");


        PartialPlus = 0;
        for(int i = 0; i < CardsInStripe.Count; i++) // Plus
        {
            PartialPlus += CardsInStripe[i].GetComponent<CardModel>().Power;
        }
        Plus = PartialPlus;
        punctuation.text = Plus.ToString();  //ends the Plus
        


        // // //when the round has finished it restarts everything and the cards go to their proper graveyard
        // if(RoundChecker != Round)
        // {
        //     RoundChecker = Round;
        //     if(Faction == "Cloud Of Fraternity")
        //     {
        //         foreach(GameObject Card in CardsInStripe)
        //         {
        //             Card.transform.SetParent(PlayerGraveyard.transform, true);
        //             Card.transform.position = PlayerGraveyard.transform.position;
        //         }
        //         CardsInStripe.Clear();
        //         Plus = 0;
        //         punctuation.text = Plus.ToString();
        //     }

        //     if(Faction == "Reign Of Punishment")
        //     {
        //         foreach(GameObject Card in CardsInStripe)
        //         {
        //             Card.transform.SetParent(EnemyGraveyard.transform, true);
        //             Card.transform.position = EnemyGraveyard.transform.position;
        //         }
        //         CardsInStripe.Clear();
        //         Plus = 0;
        //         punctuation.text = Plus.ToString();
        //     }
        // }

    }


    // augment cards
    public void Cersei()
    {
        foreach(GameObject Card in CardsInStripe)
        {
            if(Card.GetComponent<CardModel>().TypeOfCard != "Gold")
            {
                Card.GetComponent<CardModel>().Power +=2;
            }
            
        }
    }


    public void Huargos()
    {
        foreach(GameObject Card in CardsInStripe)
        {
            if(Card.GetComponent<CardModel>().TypeOfCard != "Gold")
            {
                Card.GetComponent<CardModel>().Power +=2;
            }
        }
    }

    public void Wildlings()
    {
        foreach(GameObject Card in CardsInStripe)
        {
            if(Card.GetComponent<CardModel>().TypeOfCard != "Gold")
            {
                Card.GetComponent<CardModel>().Power +=4;
            }
        }
    }

    public void Knights()
    {
        foreach(GameObject Card in CardsInStripe)
        {
            if(Card.GetComponent<CardModel>().TypeOfCard != "Gold")
            {
                Card.GetComponent<CardModel>().Power +=4;
            }
        }
    }

    public void Targaryen()
    {
        foreach(GameObject Card in CardsInStripe)
        {
            if(Card.GetComponent<CardModel>().TypeOfCard != "Gold")
            {
                Card.GetComponent<CardModel>().Power +=5;
            }
        }
    }

    public void Valyrian()
    {
        foreach(GameObject Card in CardsInStripe)
        {
            if(Card.GetComponent<CardModel>().TypeOfCard != "Gold")
            {
                Card.GetComponent<CardModel>().Power +=3;
            }
        }
    }

    //1st of the list of effects: put an augment in a line
    public void Rhaegal()
    {
        if(used == false)
        {
            GameObject eWarZone = GameObject.Find("EDistanceZone");

            GameObject newArmy = Instantiate(Card53, eWarZone.transform);
            newArmy.transform.SetParent(eWarZone.transform, false);
            newArmy.transform.position = eWarZone.transform.position;
        }
        used = true;
    } 

    //2nd: put a weather
    public void Melisandre()
    {    
        if(used == false)
        {
            GameObject eWarZone1 = GameObject.Find("CACWZone");

            GameObject newJohn = Instantiate(Card61, eWarZone1.transform);
            newJohn.transform.SetParent(eWarZone1.transform, false);
            newJohn.transform.position = eWarZone1.transform.position;
        } 
        used = true;
    }

    //3rd: eliminate the card with more power from both areas 
    public void Viserion()
    {
        // if(CardsInStripe.Count == 1 || CardsInStripe.Count > 1)
        // {
        //     int highest = CardsInStripe[0].GetComponent<CardModel>().Power;

        //     for(int i = 0; i < CardsInStripe.Count; i++)
        //     {
        //         highest = Mathf.Max(highest, CardsInStripe[i].GetComponent<CardModel>().Power);
        //     }

        //     foreach(GameObject Card in CardsInStripe)
        //     {
        //         if(Card.GetComponent<CardModel>().Power == highest && Card.GetComponent<CardModel>().TypeOfCard != "Gold")
        //         {
        //             Card.transform.position = EnemyGraveyard.transform.position;
        //             Card.transform.SetParent(EnemyGraveyard.transform, true);
        //             CardsInStripe.Remove(card);
        //             break;
        //         }
        //     }
        
        // }

        if(CardsInStripe.Count == 1 || CardsInStripe.Count > 1)
        {
            int highest = CardsInStripe[0].GetComponent<CardModel>().Power;

            for(int i = 0; i < CardsInStripe.Count; i++)
            {
                highest = Mathf.Max(highest, CardsInStripe[i].GetComponent<CardModel>().Power);
            }

            foreach(GameObject card in CardsInStripe)
            {
                if(card.GetComponent<CardModel>().Power == highest && card.GetComponent<CardModel>().Faction == "Cloud Of Fraternity" && card.GetComponent<CardModel>().TypeOfCard != "Gold")
                {
                    card.transform.SetParent(PlayerArea.transform, false);
                    card.transform.position = PlayerArea.transform.position;
                    card.GetComponent<MoveCard>().useful = true;
                    CardsInStripe.Remove(card);
                }

                else if(card.GetComponent<CardModel>().Power == highest && card.GetComponent<CardModel>().Faction == "Reign Of Punishment" && card.GetComponent<CardModel>().TypeOfCard != "Gold")
                {
                    card.transform.SetParent(EnemyArea.transform, false);
                    card.transform.position = EnemyArea.transform.position;
                    card.GetComponent<MoveCard>().useful = true;
                    CardsInStripe.Remove(card);
                }
                break;
            }
        }
    }

    //4th effect of the list: eliminate the card with less power from the enemy area
    public void RedKeep()
    {
        if(Faction == "Cloud Of Fraternity")
        {
            if(CardsInStripe.Count == 1 || CardsInStripe.Count > 1)
            {
                int lowest = CardsInStripe[0].GetComponent<CardModel>().Power;

                for(int i = 0; i < CardsInStripe.Count; i++)
                {
                    lowest = Mathf.Min(lowest, CardsInStripe[i].GetComponent<CardModel>().Power);
                }

                foreach(GameObject card in CardsInStripe)
                {
                    if(card.GetComponent<CardModel>().Power == lowest && card.GetComponent<CardModel>().TypeOfCard != "Gold")
                    {
                        card.transform.position = PlayerGraveyard.transform.position;
                        card.transform.SetParent(PlayerGraveyard.transform, true);
                        CardsInStripe.Remove(card);
                        break;
                    }
                }
            }
        }
    }

    //5th: steal a card is in the Draw script
   
    //6th: multiplies by n the power, being n the number of cards that are the same in thee field
    public int Jaime()
    {
        int count = 1;

        if(CardsInStripe.Count == 1 || CardsInStripe.Count > 1)
        {
            foreach(GameObject card in CardsInStripe)
            {
                if(card.GetComponent<CardModel>().Name == "Jaime Lannister")
                {
                    count +=1;
                }
            }
        }
        return count;


        // int count = 1;
        // int power = 7;
        // GameObject player1 = GameObject.FindGameObjectWithTag("CACZone");

        // foreach (GameObject Card in CardsInStripe)
        // {
        //     //the effect is used in the moment
        //     if(Card.GetComponent<CardModel>().Power == 7)
        //     {
        //         count += 2;
        //     }
        //      power *= count;

        //     Card12.GetComponent<CardModel>().Power = power; 
        //     Card12.transform.SetParent(player1.transform, false);
        //     Card12.transform.position = player1.transform.position;
        //     Debug.Log(power);
        // }
           
    }

    //7th: cleans the line with less cards from the field
    public void Ramsay()
    {
        GameObject player = GameObject.Find("PlayerGraveyard");

        foreach(GameObject card in CardsInStripe)
        {
            if(card.GetComponent<CardModel>().TypeOfCard != "Gold")
            {
                card.transform.SetParent(player.transform, false);
                card.transform.position = player.transform.position;
                card.GetComponent<CardModel>().Power = 0;
                card.GetComponent<CardModel>().PurePower = 0;
                
            }
        }
    }

    //8th: calculate the average, then equalize the power to the average (own field)
    public int Arya()
    {
        int sum = 0;
       
        foreach(GameObject card in CardsInStripe)
        {
            sum += card.GetComponent<CardModel>().Power;
        }
        
        return sum;
    }

    public int AryaStark()
    {
        int j = 0;
        j = CardsInStripe.Count;
        return j;
    }

    public void Replace(int us)
    {
        foreach(GameObject card in CardsInStripe)
        {
            if(card.GetComponent<CardModel>().TypeOfCard != "Gold")
            {
                card.GetComponent<CardModel>().Power = us;
            }
        }
    }


    //CLEARANCE
    public void Sparrow()
    {
        foreach(GameObject card in CardsInStripe)
        {
            if(card.GetComponent<CardModel>().TypeOfCard != "Gold" && card.GetComponent<CardModel>().UnderAttackJM1 == true)
            {
                card.GetComponent<CardModel>().UnderAttackJM1 = false;
                card.GetComponent<CardModel>().Power = card.GetComponent<CardModel>().PurePower;
            }

             if(card.GetComponent<CardModel>().TypeOfCard != "Gold" && card.GetComponent<CardModel>().UnderAttackJM2 == true)
            {
                card.GetComponent<CardModel>().UnderAttackJM2 = false;
                card.GetComponent<CardModel>().Power = card.GetComponent<CardModel>().PurePower;
            }

            if(card.GetComponent<CardModel>().TypeOfCard != "Gold" && card.GetComponent<CardModel>().UnderAttackJ1 == true)
            {
                card.GetComponent<CardModel>().UnderAttackJ1 = false;
                card.GetComponent<CardModel>().Power = card.GetComponent<CardModel>().PurePower;
            }

            if(card.GetComponent<CardModel>().TypeOfCard != "Gold" && card.GetComponent<CardModel>().UnderAttackJ2 == true)
            {
                card.GetComponent<CardModel>().UnderAttackJ2 = false;
                card.GetComponent<CardModel>().Power = card.GetComponent<CardModel>().PurePower;
            }

            if(card.GetComponent<CardModel>().TypeOfCard != "Gold" && card.GetComponent<CardModel>().UnderAttackK1 == true)
            {
                card.GetComponent<CardModel>().UnderAttackK1 = false;
                card.GetComponent<CardModel>().Power = card.GetComponent<CardModel>().PurePower;
            }

             if(card.GetComponent<CardModel>().TypeOfCard != "Gold" && card.GetComponent<CardModel>().UnderAttackK2 == true)
            {
                card.GetComponent<CardModel>().UnderAttackK2 = false;
                card.GetComponent<CardModel>().Power = card.GetComponent<CardModel>().PurePower;
            }

            if(card.GetComponent<CardModel>().TypeOfCard != "Gold" && card.GetComponent<CardModel>().UnderAttackT1 == true)
            {
                card.GetComponent<CardModel>().UnderAttackT1 = false;
                card.GetComponent<CardModel>().Power = card.GetComponent<CardModel>().PurePower;
            }

             if(card.GetComponent<CardModel>().TypeOfCard != "Gold" && card.GetComponent<CardModel>().UnderAttackT2 == true)
            {
                card.GetComponent<CardModel>().UnderAttackT2 = false;
                card.GetComponent<CardModel>().Power = card.GetComponent<CardModel>().PurePower;
            }

            if(card.GetComponent<CardModel>().TypeOfPower == "Weather")
            {
                CardsInStripe.Remove(card);
            }
        }
    }


    //WEATHERS
    public void Khal1()
    {
        foreach(GameObject card in CardsInStripe)
        {
            if(card.GetComponent<CardModel>().TypeOfCard != "Gold" && card.GetComponent<CardModel>().UnderAttackK1 == false)
            {
                card.GetComponent<CardModel>().UnderAttackK1 = true;
                card.GetComponent<CardModel>().Power -= 5;
            }
        }
    }

    public void Khal2()
    {
        foreach(GameObject card in CardsInStripe)
        {
            if(card.GetComponent<CardModel>().TypeOfCard != "Gold" && card.GetComponent<CardModel>().UnderAttackK2 == false)
            {
                card.GetComponent<CardModel>().UnderAttackK2 = true;
                card.GetComponent<CardModel>().Power -= 5;
            }
        }
    }

    public void John1()
    {
        foreach(GameObject card in CardsInStripe)
        {
            if(card.GetComponent<CardModel>().TypeOfCard != "Gold" && card.GetComponent<CardModel>().UnderAttackJ1 == false)
            {
                card.GetComponent<CardModel>().UnderAttackJ1 = true;
                card.GetComponent<CardModel>().Power -= 5;
            }
        }
    }

     public void John2()
    {
        foreach(GameObject card in CardsInStripe)
        {
            if(card.GetComponent<CardModel>().TypeOfCard != "Gold" && card.GetComponent<CardModel>().UnderAttackJ2 == false)
            {
                card.GetComponent<CardModel>().UnderAttackJ2 = true;
                card.GetComponent<CardModel>().Power -= 5;
            }
        }
    }

     public void JohnM1()
    {
        foreach(GameObject card in CardsInStripe)
        {
            if(card.GetComponent<CardModel>().TypeOfCard != "Gold" && card.GetComponent<CardModel>().UnderAttackJM1 == false)
            {
                card.GetComponent<CardModel>().UnderAttackJM1 = true;
                card.GetComponent<CardModel>().Power -= 5;
            }
        }
    }

     public void JohnM2()
    {
        foreach(GameObject card in CardsInStripe)
        {
            if(card.GetComponent<CardModel>().TypeOfCard != "Gold" && card.GetComponent<CardModel>().UnderAttackJM2 == false)
            {
                card.GetComponent<CardModel>().UnderAttackJM2 = true;
                card.GetComponent<CardModel>().Power -= 5;
            }
        }
    }

    public void Thormund1()
    {
        foreach(GameObject card in CardsInStripe)
        {
            if(card.GetComponent<CardModel>().TypeOfCard != "Gold" && card.GetComponent<CardModel>().UnderAttackT1 == false)
            {
                card.GetComponent<CardModel>().UnderAttackT1 = true;
                card.GetComponent<CardModel>().Power -= 4;
            }
        }
    }

    public void Thormund2()
    {
        foreach(GameObject card in CardsInStripe)
        {
            if(card.GetComponent<CardModel>().TypeOfCard != "Gold" && card.GetComponent<CardModel>().UnderAttackT2 == false)
            {
                card.GetComponent<CardModel>().UnderAttackT2 = true;
                card.GetComponent<CardModel>().Power -= 4;
            }
        }
    }
    
}
