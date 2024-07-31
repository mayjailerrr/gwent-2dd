public class NumberExpression : Expression
{
    public double Value { get; set; }

    public NumberExpression(double value, CodeLocation location) : base(location)
    {
        Value = value;
        Type = ExpressionType.Number;
    }

    public override void Evaluate()
    {
        // Evaluate number
    }

    public override bool CheckSemantic(Context context, Scope scope, List<CompilingError> errors)
    {
        // Semantic checks for number
        return true;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}

// Add other attribute classes as needed
