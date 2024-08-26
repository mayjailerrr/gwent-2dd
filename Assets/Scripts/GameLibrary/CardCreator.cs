using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Dynamic;
using System.Linq;
using System.IO;

public static class CardCreator
{
    public static List<string[]> GetCardInfoList(string path)
    {
        TextAsset[] textFiles = Resources.LoadAll<TextAsset>(path);

        List<string[]> cardInfoList = new List<string[]>();
        
        foreach (TextAsset text in textFiles)
        {
           Debug.Log(text.text);
           cardInfoList.Add(GetCardInfoList(text));
        }
        return cardInfoList;
    }

    private static string[] GetCardInfoList(TextAsset text)
    {
        string[] cardInfo = text.text.Split('\n');
        return cardInfo;
    }
}
