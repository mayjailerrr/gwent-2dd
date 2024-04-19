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

    public bool UnderAttackJM1 = false;
    public bool UnderAttackJM2 = false;

    public bool UnderAttackJ1 = false;
    public bool UnderAttackJ2 = false;

    public bool UnderAttackK1 = false;
    public bool UnderAttackK2 = false;

    public bool UnderAttackT1 = false;
    public bool UnderAttackT2 = false;

    public bool used = false;

}
