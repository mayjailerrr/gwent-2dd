using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardModel : MonoBehaviour
{
    public string Name;
    public string TypeOfCard;
    public int Power;
    public int PurePower;
    public string TypeOfPower;
    public string Faction;
    public int Stripe;

    public bool Drew = false;
    public bool UnderAttack = false;
    public bool used = false;

    public void MoveToGraveyard()
    {
            GameObject playerGraveyard = GameObject.Find("PGraveyard");
            if(playerGraveyard != null)
            {
                transform.position = playerGraveyard.transform.position;
                gameObject.SetActive(false);
                //Destroy(gameObject);
            }
    }
}
