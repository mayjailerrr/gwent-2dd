public class StringExpression : Expression
{
    public string Value { get; set; }

    public StringExpression(string value, CodeLocation location) : base(location)
    {
        Value = value;
        Type = ExpressionType.String;
    }

    public override void Evaluate()
    {
        // Evaluate string
    }

    public override bool CheckSemantic(Context context, Scope scope, List<CompilingError> errors)
    {
        // Semantic checks for string
        return true;
    }

    public override string ToString()
    {
        return Value;
    }
}
