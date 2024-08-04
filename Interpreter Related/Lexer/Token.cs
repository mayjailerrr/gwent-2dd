namespace Interpreter
{
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
        Operator,
        EOF,
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
}
