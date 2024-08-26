using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEditor;
using TMPro;

public class UIVisual : MonoBehaviour, IObserver
{
    public GameObject CardPrefab;

    //player1
    public TextMeshProUGUI player1Name;
    public TextMeshProUGUI player1Faction;

    public UnityEngine.UI.Image player1FirstVictory;
    public UnityEngine.UI.Image player1SecondVictory;
    public UnityEngine.UI.Image player1Graveyard;

    public TextMeshProUGUI player1DeckSize;

    public TextMeshProUGUI player1MeleeScore;
    public TextMeshProUGUI player1DistanceScore;
    public TextMeshProUGUI player1SiegeScore;
    public TextMeshProUGUI player1TotalScore;

    public HorizontalLayoutGroup player1Leader;
    [SerializeField] UIHand player1HandScript;

    //player2
    public TextMeshProUGUI player2Name;
    public TextMeshProUGUI player2Faction;

    public UnityEngine.UI.Image player2FirstVictory;
    public UnityEngine.UI.Image player2SecondVictory;
    public UnityEngine.UI.Image player2Graveyard;

    public TextMeshProUGUI player2DeckSize;

    public TextMeshProUGUI player2MeleeScore;
    public TextMeshProUGUI player2DistanceScore;
    public TextMeshProUGUI player2SiegeScore;
    public TextMeshProUGUI player2TotalScore;

    public HorizontalLayoutGroup player2Leader;
    [SerializeField] UIHand player2HandScript;

    
    public Image WeatherEffectMelee1;
    public Image WeatherEffectDistance1;
    public Image WeatherEffectSiege1;

    public Image WeatherEffectMelee2;
    public Image WeatherEffectDistance2;
    public Image WeatherEffectSiege2;

    public Image Info;
    public TextMeshProUGUI MessageInfo;


    //Battlefield objects
    public HorizontalLayoutGroup MeleeZone1;
    public HorizontalLayoutGroup DistanceZone1;
    public HorizontalLayoutGroup SiegeZone1;

    public HorizontalLayoutGroup MeleeZone2;
    public HorizontalLayoutGroup DistanceZone2;
    public HorizontalLayoutGroup SiegeZone2;

    public HorizontalLayoutGroup IncreaseMelee1;
    public HorizontalLayoutGroup IncreaseDistance1;
    public HorizontalLayoutGroup IncreaseSiege1;

    public HorizontalLayoutGroup IncreaseMelee2;
    public HorizontalLayoutGroup IncreaseDistance2;
    public HorizontalLayoutGroup IncreaseSiege2;

    public HorizontalLayoutGroup MeleeWeather;
    public HorizontalLayoutGroup DistanceWeather;
    public HorizontalLayoutGroup SiegeWeather;

    //elements for the UI
    public Image SmogForDecoyEvent;
    public HorizontalLayoutGroup DecoyEventCardShower;
    public GameObject CardToPickPrefab;

    private void OnEnable()
    {
        //GameManager.gameManager.AddObserver(this);
    }

    private void OnDisable()
    {
        GameManager.gameManager.RemoveObserver(this);
    }

