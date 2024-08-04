using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Interpreter
{
    public class LexicalAnalyzer
    {
        private Dictionary<string, string> keywords = new Dictionary<string, string>();
        public IEnumerable<string> Keywords { get { return keywords.Keys; } }

        public void RegisterKeyword(string keyword, string tokenValue)
        {
            this.keywords[keyword] = tokenValue;
        }

        public IEnumerable<Token> GetTokens(string fileName, string code, List<LexicalizingError> errors)
        {
            List<Token> tokens = new List<Token>();

            var tokenPatterns = new Dictionary<string, string>
            {
                { "Keyword", $@"\b({string.Join("|", keywords.Keys)})\b" },
                { "Operator", @"==|!=|<=|>=|=|--|\+\+|&&|\|\||!|@|@@|=>|<|>|^|\+|\-|\*|\/|\.|\^" },
                { "Identifier", @"\b[a-zA-Z_]\w*\b" },
                { "Number", @"\b\d+(\.\d+)?\b" },
                { "String", @"""[^""\\]*(?:\\.[^""\\]*)*""" },
                { "Symbol", @"[{}:;,\[\]()]" },
                { "Whitespace", @"\s+" }
            };

            var combinedPattern = string.Join("|", tokenPatterns.Select(kv => $"(?<{kv.Key}>{kv.Value})"));
            var regex = new Regex(combinedPattern, RegexOptions.Compiled);

            var matches = regex.Matches(code);
            int lastMatchEnd = 0;

            foreach (Match match in matches)
            {
                CodeLocation location = new CodeLocation { File = fileName, Line = 0, Column = 1 };

                if (match.Groups["Keyword"].Success)
                {
                    tokens.Add(new Token(TokenType.Keyword, match.Value, location));
                }
                else if (match.Groups["Operator"].Success)
                {
                    tokens.Add(new Token(TokenType.Operator, match.Value, location));
                }
                else if (match.Groups["Identifier"].Success)
                {
                    tokens.Add(new Token(TokenType.Identifier, match.Value, location));
                }
                else if (match.Groups["Number"].Success)
                {
                    tokens.Add(new Token(TokenType.Number, match.Value, location));
                }
                else if (match.Groups["String"].Success)
                {
                    // Extract the content within the quotes
                    var stringValue = match.Value.Substring(1, match.Value.Length - 2);
                    tokens.Add(new Token(TokenType.String, stringValue, location));
                }
                else if (match.Groups["Symbol"].Success)
                {
                    tokens.Add(new Token(TokenType.Symbol, match.Value, location));
                }
                else if (!match.Groups["Whitespace"].Success)
                {
                    errors.Add(new LexicalizingError(location, ErrorCode.Unknown, $"Unexpected token: {match.Value}"));
                }

                lastMatchEnd = match.Index + match.Length;

                for (int i = 0; i < match.Index - 1; i++)
                {
                    if (code[i] == '\n')
                    {
                        location.Line++;
                        location.Column = 1;
                    }
                    else
                    {
                        location.Column++;
                    }
                } 
            }

            if (lastMatchEnd == code.Length)
            {
                tokens.Add(new Token(TokenType.EOF, "End of your code", new CodeLocation { File = fileName, Line = 0, Column = 0 }));
            }
            else
            {
                errors.Add(new LexicalizingError(new CodeLocation { File = fileName, Line = 0, Column = lastMatchEnd }, ErrorCode.Unknown, $"Unexpected token: {code.Substring(lastMatchEnd)}"));
            }

            CheckSyntax(tokens, errors);

            return tokens;
        }

        private void CheckSyntax(List<Token> tokens, List<LexicalizingError> errors)
        {
            Stack<Token> braces = new Stack<Token>();

            for (int i = 0; i < tokens.Count; i++)
            {
                var token = tokens[i];

                if (token.Type == TokenType.Symbol)
                {
                    if (token.Value == "{" || token.Value == "[")
                    {
                        braces.Push(token);
                    }
                    else if (token.Value == "}" || token.Value == "]")
                    {
                        if (braces.Count == 0)
                        {
                            errors.Add(new LexicalizingError(token.Location, ErrorCode.UnmatchedBrace, $"Unmatched closing brace: {token.Value}"));
                        }
                        else
                        {
                            var openBrace = braces.Pop();
                            if ((openBrace.Value == "{" && token.Value != "}") || (openBrace.Value == "[" && token.Value != "]"))
                            {
                                errors.Add(new LexicalizingError(token.Location, ErrorCode.UnmatchedBrace, $"Unmatched open brace: {token.Value}"));
                            }
                        }
                    }
                }
            }

            // Check for unmatched braces remaining in the stack
            while (braces.Count > 0)
            {
                var openBrace = braces.Pop();
                errors.Add(new LexicalizingError(openBrace.Location, ErrorCode.MissingBrace, $"Missing closing brace for: {openBrace.Value
                }"));
            }
        } 
    }

    public class LexicalizingError
    {
        public CodeLocation Location { get; }
        public ErrorCode Code { get; }
        public string Message { get; }

        public LexicalizingError(CodeLocation location, ErrorCode code, string message)
        {
            Location = location;
            Code = code;
            Message = message;
        }

        public override string ToString()
        {
            return $"{Location}: {Code} - {Message}";
        }
    }

    public enum ErrorCode
    {
        Unknown,
        MissingBrace,
        UnmatchedBrace,
        MissingColon,
    }


}

