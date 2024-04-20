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

    public GameObject PlayerGraveyard;
    public GameObject EnemyGraveyard;
    public GameObject PlayerArea;
    public GameObject EnemyArea;

    public GameObject Card61;

    public bool used = false;
    public bool usedV = false;

    private int RoundChecker = 1;
    private int Round = 1;
    private int PartialPlus = 0;


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

        if(RoundChecker != Round)
        {
            GameObject player = GameObject.Find("PlayerGraveyard");
            GameObject enemy = GameObject.Find("EnemyGraveyard");

            foreach(GameObject Card in CardsInStripe)
            {
                if(Card.GetComponent<CardModel>().Faction == "Cloud Of Fraternity")
                {
                    Card.transform.SetParent(player.transform, false);
                    Card.transform.position = player.transform.position;
                    Plus = 0;
                    punctuation.text = Plus.ToString();
                }
            }

            foreach(GameObject Card in CardsInStripe)
            {
                if(Card.GetComponent<CardModel>().Faction == "Reign Of Punishment")
                {
                    Card.transform.SetParent(enemy.transform, false);
                    Card.transform.position = enemy.transform.position;
                    Plus = 0;
                    punctuation.text = Plus.ToString();
                }
            }
                
            CardsInStripe.Clear();
            RoundChecker = Round;
        }

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

    public void Fire()
    {
        foreach(GameObject Card in CardsInStripe)
        {
            if(Card.GetComponent<CardModel>().TypeOfCard != "Gold")
            {
                Card.GetComponent<CardModel>().Power +=3;
            }
        }
    }

    //1st of the list of effects: put an augment in a line (is in the Rhaegal script)
   
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
        GameObject grave = GameObject.Find("PlayerGraveyard");
        GameObject grave2 = GameObject.Find("EnemyGraveyard");

        if (CardsInStripe.Count > 0)
        {
            int highest = CardsInStripe[0].GetComponent<CardModel>().Power;

            foreach (GameObject card in CardsInStripe)
            {
                highest = Mathf.Max(highest, card.GetComponent<CardModel>().Power);
            }

            // Iterate over the list to remove cards with the highest power
            for (int i = CardsInStripe.Count - 1; i >= 0; i--)
            {
                GameObject card = CardsInStripe[i];

                if (card.GetComponent<CardModel>().Power == highest)
                {
                    if(card.GetComponent<CardModel>().TypeOfCard == "Gold")
                    {
                        return;
                    }
                    else if(card.GetComponent<CardModel>().Faction == "Reign Of Punishment")
                    {
                        card.transform.position = grave2.transform.position;
                        card.transform.SetParent(grave2.transform, true);
                    }
                    else if (card.GetComponent<CardModel>().Faction == "Cloud Of Fraternity")
                    {
                        card.transform.position = grave.transform.position;
                        card.transform.SetParent(grave.transform, true);
                    }

                    CardsInStripe.RemoveAt(i);
                }
            }
        }
    }


    //4th effect of the list: eliminate the card with less power from the enemy area

    public void RedKeep()
    {
        GameObject grave = GameObject.Find("PlayerGraveyard");

        if(CardsInStripe.Count == 1 || CardsInStripe.Count > 1)
        {
            int lowest = CardsInStripe[0].GetComponent<CardModel>().Power;

            for(int i = 0; i < CardsInStripe.Count; i++)
            {
                lowest = Mathf.Min(lowest, CardsInStripe[i].GetComponent<CardModel>().Power);
            }

            foreach(GameObject card in CardsInStripe)
            {
                if(card.GetComponent<CardModel>().TypeOfCard == "Gold" && lowest == card.GetComponent<CardModel>().Power)
                {
                    return;
                }
                else if(card.GetComponent<CardModel>().Power == lowest)
                {
                    card.transform.position = grave.transform.position;
                    card.transform.SetParent(grave.transform, true);
                    CardsInStripe.Remove(card);
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
           
    }

    //7th: cleans the line with less cards from the field
    public void Ramsay()
    {
        GameObject player = GameObject.Find("PlayerGraveyard");

        foreach(GameObject card in CardsInStripe)
        {
            if(card.GetComponent<CardModel>().TypeOfCard != "Gold")
            {
                card.transform.SetParent(player.transform, true);
                card.transform.position = player.transform.position;
                card.GetComponent<CardModel>().Power = 0;
                card.GetComponent<CardModel>().PurePower = 0;
            }
            else if(card.GetComponent<CardModel>().TypeOfCard == "Gold")
            {
                return;
            }
        }
        CardsInStripe.Clear();
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
            if(card.GetComponent<CardModel>().TypeOfCard != "Gold")
            {
                if(card.GetComponent<CardModel>().UnderAttackJM1 == true)
                {
                    card.GetComponent<CardModel>().UnderAttackJM1 = false;
                    card.GetComponent<CardModel>().Power += 5;
                }
                
                if(card.GetComponent<CardModel>().UnderAttackJM2 == true)
                {
                    card.GetComponent<CardModel>().UnderAttackJM2 = false;
                    card.GetComponent<CardModel>().Power += 5;
                }
                
                if(card.GetComponent<CardModel>().UnderAttackJ1 == true)
                {
                    card.GetComponent<CardModel>().UnderAttackJ1 = false;
                    card.GetComponent<CardModel>().Power += 5;
                }
                
                if(card.GetComponent<CardModel>().UnderAttackJ2 == true)
                {
                    card.GetComponent<CardModel>().UnderAttackJ2 = false;
                    card.GetComponent<CardModel>().Power += 5;
                }
               
                if(card.GetComponent<CardModel>().UnderAttackK1 == true)
                {
                    card.GetComponent<CardModel>().UnderAttackK1 = false;
                    card.GetComponent<CardModel>().Power += 5;
                }
                
                if(card.GetComponent<CardModel>().UnderAttackK2 == true)
                {
                    card.GetComponent<CardModel>().UnderAttackK2 = false;
                    card.GetComponent<CardModel>().Power += 5;
                }
               
                if(card.GetComponent<CardModel>().UnderAttackT1 == true)
                {
                    card.GetComponent<CardModel>().UnderAttackT1 = false;
                    card.GetComponent<CardModel>().Power += 4;
                }
                
                if(card.GetComponent<CardModel>().UnderAttackT2 == true)
                {
                    card.GetComponent<CardModel>().UnderAttackT2 = false;
                    card.GetComponent<CardModel>().Power += 4;
                }
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

    public void Lure()
    {
        if(CardsInStripe.Count == 1 || CardsInStripe.Count > 1)
        {
            int highest = CardsInStripe[0].GetComponent<CardModel>().Power;

            for(int i = 0; i < CardsInStripe.Count; i++)
            {
                highest = Mathf.Max(highest, CardsInStripe[i].GetComponent<CardModel>().Power);
            }
            
            foreach(GameObject card in CardsInStripe)
            {
                if(card.GetComponent<CardModel>().Power == highest && card.GetComponent<CardModel>().Faction == "Cloud Of Fraternity" && card.GetComponent<CardModel>().TypeOfCard != "Gold" && card.GetComponent<CardModel>().TypeOfPower != "Lure")
                {
                    card.transform.SetParent(PlayerArea.transform, false);
                    card.transform.position = PlayerArea.transform.position;
                    card.GetComponent<MoveCard>().useful = true;
                    CardsInStripe.Remove(card);
                }
                if(card.GetComponent<CardModel>().Power == highest && card.GetComponent<CardModel>().Faction == "Reign Of Punishment" && card.GetComponent<CardModel>().TypeOfCard != "Gold" && card.GetComponent<CardModel>().TypeOfPower != "Lure")
                {
                    card.transform.SetParent(EnemyArea.transform, false);
                    card.transform.position = EnemyArea.transform.position;
                    card.GetComponent<MoveCard>().useful = true;
                    CardsInStripe.Remove(card);
                }
            }
        }
    }   
    
}