    void Start()
    {
        GameManager.gameManager.AddObserver(this);

        Info.gameObject.SetActive(false);
        SmogForDecoyEvent.gameObject.SetActive(false);

        HideImage(player1FirstVictory);
        HideImage(player1SecondVictory);
        HideImage(player1Graveyard);

        HideImage(player2FirstVictory);
        HideImage(player2SecondVictory);
        HideImage(player2Graveyard);

        WeatherEffectMelee1.gameObject.SetActive(false);
        WeatherEffectDistance1.gameObject.SetActive(false);
        WeatherEffectSiege1.gameObject.SetActive(false);

        WeatherEffectMelee2.gameObject.SetActive(false);
        WeatherEffectDistance2.gameObject.SetActive(false);
        WeatherEffectSiege2.gameObject.SetActive(false);

        player1MeleeScore.text = " ";
        player1DistanceScore.text = " ";
        player1SiegeScore.text = " ";

        player2MeleeScore.text = " ";
        player2DistanceScore.text = " ";
        player2SiegeScore.text = " ";

        player1TotalScore.text = "0";
        player2TotalScore.text = "0";

        player1Name.text = GameManager.player.PlayerName;
        player1Faction.text = GameManager.player.PlayerFaction;

        player2Name.text = GameManager.opponent.PlayerName;
        player2Faction.text = GameManager.opponent.PlayerFaction;

        var newCard1 = Instantiate(CardPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        newCard1.transform.SetParent(player1Leader.transform, false);
        UICard uI = newCard1.GetComponent<UICard>();
        uI.PrintCard(GameManager.player.PlayerLeader);

        var newCard2 = Instantiate(CardPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        newCard2.transform.SetParent(player2Leader.transform, false);
        UICard uI2 = newCard2.GetComponent<UICard>();
        uI2.PrintCard(GameManager.opponent.PlayerLeader);
    }

    void Update()
    {
        player1DeckSize.text = " " + GameManager.player.PlayerHand.GameDeck.Count.ToString();
        player2DeckSize.text = " " + GameManager.opponent.PlayerHand.GameDeck.Count.ToString();
    }

    public void OnNotify(System.Enum action, Card card)
    {
        Debug.Log($"UIVisual OnNotify for {action.ToString()}");

        switch (action)
        {
            case GameEvents.Start:
                StartCoroutine(StartGame());
                return;
            case GameEvents.Summon:
                StartCoroutine(Summon(card));
                return;
            case GameEvents.PassTurn:
                StartCoroutine(PassTurn());
                return;
            case GameEvents.StartRound:
                StartCoroutine(StartRound());
                return;
            case GameEvents.FinishRound:
                StartCoroutine(FinishRound());
                return;
            case GameEvents.FinishGame:
                StartCoroutine(FinishGame());
                return;
            case GameEvents.DrawCard:
                DrawCard();
                return;
            case GameEvents.Invoke:
                InvokeUI(card);
                return;
            case GameEvents.DecoyEventStart:
                DecoyEventInit();
                return;
            case GameEvents.DecoyEventEnd:
                DecoyEventEnd(card);
                return;
            case GameEvents.DecoyEventAbort:
                AbortingDecoyEvent();
                return;
        }
    }

    private void DrawCard()
    {
        if (GameManager.player.PlayerHand.PlayerHand.Count <= 10)
            player1HandScript.DrawCardUI(GameManager.player.PlayerHand.PlayerHand[GameManager.player.PlayerHand.PlayerHand.Count - 1]);
        if (GameManager.player.PlayerHand.PlayerHand.Count <= 10)
            player1HandScript.DrawCardUI(GameManager.player.PlayerHand.PlayerHand[GameManager.player.PlayerHand.PlayerHand.Count - 2]);
        
        if (GameManager.opponent.PlayerHand.PlayerHand.Count <= 10)
            player2HandScript.DrawCardUI(GameManager.opponent.PlayerHand.PlayerHand[GameManager.opponent.PlayerHand.PlayerHand.Count - 1]);
        if (GameManager.opponent.PlayerHand.PlayerHand.Count <= 10)
            player2HandScript.DrawCardUI(GameManager.opponent.PlayerHand.PlayerHand[GameManager.opponent.PlayerHand.PlayerHand.Count - 2]);
        
    }

    public void AbortingDecoyEvent()
    {
        UpdateScores();
        UpdateBattlefieldUI();

        if (GameManager.player.IsActive == true && GameManager.opponent.HasPassed == false)
            StartCoroutine(ShowInfo($"Player {GameManager.opponent.PlayerName} 's turn now."));   
        else if (GameManager.opponent.IsActive == true && GameManager.player.HasPassed == false)
            StartCoroutine(ShowInfo($"Player {GameManager.player.PlayerName} 's turn now."));
    }

    public void DecoyEventEnd(Card card)
    {
        UpdateScores();
        UpdateBattlefieldUI();

        if (GameManager.player.IsActive == true && GameManager.opponent.HasPassed == false)
            InternalDecoyEventEnd(card, player1HandScript, GameManager.opponent);
        if (GameManager.opponent.IsActive == true && GameManager.player.HasPassed == false)
            InternalDecoyEventEnd(card, player2HandScript, GameManager.player);   
    }

    public void InternalDecoyEventEnd(Card card, UIHand hand, Player nextPlayer)
    {
        var newCard = Instantiate(CardPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        newCard.transform.SetParent(hand.Hand.GetComponent<HorizontalLayoutGroup>().transform, false);
        newCard.GetComponent<UICard>().PrintCard(card);
        for (int i = 0; i < DecoyEventCardShower.transform.childCount; i++)
        {
            GameObject cardToDestroy = DecoyEventCardShower.transform.GetChild(i).gameObject;
            Destroy(cardToDestroy);
        }
        SmogForDecoyEvent.gameObject.SetActive(false);
        StartCoroutine(ShowInfo($"Player {nextPlayer.PlayerName} 's turn now."));
    }

    public void DecoyEventInit()
    {
        if (GameManager.player.IsActive == true)
            InternalDecoyEventInit(GameManager.player, new());
        else InternalDecoyEventInit(GameManager.opponent, new());
    }

    private void InternalDecoyEventInit(Player activePlayer, List<Silver> cardsInDecoy)
    {
        List<UnityCard> Zone = null;

        for (int i = 0; i < 3; i++)
        {
            List<UnityCard> cardList = activePlayer.Battlefield.GetZoneFromBattlefield(Utils.ZoneForIndex[i]);

            foreach (UnityCard unityCard in cardList)
            {
                if (unityCard.Type == CardTypes.Lure) 
                {
                    Zone = cardList;
                }
            }
        }

        if (Zone != null)
        {
            if (Zone.Count == 1)
            {
                GameManager.gameManager.AbortDecoyEvent();
            }

            else 
            {
                foreach (UnityCard unityCard in Zone)
                {
                    if (unityCard is Silver silverCard)
                        cardsInDecoy.Add(silverCard);
                }

                if (cardsInDecoy.Count > 0)
                {
                    foreach (Silver silverCard in cardsInDecoy)
                    {
                        Debug.Log(cardsInDecoy.Count);
                        var newCard = Instantiate(CardToPickPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                        newCard.transform.SetParent(DecoyEventCardShower.transform, false);
                        UICard uI = newCard.GetComponent<UICard>();
                        uI.PrintCard(silverCard);
                    }
                }
                else 
                {
                    GameManager.gameManager.AbortDecoyEvent();
                    return;
                }

                SmogForDecoyEvent.gameObject.SetActive(true);
            }
        }
        else 
        {
            GameManager.gameManager.AbortDecoyEvent();
        }

    }

    private void InvokeUI(Card card)
    {
        UpdateScores();
        UpdateBattlefieldUI();

        if (GameManager.player.IsActive == true && GameManager.opponent.HasPassed == false)
            StartCoroutine(ShowInfo($"Player {GameManager.opponent.PlayerName} 's turn now."));
        if (GameManager.opponent.IsActive == true && GameManager.player.HasPassed == false)
            StartCoroutine(ShowInfo($"Player {GameManager.player.PlayerName} 's turn now."));
        
        if (card is WeatherCard weatherCard)
        {
            switch (weatherCard.Zone)
            {
                case ZoneTypes.Melee:
                    InternalInvokeUi(card, MeleeWeather);
                    ShowWeatherInZone(WeatherEffectMelee1);
                    ShowWeatherInZone(WeatherEffectMelee2);
                    return;
                case ZoneTypes.Distance:
                    InternalInvokeUi(card, DistanceWeather);
                    ShowWeatherInZone(WeatherEffectDistance1);
                    ShowWeatherInZone(WeatherEffectDistance2);
                    return;
                case ZoneTypes.Siege:
                    InternalInvokeUi(card, SiegeWeather);
                    ShowWeatherInZone(WeatherEffectSiege1);
                    ShowWeatherInZone(WeatherEffectSiege2);
                    return;
            }
        }

        else if (GameManager.player.IsActive == true)
        {
            if (card is Augment increaseCard)
            {
                switch (increaseCard.Zone)
                {
                    case ZoneTypes.Melee:
                        InternalInvokeUi(card, IncreaseMelee1);
                        return;
                    case ZoneTypes.Distance:
                        InternalInvokeUi(card, IncreaseDistance1);
                        return;
                    case ZoneTypes.Siege:
                        InternalInvokeUi(card, IncreaseSiege1);
                        return;
                }
            }
        }

        else if (GameManager.opponent.IsActive == true)
        {
            if (card is Augment increaseCard)
            {
                switch (increaseCard.Zone)
                {
                    case ZoneTypes.Melee:
                        InternalInvokeUi(card, IncreaseMelee2);
                        return;
                    case ZoneTypes.Distance:
                        InternalInvokeUi(card, IncreaseDistance2);
                        return;
                    case ZoneTypes.Siege:
                        InternalInvokeUi(card, IncreaseSiege2);
                        return;
                }
            }
        }
    }

    private void InternalInvokeUi(Card card, HorizontalLayoutGroup place)
    {
        var newCard = Instantiate(CardPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        newCard.transform.SetParent(place.transform, false);
        newCard.transform.position = place.transform.position;
        newCard.GetComponent<UICard>().PrintCard(card);
    }

    private void ShowWeatherInZone(Image weatherImage)
    {
        weatherImage.gameObject.SetActive(true);
        weatherImage.raycastTarget = false;
    }

    private void UpdateScores()
    {
        player1MeleeScore.text = GameManager.player.Battlefield.MeleeZoneScore.ToString();
        player1DistanceScore.text = GameManager.player.Battlefield.DistanceZoneScore.ToString();
        player1SiegeScore.text = GameManager.player.Battlefield.SiegeZoneScore.ToString();
        player1TotalScore.text = GameManager.player.Battlefield.TotalScore.ToString();

        player2MeleeScore.text = GameManager.opponent.Battlefield.MeleeZoneScore.ToString();
        player2DistanceScore.text = GameManager.opponent.Battlefield.DistanceZoneScore.ToString();
        player2SiegeScore.text = GameManager.opponent.Battlefield.SiegeZoneScore.ToString();
        player2TotalScore.text = GameManager.opponent.Battlefield.TotalScore.ToString();

        Debug.Log("Scores updated");
    }

    IEnumerator ShowInfo(string text)
    {
        Debug.Log("The message is: " + text);
        MessageInfo.text = text;
        Info.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);

        Info.gameObject.SetActive(false);
        Debug.Log("The message:" + text + "will be occult now");
    }

    private void ShowFinalMessage(string text)
    {
        Debug.Log("The message that will appear is: " + text);
        MessageInfo.text = text;
        Info.gameObject.SetActive(true);
    }

    private void ShowImage(UnityEngine.UI.Image image)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
    }

    private void HideImage(UnityEngine.UI.Image image)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
    }

    private async void ClearBattlefieldUI()
    {
        await Task.Delay(3000);

        ClearZoneUI(MeleeZone1);
        ClearZoneUI(DistanceZone1);
        ClearZoneUI(SiegeZone1);

        ClearZoneUI(MeleeZone2);
        ClearZoneUI(DistanceZone2);
        ClearZoneUI(SiegeZone2);

        ClearSlot(IncreaseMelee1);
        ClearSlot(IncreaseDistance1);
        ClearSlot(IncreaseSiege1);

        ClearSlot(IncreaseMelee2);
        ClearSlot(IncreaseDistance2);
        ClearSlot(IncreaseSiege2);

        WeatherEffectMelee1.gameObject.SetActive(false);
        WeatherEffectDistance1.gameObject.SetActive(false);
        WeatherEffectSiege1.gameObject.SetActive(false);

        WeatherEffectMelee2.gameObject.SetActive(false);
        WeatherEffectDistance2.gameObject.SetActive(false);
        WeatherEffectSiege2.gameObject.SetActive(false);   
    }

    private void ClearSlot(HorizontalLayoutGroup toClear)
    {
        for (int i = 0; i < toClear.transform.childCount; i++)
        {
            GameObject card = toClear.transform.GetChild(i).gameObject;

            if (toClear.transform.parent.parent.name == "Payer1")
                SendToGraveyard(card, player1Graveyard);
            if (toClear.transform.parent.parent.name == "Payer2")
                SendToGraveyard(card, player2Graveyard);
            if (toClear.transform.parent.parent.name == "BattlefieldBoard")
                Destroy(card);
        }
    }

    private void ClearZoneUI(HorizontalLayoutGroup toClear)
    {
        for (int i = 0; i < toClear.transform.childCount; i++)
        {
            GameObject card = toClear.transform.GetChild(i).gameObject;

            if (toClear.transform.parent.parent.name == "Payer1")
                SendToGraveyard(card, player1Graveyard);
           
            if (toClear.transform.parent.parent.name == "Payer2")
                SendToGraveyard(card, player2Graveyard);
        }
    }

    private void SendToGraveyard(GameObject card, Image graveyardImage)
    {
        LeanTween.move(card, graveyardImage.transform.position, 1f)
                .setOnComplete(() => Destroy(card));
        ShowImage(graveyardImage);
    }

    private void ShowVictories()
    {
        if (GameManager.player.GamesWon < 2 && GameManager.player.GamesWon > 0)
            ShowImage(player1FirstVictory);
        if (GameManager.player.GamesWon == 2)
            ShowImage(player1SecondVictory);
        
        if (GameManager.opponent.GamesWon < 2 && GameManager.opponent.GamesWon > 0)
            ShowImage(player2FirstVictory); 
        if (GameManager.opponent.GamesWon == 2)
            ShowImage(player2SecondVictory);
            
    }

    private IEnumerator StartGame()
    {
        UpdateScores();

        yield return new WaitForSeconds(2f);

        if (GameManager.player.IsActive == true)
            StartCoroutine(ShowInfo($"Player {GameManager.player.PlayerName} starts now."));
        if (GameManager.opponent.IsActive == true)
            StartCoroutine(ShowInfo($"Player {GameManager.opponent.PlayerName} starts now."));
    }

    private IEnumerator Summon(Card card)
    {
        UpdateScores();
        UpdateBattlefieldUI();

        if (card != null)
        {
            if (GameManager.player.IsActive == true && GameManager.opponent.HasPassed == false)
                StartCoroutine(ShowInfo($"Player {GameManager.opponent.PlayerName} 's turn now."));
            else if (GameManager.opponent.IsActive == true && GameManager.player.HasPassed == false)
                StartCoroutine(ShowInfo($"Player {GameManager.player.PlayerName} 's turn now."));

            
            if (card.Name == "John")
            {
                ShowWeatherInZone(WeatherEffectMelee1);
                ShowWeatherInZone(WeatherEffectMelee2);
            }
            if (card.Name == "Khal")
            {
                ShowWeatherInZone(WeatherEffectDistance1);
                ShowWeatherInZone(WeatherEffectDistance2);
            }
            if (card.Name == "Thormund")
            {
                ShowWeatherInZone(WeatherEffectSiege1);
                ShowWeatherInZone(WeatherEffectSiege2);
            }
            if (card.Type == CardTypes.Clearance || card.EffectNumber == 12)
            {
                WeatherEffectMelee1.gameObject.SetActive(false);
                WeatherEffectDistance1.gameObject.SetActive(false);
                WeatherEffectSiege1.gameObject.SetActive(false);

                WeatherEffectMelee2.gameObject.SetActive(false);
                WeatherEffectDistance2.gameObject.SetActive(false);
                WeatherEffectSiege2.gameObject.SetActive(false);

                ClearSlot(MeleeWeather);
                ClearSlot(DistanceWeather);
                ClearSlot(SiegeWeather);
            }

            if (card.EffectNumber == 7)
            {
                if (GameManager.player.IsActive == true)
                    player1HandScript.DrawCardUI(GameManager.player.PlayerHand.PlayerHand[GameManager.player.PlayerHand.PlayerHand.Count - 1]);
                else if (GameManager.opponent.IsActive == true)
                    player2HandScript.DrawCardUI(GameManager.opponent.PlayerHand.PlayerHand[GameManager.opponent.PlayerHand.PlayerHand.Count - 1]);
            }

            if (card.EffectNumber == 16)
            {
                player1HandScript.LeaderAbilityActive = false;
                player2HandScript.LeaderAbilityActive = false;
            }

            if (card.EffectNumber == 15)
            {
                if (GameManager.player.IsActive == true)
                    player1HandScript.LeaderAbilityActive = false;
                else if (GameManager.opponent.IsActive == true)
                    player2HandScript.LeaderAbilityActive = false;
            }
        }

        else yield return null;
    }

    IEnumerator PassTurn()
    {
        if (GameManager.player.HasPassed == true && GameManager.opponent.HasPassed == false)
        {
            StartCoroutine(ShowInfo($"Player {GameManager.player.PlayerName} has passed."));
            yield return new WaitForSeconds(2f);
            StartCoroutine(ShowInfo($"Player {GameManager.opponent.PlayerName} 's turn now."));
        }
        else if (GameManager.opponent.HasPassed == true && GameManager.player.HasPassed == false)
        {
            StartCoroutine(ShowInfo($"Player {GameManager.opponent.PlayerName} has passed."));
            yield return new WaitForSeconds(2f);
            StartCoroutine(ShowInfo($"Player {GameManager.player.PlayerName} 's turn now."));
        }
    }

    IEnumerator StartRound()
    {
        ShowVictories();

        if (GameManager.player.IsActive == true && GameManager.opponent.HasPassed == false)
        {
            yield return new WaitForSeconds(4f);
            StartCoroutine(ShowInfo($"Player {GameManager.player.PlayerName} 's turn now."));
        }
        else if (GameManager.opponent.IsActive == true && GameManager.player.HasPassed == false)
        {
            yield return new WaitForSeconds(4f);
            StartCoroutine(ShowInfo($"Player {GameManager.opponent.PlayerName} 's turn now."));
        }

        UpdateScores();
        UpdateBattlefieldUI();
    }

    IEnumerator FinishRound()
    {
        StartCoroutine(ShowInfo("Round finished!"));

        ClearBattlefieldUI();

        if (GameManager.player.Battlefield.TotalScore > GameManager.opponent.Battlefield.TotalScore)
        {
           yield return new WaitForSeconds(2f);
            StartCoroutine(ShowInfo($"Player {GameManager.player.PlayerName} wins the round!"));
        }
        else if (GameManager.opponent.Battlefield.TotalScore > GameManager.player.Battlefield.TotalScore)
        {
            yield return new WaitForSeconds(2f);
            StartCoroutine(ShowInfo($"Player {GameManager.opponent.PlayerName} wins the round!"));
        }
        else 
        {
            yield return new WaitForSeconds(2f);
            StartCoroutine(ShowInfo("It's a draw!"));
        }
    }

    IEnumerator FinishGame()
    {
        if (GameManager.player.GamesWon > GameManager.opponent.GamesWon)
        {
            yield return new WaitForSeconds(5f);
            ShowFinalMessage($"Player {GameManager.player.PlayerName} wins the game!");
        }

        else if (GameManager.opponent.GamesWon > GameManager.player.GamesWon)
        {
            yield return new WaitForSeconds(5f);
            ShowFinalMessage($"Player {GameManager.opponent.PlayerName} wins the game!");
        }

        else 
        {
            yield return new WaitForSeconds(5f);
            ShowFinalMessage("It's a draw!");
        }

        yield return new WaitForSeconds(10f); //26
        SceneManager.LoadScene("MainMenuToBack");
    }

    private void UpdateBattlefieldUI()
    {
        UpdateZoneUI(MeleeZone1, GameManager.player, 0);
        UpdateZoneUI(DistanceZone1, GameManager.player, 1);
        UpdateZoneUI(SiegeZone1, GameManager.player, 2);

        UpdateZoneUI(MeleeZone2, GameManager.opponent, 0);
        UpdateZoneUI(DistanceZone2, GameManager.opponent, 1);
        UpdateZoneUI(SiegeZone2, GameManager.opponent, 2);

        Debug.Log("Battlefield updated");
    }

    private void UpdateZoneUI(HorizontalLayoutGroup zoneToClear, Player playerToUpdate, int zoneCorrespondence)
    {
        for (int i = 0; i < zoneToClear.transform.childCount; i++)
        {
            Card card = zoneToClear.transform.GetChild(i).GetComponent<UICard>().MotherCard;

            if (card is Silver silverCard)
            {
                if (!playerToUpdate.Battlefield.GetZoneFromBattlefield(Utils.ZoneForIndex[zoneCorrespondence]).Contains(silverCard))
                {
                    GameObject cardToDestroy = zoneToClear.transform.GetChild(i).gameObject;
                    SendToGraveyard(cardToDestroy, player2Graveyard);
                    Debug.Log(card.Name + " has been sent to the graveyard.");
                }

                else zoneToClear.transform.GetChild(i).GetComponent<UICard>().Power.text = silverCard.initialPower.ToString();
            }
           
        }
    }
}


