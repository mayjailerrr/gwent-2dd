using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using UnityEngine.UI;
using System;
using TMPro;

public class CardInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject cardInfoPrefab;
    private GameObject cardInfoInstance;

    public void OnPointerEnter(PointerEventData eventData)
    {
        cardInfoInstance = Instantiate(cardInfoPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        UICard uICard = gameObject.GetComponent<UICard>();
        Card card = uICard.MotherCard;

        GameObject battlefield = GameObject.Find("BattlefieldBoard");
        if (battlefield != null)
        {
            cardInfoInstance.transform.SetParent(battlefield.transform);
            cardInfoInstance.transform.localPosition = new Vector3(799.745f, -8.0395f, 0f);
        }

        else throw new ArgumentException("Battlefield not found");

        Transform cardNameText = cardInfoInstance.transform.Find("CardName");
        if (cardNameText != null)
        {
            TextMeshProUGUI textInfo = cardNameText.GetComponent<TextMeshProUGUI>();
            if (textInfo != null)
            {
                textInfo.text = "Name:" + card.Name;
            }
        }

        Transform cardTypeText = cardInfoInstance.transform.Find("CardType");
        if (cardTypeText != null)
        {
            TextMeshProUGUI textInfo = cardTypeText.GetComponent<TextMeshProUGUI>();
            if (textInfo != null)
            {
                textInfo.text = "Type:" + card.Type;
            }
        }

        Transform cardZoneText = cardInfoInstance.transform.Find("CardZone");
        if (cardZoneText != null)
        {
            TextMeshProUGUI textInfo = cardZoneText.GetComponent<TextMeshProUGUI>();
            if (textInfo != null)
            {
                if (card is UnityCard unityCard)
                    textInfo.text = "Zone:" + unityCard.ZoneString;
                else textInfo.text = "";
            }
        }

        Transform cardPowerText = cardInfoInstance.transform.Find("CardPower");
        if (cardPowerText != null)
        {
            TextMeshProUGUI textInfo = cardPowerText.GetComponent<TextMeshProUGUI>();
            if (textInfo != null)
            {
                if (card is UnityCard unityCard)
                    textInfo.text = "Power:" + unityCard.Power.ToString();
                else textInfo.text = "";
            }
        }

        Transform cardEffectText = cardInfoInstance.transform.Find("CardEffect");
        if (cardEffectText != null)
        {
            TextMeshProUGUI textInfo = cardEffectText.GetComponent<TextMeshProUGUI>();
            if (textInfo != null)
                textInfo.text = "Effect:" + card.EffectDescription;
        }

        Transform cardDescriptionText = cardInfoInstance.transform.Find("CardDescription");
        if (cardDescriptionText != null)
        {
            TextMeshProUGUI textInfo = cardDescriptionText.GetComponent<TextMeshProUGUI>();
            if (textInfo != null)
                textInfo.text = "Description:" + card.CharacterDescription;
        }

        Transform cardQuoteText = cardInfoInstance.transform.Find("CardQuote");
        if (cardQuoteText != null)
        {
            TextMeshProUGUI textInfo = cardQuoteText.GetComponent<TextMeshProUGUI>();
            if (textInfo != null)
                textInfo.text = card.Quote;
        }
    }

    public void OnPointerExit(UnityEngine.EventSystems.PointerEventData eventData)
    {
        if (cardInfoInstance != null)
            Destroy(cardInfoInstance);
    }
}
