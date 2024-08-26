using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class GameData
{
    public static string PlayerName;
    public static DeckCreator PlayerInitialDeck;
    public static Player Player1;

    public static string OpponentName;
    public static DeckCreator OpponentInitialDeck;
    public static Player Opponent;

    public static bool IsReady = false;

    public static void SetPlayer()
    {
        if (PlayerName == null || PlayerInitialDeck == null)
        {
            PlayerInitialDeck = CreatingDeck.actualDeck;
            Player1 = new Player(PlayerName, PlayerInitialDeck.Faction, PlayerInitialDeck);
        }
        else
        {
            OpponentInitialDeck = CreatingDeck.actualDeck;
            Opponent = new Player(OpponentName, OpponentInitialDeck.Faction, OpponentInitialDeck);
            IsReady = true;
        }
    }

    public static void ResetData()
    {
        PlayerName = null;
        PlayerInitialDeck = null;
        Player1 = null;

        OpponentName = null;
        OpponentInitialDeck = null;
        Opponent = null;

        IsReady = false;
    }
}
