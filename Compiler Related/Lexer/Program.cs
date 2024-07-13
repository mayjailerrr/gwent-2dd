using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public class CodeLocation
{
    public string File { get; set; } = string.Empty;
    public int Line { get; set; }
    public int Column { get; set; }

    public override string ToString()
    {
        return $"{File} (Line: {Line}, Column: {Column})";
    }
}
public enum TokenType
{
    Keyword,
    Identifier,
    Number,
    String,
    Symbol,
}
public class Token
{
    public TokenType Type { get; }
    public string Value { get; }
    public CodeLocation Location { get; }

    public Token(TokenType type, string value, CodeLocation location)
    {
        Type = type;
        Value = value;
        Location = location;
    }

    public override string ToString()
    {
        return $"{Type}: {Value} at {Location}";
    }
}
public class CompilingError
{
    public CodeLocation Location { get; }
    public ErrorCode Code { get; }
    public string Message { get; }

    public CompilingError(CodeLocation location, ErrorCode code, string message)
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


public class LexicalAnalyzer
{
    private Dictionary<string, string> operators = new Dictionary<string, string>();
    private Dictionary<string, string> keywords = new Dictionary<string, string>();
    private Dictionary<string, string> texts = new Dictionary<string, string>();

    public IEnumerable<string> Keywords { get { return keywords.Keys; } }

    public void RegisterOperator(string op, string tokenValue)
    {
        this.operators[op] = tokenValue;
    }

    public void RegisterKeyword(string keyword, string tokenValue)
    {
        this.keywords[keyword] = tokenValue;
    }

    public void RegisterText(string start, string end)
    {
        this.texts[start] = end;
    }

    public IEnumerable<Token> GetTokens(string fileName, string code, List<CompilingError> errors)
    {
        List<Token> tokens = new List<Token>();

        var tokenPatterns = new Dictionary<string, string>
        {
            { "Keyword", $@"\b({string.Join("|", keywords.Keys)})\b" },
            { "Identifier", @"\b[a-zA-Z_]\w*\b" },
            { "Number", @"\b\d+(\.\d+)?\b" },
            { "String", @"""([^""\\]|\\.)*""" },
            { "Symbol", @"[{}:;,\[\]()\.\+\-*/]|--|\+\+" },
            { "Whitespace", @"\s+" }
        };

        var combinedPattern = string.Join("|", tokenPatterns.Select(kv => $"(?<{kv.Key}>{kv.Value})"));
        var regex = new Regex(combinedPattern, RegexOptions.Compiled);

        var matches = regex.Matches(code);
        foreach (Match match in matches)
        {
            CodeLocation location = new CodeLocation { File = fileName, Line = 0, Column = 1 };

            if (match.Groups["Keyword"].Success)
            {
                tokens.Add(new Token(TokenType.Keyword, match.Value, location));
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
                tokens.Add(new Token(TokenType.String, match.Value, location));
            }
            else if (match.Groups["Symbol"].Success)
            {
                tokens.Add(new Token(TokenType.Symbol, match.Value, location));
            }
            else if (!match.Groups["Whitespace"].Success)
            {
                errors.Add(new CompilingError(location, ErrorCode.Unknown, $"Unexpected token: {match.Value}"));
            }

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

        CheckSyntax(tokens, errors);

        return tokens;
    }

    private void CheckSyntax(List<Token> tokens, List<CompilingError> errors)
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
                        errors.Add(new CompilingError(token.Location, ErrorCode.UnmatchedBrace, $"Unmatched closing brace: {token.Value}"));
                    }
                    else
                    {
                        var openBrace = braces.Pop();
                        if ((openBrace.Value == "{" && token.Value != "}") || (openBrace.Value == "[" && token.Value != "]"))
                        {
                            errors.Add(new CompilingError(token.Location, ErrorCode.UnmatchedBrace, $"Unmatched open brace: {token.Value}"));
                        }
                    }
                }
            }
            else if (token.Type == TokenType.Keyword)
            {
                if (char.IsUpper(token.Value[0]))
                {
                    //Check if the previous token is a point or a colon
                    if (tokens[i - 1].Value == "." || tokens[i + 1].Value == ":")
                    {
                        continue;      
                    }
                    
                    else
                    {
                        errors.Add(new CompilingError(token.Location, ErrorCode.MissingColon, "Expected colon after keyword"));
                    }
                }
                
            }
            else if (token.Type == TokenType.Identifier || token.Type == TokenType.Number || token.Type == TokenType.String)
            {
                continue;
            }
        }

        // Check for unmatched braces remaining in the stack
        while (braces.Count > 0)
        {
            var openBrace = braces.Pop();
            errors.Add(new CompilingError(openBrace.Location, ErrorCode.MissingBrace, $"Missing closing brace for: {openBrace.Value
            }"));
        }
    }
}

