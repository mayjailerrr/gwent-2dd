using System.IO;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEditor;
using System.Runtime.Serialization.Formatters.Binary;
using GameLibrary;

public class VisualAllocation : MonoBehaviour
{
    Sprite image;
    string savingPath = "/home/analaura/Documentos/Compiler";

    public VisualAllocation(Sprite image)
    {
        this.image = image;
    }

    public void AllocateVisual(Card card, string imagePath, string infoPath, bool save)
    {
        Sprite mainImage = null;
        Sprite info = null;

        try 
        {
            if (save)
            {
                mainImage = GetSpriteAt(imagePath, card.Name, true);
                info = GetSpriteAt(infoPath, card.Name, false);
            }
            else 
            {
                mainImage = GetSpriteAt(savingPath + "/Main Image/", card.Name, true, false, ".jpeg");
                info = GetSpriteAt(savingPath + "/Info/", card.Name, false, false, ".jpeg");
            }
        }
        catch { }

        if (!(mainImage is null || info is null))
            card.AllocateInfo(new VisualInfo(mainImage, info, card.Faction));
    }


    Sprite GetSpriteAt(string path, string name, bool isMain, bool save = true, string ext = "")
    {
        string pathh = path + "\\" + name;

        if(ext.Length == 0)
        {
            ext = ".png";
            if (!File.Exists(path + ".jpg")) ext = ".jpg";
            else if (!File.Exists(path + ".jpeg")) ext = ".jpeg";
            else if (!File.Exists(path + ".bmp")) ext = ".bmp";
            else if (!File.Exists(path + ".tiff")) ext = ".tiff";
            else if (!File.Exists(path + ".gif")) ext = ".gif";
            else return null;
        }

        pathh += ext;

        FileStream file = File.OpenRead(pathh);
        byte[] data = new byte[file.Length];
        file.Read(data, 0, data.Length);
        file.Close();

        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(data);
        if (save) SaveImage(data, isMain, name);
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));
    }

    void SaveImage(byte[] bytes, bool isMain, string name)
    {
        string path = savingPath;
        if (isMain) path += "/Main Image/";
        else path += "/Info/";
        path += name + ".jpeg";

        File.WriteAllBytes(path, bytes);
    }
   

}
