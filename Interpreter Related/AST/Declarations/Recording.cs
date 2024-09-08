using System;
using System.Collections.Generic;
using System.Text;
using Interpreter;
using GameLibrary;

namespace Interpreter.Statements
{
    class  Record : IStatement
    {
        bool runned = false;
        List<Card> declaredCards;
        List<IStatement> effects;
        List<IStatement> cards;
        public (int, int) CodeLocation => (0,0);

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
                int start = CardState.Cards.Count; 

                foreach (var card in cards)
                {
                    card.RunIt();
                }

                declaredCards = CardState.Cards.GetRange(start, CardState.Cards.Count - start);

                runned = true;
            }
        }

        
    }
}