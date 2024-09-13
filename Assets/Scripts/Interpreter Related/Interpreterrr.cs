using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameLibrary;
using Interpreterr.Statements;
using System.Text;
using System.IO;

namespace Interpreterr
{
    class Interpreterrr
{
    ASTPrinter printer;
    LexicalAnalyzer lexer = new LexicalAnalyzer();
    Parser parser;
    public List<Card> SettledCards { get; private set; }
    Statements.Record input;
    public bool valid = true;

    string mainPath = "/home/analaura/Documentos/Compiler";
   

    public Interpreterrr(ASTPrinter printer, List<string> cards = null, List<string> effects = null, string path = "")
    {
        StartOver();
        this.printer = printer;

        if (path != "") mainPath = path;

        try
        {
            if (!(effects is null))
                foreach (var item in effects)
                {
                    StreamReader sr = new StreamReader(mainPath + "Effects/Scripts/" + item + ".jpeg");
                    this.Interpret(sr.ReadLine());
                    sr.Close();
                }
            if (!(cards is null))
                foreach (var item in cards)
                {
                    StreamReader sr = new StreamReader(mainPath + "Cards/Scripts/" + item + ".jpeg");
                    if (!this.Interpret(sr.ReadLine()))
                    {
                        RemoveMessage();
                        Log("Incorrect load of the effects of these cards. Try Again");
                        valid = false;
                    }
                    sr.Close();
                }
        }
        catch (FileNotFoundException f)
        {
            RemoveMessage();
            Log("Incorrect loading:" + f.Message);
            valid = false;
        }

        if (valid) RemoveMessage();
    }

    void StartOver()
    {
        EffectState.StartOver();
        CardState.StartOver();
        Statements.Record.Reset();
        SettledCards = new List<Card>();
    }

    void Log(string text) => printer.Print(text);
    void RemoveMessage() => printer.Clear();


    public bool Interpret(string code)
    {

        if (CheckSemantic(code))
        {
            try
            {
                input.RunIt();
            }
            catch (RunningError e)
            {
                Log(e.Message);
                return false;
            }
            catch (Attention a)
            {
                Log(a.Message);
            }
            SettledCards.AddRange(input.SetCards());
            return true;
        }
        else return false;
    }

    public bool CheckSemantic(string code)
    {
        if(valid == false) return false;
        else RemoveMessage();

        
        List<Token> list = lexer.GetTokens(code, out string[] lexicalErrors);

        if (lexicalErrors.Length > 0)
        {
            for (int i = 0; i < lexicalErrors.Length; i++)
            {
                Log($"{i + 1}, {lexicalErrors[i]}");
            }
            return false;
        }

        else 
        {
            parser = new Parser(list);
            input = parser.Parse();

            if (parser.Errors.Count > 0)
            {
                foreach (var error in parser.Errors)
                {
                    Log(error);
                }
                return false;
            }

            else 
            {
                List<string> semanticErrors = new List<string>();

                try 
                {
                    input.CheckSemantic(out semanticErrors);
                }

                catch (Attention a)
                {
                    Log(a.Message);
                }

                if (semanticErrors.Count > 0)
                {
                    foreach (var error in semanticErrors)
                    {
                        Log(error);

                    }
                    return false;
                }
                else return true;
            }
        }
    }
}


}
