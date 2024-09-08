using System;
using System.Collections.Generic;
using System.Text;
using Interpreter;
using GameLibrary;
using Interpreter.Statements;
using System.IO;

class Interpreterr
{ 
    ASTPrinter? printer;
    LexicalAnalyzer lexer = new LexicalAnalyzer();
    Parser? parser;
    public List<Card> SettledCards { get; private set; }
    Record input;
    bool valid = true;

    string filePath1 = "cards.txt";
    string filePath2 = "effects.txt";
   

    public Interpreterr(ASTPrinter printer, List<string> cards = null, List<string> effects = null)
    {
        StartOver();
        this.printer = printer;

        if (!File.Exists(filePath1) || !File.Exists(filePath2))
        {
            Console.WriteLine("File not found.");
            return;
        }
         
        string declaredCards = File.ReadAllText(filePath1);
        string declaredEffects = File.ReadAllText(filePath2);

        try 
        {
            if (declaredCards != null)
            {
               this.Interpret(declaredCards);
            }

            if (declaredEffects != null)
            {
                if (!this.Interpret(declaredEffects))
                {
                    Console.Clear();
                    Console.WriteLine($"Invalid input, bro");
                    valid = false;

                }
                this.Interpret(declaredEffects);
            }

        }
        catch(FileNotFoundException e)
        {
            Console.Clear();
            Console.WriteLine("File not found." + e.Message);
            valid = false;
        }

        if (valid) Console.Clear();  
    }

    void StartOver()
    {
        EffectState.StartOver();
        CardState.StartOver();
        SettledCards = new List<Card>();
    }


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
                Console.WriteLine(e.Message);
                return false;
            }
            catch (Attention a)
            {
                Console.WriteLine(a.Message);
            }
            SettledCards.AddRange(input.SetCards());
            return true;
        }
        else return false;
    }

    public bool CheckSemantic(string code)
    {
        if(valid == false) return false;
        else Console.Clear();

        
        List<Token> list = lexer.GetTokens(code, out string[] lexicalErrors);

        if (lexicalErrors.Length > 0)
        {
            for (int i = 0; i < lexicalErrors.Length; i++)
            {
                Console.WriteLine($"{i+1}, {lexicalErrors[i]}");
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
                    Console.WriteLine(error);
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
                    Console.WriteLine(a.Message);
                }

                if (semanticErrors.Count > 0)
                {
                    foreach (var error in semanticErrors)
                    {
                        Console.WriteLine(error);

                    }
                    return false;
                }
                else return true;
            }
        }
    }
}