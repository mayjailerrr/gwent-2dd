using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Interpreterr;
using System.IO;
using GameLibrary;

public class CardCreationManager : MonoBehaviour
{
    [SerializeField] Sprite cardImage; 
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject menu;
    [SerializeField] GameObject consoleMenu;
    [SerializeField] TMP_Text terminal;
    [SerializeField] TMP_Text StoredEffectsSetOut;
    [SerializeField] TMP_Text StoredCardsSetOut;
    [SerializeField] TMP_Text StoreTerminal;
    [SerializeField] TMP_InputField console;
    [SerializeField] TMP_InputField StoredEffectInput;
    [SerializeField] TMP_InputField StoredCardInput;
    [SerializeField] TMP_InputField scriptPathInput;
    [SerializeField] TMP_InputField imagePathInput;
    [SerializeField] TMP_InputField infoPathInput;

    Interpreterr.Interpreterrr interpreterrr;
   
    VisualAllocation visualAllocation;
    List<Card> cards;
    List<string> addedCards;
    List<string> storedCards;
    List<string> storedEffects;
    string mainPath = "/home/analaura/Documentos/Compiler";
    string scriptPath = "/home/analaura/My project/Assets/Scripts/New/Scripts";
    string imagePath = "/home/analaura/My project/Assets/Scripts/New/Main Image";
    string infoPath = "/home/analaura/My project/Assets/Scripts/New/Info";
    bool ValidInputInterpreted = true;


    public void Awake()
    {
        Directory.CreateDirectory(mainPath + "/Cards/Scripts");
        Directory.CreateDirectory(mainPath + "/Cards/Main Image");
        Directory.CreateDirectory(mainPath + "/Cards/Info"); 
        Directory.CreateDirectory(mainPath + "/Effects/Scripts"); 
        ResetCards();
        addedCards = new List<string>();
        storedEffects = new List<string>();
        UpdateStoredScripts(StoredEffectsSetOut, "Effects");
        UpdateStoredScripts(StoredCardsSetOut, "Cards");
        if (AllCards.cloudsCards.Count > 22) 
            AllCards.cloudsCards.RemoveRange(22, AllCards.cloudsCards.Count - 22);

        if (AllCards.reignCards.Count > 22) 
            AllCards.reignCards.RemoveRange(22, AllCards.reignCards.Count - 22);
    }

    public void InterpretScriptsInput()
    {
        string[] paths = Directory.GetFiles(scriptPath);
        interpreterrr = new Interpreterrr(new ASTPrinter(terminal), storedCards, storedEffects);
        ValidInputInterpreted = true;
        foreach (var path in paths)
        {
            if (path.Substring(path.Length - 5) == ".meta") continue;
            else if (!interpreterrr.Interpret(File.ReadAllText(path)))
            {
                consoleMenu.SetActive(true);
                menu.SetActive(false);
            }

            AddCards(interpreterrr.SettledCards, imagePath, infoPath, true);

        }
    }

    public void InterpretPathInput()
    {
        interpreterrr = new Interpreterrr(new ASTPrinter(terminal), storedCards, storedEffects);
        
        if (interpreterrr.valid)
        {
            ValidInputInterpreted = true;
            AddCards(interpreterrr.SettledCards, mainPath + "/Cards/Main Image", mainPath + "/Cards/Info", false);
        }
        else 
        {
            consoleMenu.SetActive(true);
            menu.SetActive(false);
        }
    }

    public void CheckSemanticConsoleInput()
    {
        if (ValidInputInterpreted) 
        {
            interpreterrr.CheckSemantic(console.text);
            if (interpreterrr is null) Debug.Log("Interpreter is null");
        }
        
    }

    public void InterpretConsoleInput()
    {
        if (console.text == "") return;

        if (ValidInputInterpreted)
        {
            if (interpreterrr.Interpret(console.text))
                console.text = "";
            AddCards(interpreterrr.SettledCards, imagePath, infoPath, true);

          
        }
    }

    public void AddCardsToGame()
    {
        if (!ValidInputInterpreted) InterpretPathInput();

        foreach (var card in cards)
        {
            if (card is LeaderCard leader && !Player.Leaders.ContainsKey(leader.Name))
            {
                Player.Leaders.Add(leader.Name, leader);
            }
            else if (card.Factions is Faction.Clouds && AllCards.cloudsCards.Contains(card))
            {
                AllCards.cloudsCards.Add(card);
            }
            else if (!AllCards.cloudsCards.Contains(card))
            {
                AllCards.reignCards.Add(card);
            }
        }
        UpdateStoredScripts(StoredEffectsSetOut, "Effects");
        UpdateStoredScripts(StoredCardsSetOut, "Cards");
        ResetCards();
    }

