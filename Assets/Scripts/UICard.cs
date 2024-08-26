using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UI;
using System.Collections;
using UnityEngine;
using TMPro;

public class UICard : MonoBehaviour
{
    public Card MotherCard;
    public Image CardImage;
    public TextMeshProUGUI Power;

    public void PrintCard(Card card)
    {
        MotherCard = card;
        CardImage.sprite = Resources.Load<Sprite>(MotherCard.Name);

        if (MotherCard is UnityCard unityCard)
            Power.text = unityCard.Power.ToString();
        else Power.text = " ";
    }
}
