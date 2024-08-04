namespace Interpreter
{
    public class Lexicalizing
    {
        private static LexicalAnalyzer? Lexer;
        public static LexicalAnalyzer Lexical
        {
            get
            {
                if (Lexer == null)
                {
                    Lexer = new LexicalAnalyzer();

                    Lexer.RegisterKeyword("effect", "EFFECT");
                    Lexer.RegisterKeyword("Params", "PARAMS");
                    Lexer.RegisterKeyword("Action", "ACTION");

                    // card definition keywords
                    Lexer.RegisterKeyword("card", "CARD");
                    Lexer.RegisterKeyword("Type", "TYPE");
                    Lexer.RegisterKeyword("Name", "NAME");
                    Lexer.RegisterKeyword("Faction", "FACTION");
                    Lexer.RegisterKeyword("Power", "POWER");
                    Lexer.RegisterKeyword("Range", "RANGE");
                    Lexer.RegisterKeyword("OnActivation", "ONACTIVATION");
                    Lexer.RegisterKeyword("Effect", "EFFECT");
                    Lexer.RegisterKeyword("Selector", "SELECTOR");
                    Lexer.RegisterKeyword("Source", "SOURCE");
                    Lexer.RegisterKeyword("Single", "SINGLE");
                    Lexer.RegisterKeyword("Predicate", "PREDICATE");
                    Lexer.RegisterKeyword("PostAction", "POSTACTION");
                    Lexer.RegisterKeyword("Amount", "AMOUNT");

                    Lexer.RegisterKeyword("if", "IF");
                    Lexer.RegisterKeyword("else", "ELSE");
                    Lexer.RegisterKeyword("while", "WHILE");
                    Lexer.RegisterKeyword("for", "FOR");
                    Lexer.RegisterKeyword("return", "RETURN");
                    Lexer.RegisterKeyword("break", "BREAK");
                    Lexer.RegisterKeyword("continue", "CONTINUE");
                    Lexer.RegisterKeyword("in", "IN");

                    Lexer.RegisterKeyword("true", "TRUE");
                    Lexer.RegisterKeyword("false", "FALSE");
                }

                return Lexer;
            }
        }

    }
}