class Program
{
    static void Main()
    {
       // string code = @"
        // effect {
        //     Name: ""Damage"",
        //     Params: {
        //         Amount: Number
        //     },
        //     Action: (targets, context) => {
        //         for target in targets { 
        //             i = 0; 
        //             while (i++ < Amount)
        //                 target.Power -= 1;
        //         };
        //     }
        // }
        // ";

        string code = @"
        card {
            Type: ""Gold"",
            Name: ""Witch"",
            Faction: ""Northern Realms"",
            Power: 10,
            Range: [""Melee"",""Ranged""],
            OnActivation: [
                {
                    Effect: {
                        Name: ""Damage"", 
                        Amount: 5 
                    },
                    Selector: {
                        Source: ""board"", 
                        Single: false, //for default is false
                        Predicate: (unit) => unit.Faction == ""Northern Realms""
                    },
                    PostAction: {
                        Type: ""ReturnToDeck"",
                        Selector: { 
                            Source: ""parent"",
                            Single: false,
                            Predicate: (unit) => unit.Power < 1
                        }
                    }
                },
                {
                    Effect: ""Draw"" /*if it's put a string is equivalent to {Name: ""Draw""}*/
                }
            ]
        }";

        LexicalAnalyzer lexer = new LexicalAnalyzer();
        lexer.RegisterKeyword("effect", "EFFECT");
        lexer.RegisterKeyword("Params", "PARAMS");
        lexer.RegisterKeyword("Action", "ACTION");
        lexer.RegisterOperator("{", "{");
        lexer.RegisterOperator("}", "}");
        lexer.RegisterOperator(":", ":");
        lexer.RegisterOperator(",", ",");

        // card definition keywords
        lexer.RegisterKeyword("card", "CARD");
        lexer.RegisterKeyword("Type", "TYPE");
        lexer.RegisterKeyword("Name", "NAME");
        lexer.RegisterKeyword("Faction", "FACTION");
        lexer.RegisterKeyword("Power", "POWER");
        lexer.RegisterKeyword("Range", "RANGE");
        lexer.RegisterKeyword("OnActivation", "ONACTIVATION");
        lexer.RegisterKeyword("Effect", "EFFECT");
        lexer.RegisterKeyword("Selector", "SELECTOR");
        lexer.RegisterKeyword("Source", "SOURCE");
        lexer.RegisterKeyword("Single", "SINGLE");
        lexer.RegisterKeyword("Predicate", "PREDICATE");
        lexer.RegisterKeyword("PostAction", "POSTACTION");
        lexer.RegisterKeyword("Amount", "AMOUNT");
        lexer.RegisterKeyword("true", "TRUE");
        lexer.RegisterKeyword("false", "FALSE");

        // operators
        lexer.RegisterOperator("++", "INCREMENT");
        lexer.RegisterOperator("--", "DECREMENT");
        lexer.RegisterOperator("+", "PLUS");
        lexer.RegisterOperator("-", "MINUS");
        lexer.RegisterOperator("*", "MULTIPLY");
        lexer.RegisterOperator("/", "DIVIDE");


        List<CompilingError> errors = new List<CompilingError>();
        var tokens = lexer.GetTokens("example.txt", code, errors);

        foreach (var token in tokens)
        {
            Console.WriteLine(token);
        }

        foreach (var error in errors)
        {
            Console.WriteLine($"Error: {error}");
        }
    }
}

