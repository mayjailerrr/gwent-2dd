using System;
using System.Collections.Generic;
using System.Text;
using Interpreterr;
using GameLibrary;
using System.IO;

namespace Interpreterr.Statements
{
    class  Record : IStatement
    {
        bool runned = false;
        List<Card> declaredCards;
        List<IStatement> effects;
        List<IStatement> cards;
        public (int, int) CodeLocation => (0,0);
        static int effectsCount = 0;

        static string mainPath = "/home/analaura/Documentos/Compiler";

        public Record(List<IStatement> cards, List<IStatement> effects)
        {
            this.declaredCards = new List<Card>();
            this.effects = effects;
            this.cards = cards;
        }

        public List<Card> SetCards()
        {
            if (!runned) RunIt();
            return declaredCards;
        }

        public bool CheckSemantic(out List<string> errorsList)
        {
            errorsList = new List<string>();

            string attention = "";

            attention += Errors(cards, ref errorsList);
            attention += Errors(effects, ref errorsList);
            
            if (attention != "") 
                throw new Attention(attention);
            
            return errorsList.Count == 0;
        }
            

        static string Errors(List<IStatement> list, ref List<string> errorsList)
        {
            string attention = "";

            for (int i = 0; i < list.Count; i++)
            {
                try
                {
                    if (!list[i].CheckSemantic(out List<string> temperrorsList))
                    {
                        errorsList.AddRange(temperrorsList);
                    }
                }
                catch (Attention a)
                {
                    attention += a.Message + "\n";
                }
            }
            return attention;
        }

        public void RunIt()
        {
            if(!runned)
            {
                int startCardsCount = CardState.Cards.Count; 

                foreach (var card in cards)
                {
                    card.RunIt();
                }

                declaredCards = CardState.Cards.GetRange(startCardsCount, CardState.Cards.Count - startCardsCount);
                runned = true;

                string effectsAttention = WriteTheFiles(EffectState.DeclaredEffects, effectsCount, "Effects/Scripts/", "jpeg");
                string cardsAttention = WriteTheFiles(CardState.DeclaredCards, startCardsCount, "Cards/Scripts/", ".jpeg");

                effectsCount = EffectState.DeclaredEffects.Count;

                if (effectsAttention.Length != 0 || cardsAttention.Length != 0) 
                    throw new Attention(effectsAttention + cardsAttention);
            }
        }

        static string WriteTheFiles(Dictionary<string, string> dict, int start, string folder, string ext)
        {
            string attentions = "";
            int initialSave = start;

            foreach (var item in dict)
            {
                if (start > 0) start--;
                else if (File.Exists(mainPath + folder + item.Key + ext))
                    attentions += $"{item.Key} its already in use \n";
                
                if (attentions.Length == 0) 
                    foreach (var pair in dict)
                    {
                        if (initialSave > 0) initialSave--;
                        else 
                        {
                            StreamWriter sw = new StreamWriter(mainPath + folder + pair.Key + ext);
                            sw.WriteLine(pair.Value);
                            sw.Close();
                        }
                    }
            }

            return attentions;
        }

        public static void Reset() => effectsCount = 0;
    }
}