    public void BackFromCardCreation()
    {
          AddCardsToGame();
          if (interpreterrr != null || interpreterrr.valid)
          {
                mainMenu.SetActive(true);
                menu.SetActive(false);
          };
    }

    public void AddStoredEffect()
    {
        StoreTerminal.text = "";
        ReCharge("effect", StoredEffectInput, StoredEffectsSetOut.text, storedEffects);

    }

    public void AddStoredCard()
    {
        StoreTerminal.text = "";
        ReCharge("card", StoredCardInput, StoredCardsSetOut.text, storedCards);
    }

    public void ChangeScriptsPath()
    {
        if (scriptPathInput.text.Length > 0)
        {
            scriptPath = scriptPathInput.text;
        }
        else scriptPath = scriptPathInput.placeholder.GetComponent<TMP_Text>().text;
    }

    public void ChangeImagesPath()
    {
        if (imagePathInput.text.Length > 0)
        {
            imagePath = imagePathInput.text;
        }
        else imagePath = imagePathInput.placeholder.GetComponent<TMP_Text>().text;
    }

    public void ChangeInfoPath()
    {
        if (infoPathInput.text.Length > 0)
        {
            infoPath = infoPathInput.text;
        }
        else infoPath = infoPathInput.placeholder.GetComponent<TMP_Text>().text;
    }
    public void DefaultDeck()
    {
        ResetCards();
        Awake();
    }

    public void DeleteFiles()
    {
        List<string> paths = new List<string>();
        paths.AddRange(Directory.GetFiles(mainPath + "/Effects/Scripts"));
        paths.AddRange(Directory.GetFiles(mainPath + "/Cards/Scripts")); 
        paths.AddRange(Directory.GetFiles(mainPath + "/Cards/Main Image")); 
        paths.AddRange(Directory.GetFiles(mainPath + "/Cards/Info")); 

        foreach (var item in paths)
        {
            File.Delete(item);
        }

        UpdateStoredScripts(StoredEffectsSetOut, "Effects");
        UpdateStoredScripts(StoredCardsSetOut, "Cards");
    }

    void ResetCards()
    {
        cards = new List<Card>();
        storedCards = new List<string>();
        visualAllocation = new VisualAllocation(cardImage);
    }

    void UpdateStoredScripts(TMP_Text display, string folder)
    {
        string[] paths = Directory.GetFiles(mainPath + "/" + folder + "/Scripts");
        display.text = "";
       
        if (paths.Length != 0)
        {
            foreach (var path in paths)
            {
                if (path.Substring(path.Length - 5) == ".meta") continue;
                string[] stepsInPath = path.Substring(0, path.Length - 4).Split('/');
                string name = stepsInPath[stepsInPath.Length - 1];
                if (!(folder == "Cards" && addedCards.Contains(name)))
                {
                    display.text += name + ", ";
                }
            }
        }
        if (display.text.Length == 0) display.text = "\n\n           Empty";
        else display.text = display.text.Substring(0, display.text.Length - 2);
    }

    void ReCharge(string type, TMP_InputField input, string container, List<string> names)
    {
        StoreTerminal.text = "";
        string[] inputNames = input.text.Split(',', System.StringSplitOptions.RemoveEmptyEntries);

        foreach (string item in inputNames)
        {
            string name = item.Trim();

            if (!container.Contains(name))
            {
                StoreTerminal.text = "Does not exist" + type + " " + name;
            }

            else if (names.Contains(name))
            {
                names.Remove(name);
                StoreTerminal.text = "Its unloaded" + type + " " + name;
                input.text = "";
            }
            else
            {
                names.Add(name);
                ValidInputInterpreted = false;
                StoreTerminal.text = "Its loaded" + type + " " + name;
                input.text = "";
            }
        }
    }

    void AddCards(List<Card> list, string imagePath, string infoPath, bool save)
    {
        foreach (var card in list)
        {
            this.visualAllocation.AllocateVisual(card, imagePath, infoPath, save);
            cards.Add(card);
        }
    }
}


