using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SetValidName : MonoBehaviour
{
    public InputField Input;
    public Text InputOrder;
    public Text PlayerName;
    public Button StartButton;

    void Start()
    {
        StartButton.interactable = true;

        if (GameData.PlayerName == null)
            InputOrder.text = "Player 1, please enter your name.";
        else InputOrder.text = "Player 2, please enter your name.";
    }

    void Update()
    {
        if (PlayerName.text.Length > 4)
            StartButton.interactable = true;
        else
            StartButton.interactable = false;
    }

    public void SetName()
    {
        if (GameData.Player1 == null)
            GameData.PlayerName = PlayerName.text;
        
        else GameData.OpponentName = PlayerName.text;

        SceneManager.LoadScene("CardCreator");
    }
}
