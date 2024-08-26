using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BattlefieldSceneLoad : MonoBehaviour
{
    public void LoadBattlefieldScene()
    {
        if (GameData.IsReady == true)
            SceneManager.LoadScene("SwapTwoCards");
        else SceneManager.LoadScene("SetPlayerName");
    }
}
