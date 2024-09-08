using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;

namespace Interpreter
{
    public class LexicalAnalyzer
    {
        Regex whitespace = new Regex(@"\s+");
        Regex phrase = new Regex(@"[_a-zA-Z]+[_a-zA-Z0-9]*"); //+
        Regex symbol = new Regex(@"==|!=|<=|>=|=|--|\+\+|&&|\|\||!|@|@@|=>|<|>|^|\+|\-|\*|\/|\.|\^");
        Regex number = new Regex(@"\d+(\.\d+)?"); //+

        private void AddMatch(List<Token> tokens, string match, int line, ref int column, TokenType type = TokenType.Sign)
        {
           try
           {
                tokens.Add(new Token((type == TokenType.Sign? Token.allTypes[match] : type), match, line + 1, column + 1));
           }
           catch (KeyNotFoundException)
           {
                tokens.Add(new Token(TokenType.Identifier, match, line + 1, column + 1));
                Token.allTypes.Add(match, TokenType.Identifier);
           }
           column += match.Length - 1;

        } 
       

        public List<Token> GetTokens(string code, out string[] errors)
        {
            List<Token> tokens = new List<Token>();
            List<string> errorList = new List<string>();
            errors = new string[0];
            string[] input = code.Split('\n');
            bool quoteMarks = false;
            code.Trim();
            string actualToken = "";
            

            string current = "";
            int line = 0;
            int column = 0;
            (int, int) lastMatched = (0, 0);

            do 
            {
                current = input[line];

                do
                {
                    if (current[column] == '$' && !quoteMarks && (line != input.Length - 1 && column != current.Length - 1))
                    {
                        errorList.Add("Unexpected token \'$\' at " + line + "," + column);
                        actualToken = "";
                        column++;
                        continue;
                    }


                    try
                    {
                        if (current[column] == '/' && current[column + 1] == '/')
                            break;
                    }
                    catch (Exception) {}

                    if (current[column] == '"')
                    {
                        quoteMarks = !quoteMarks;
                        actualToken += current[column];
                        lastMatched = (line + 1, column + 1);
                        
                        if (!quoteMarks)
                        {
                            tokens.Add(new Token(TokenType.String, actualToken, line + 1, column + 1)); 
                            actualToken = "";
                        }
                    }

                    else if (quoteMarks)
                        actualToken += current[column];
                   
                    else 
                    {
                       var matchers = new Dictionary<Regex, Action>
                        {
                            { phrase, () => AddMatch(tokens, phrase.Match(current, column).Value, line, ref column) },
                            { number, () => AddMatch(tokens, number.Match(current, column).Value, line, ref column, TokenType.Number) },
                            { symbol, () => AddMatch(tokens, symbol.Match(current, column).Value, line, ref column) }
                        };

                        bool matched = false;

                        foreach (var matcher in matchers)
                        {
                            if (matcher.Key.IsMatch(current[column].ToString()))
                            {
                                matcher.Value();
                                matched = true;
                                break;
                            }
                        }

                        if (!matched && !whitespace.IsMatch(current[column].ToString()))
                        {
                            errorList.Add("Unexpected token '" + current[column] + "' at " + line + "," + column);
                        }

                    }

                    column++;
                } while (column < current.Length);

                column = 0;
                line++;
           
            } while (line < input.Length);
          
            if (quoteMarks)
                errorList.Add("Missing closing quote at " + lastMatched.Item1 + "," + lastMatched.Item2);
            if (errorList.Count > 0)
            {
                errors = errorList.ToArray();
                return new List<Token>();
            }

            tokens.Add(new Token(TokenType.Sign, "$", line, column));
            return tokens;
        }
    }
}

