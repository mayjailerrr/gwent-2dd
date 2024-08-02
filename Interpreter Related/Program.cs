
string code = File.ReadAllText("./code");  

LexicalAnalyzer lex = Lexicalizing.Lexical;

List<LexicalizingError> errors = new List<LexicalizingError>();

var tokens = lex.GetTokens("code.txt", code, errors);

foreach (var token in tokens)
{
    Console.WriteLine(token);
}

foreach (var error in errors)
{
    Console.WriteLine($"Lexical Error: {error}");
}