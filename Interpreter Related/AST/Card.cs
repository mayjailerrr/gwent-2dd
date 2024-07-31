public class Card : ASTNode
{
    public string Type { get; set; }
    public string Name { get; set; }
    public string Faction { get; set; }
    public Expression Power { get; set; }
    public List<string> Range { get; set; }
    public List<OnActivation> OnActivations { get; set; }

    public Card(CodeLocation location) : base(location)
    {
        Range = new List<string>();
        OnActivations = new List<OnActivation>();
    }

    public override bool CheckSemantic(Context context, Scope scope, List<CompilingError> errors)
    {
        bool valid = Power.CheckSemantic(context, scope, errors);
        if (Power.Type != ExpressionType.Number)
        {
            errors.Add(new CompilingError(Location, ErrorCode.Invalid, "The Power must be numerical"));
            valid = false;
        }

        foreach (var onActivation in OnActivations)
        {
            valid &= onActivation.CheckSemantic(context, scope, errors);
        }

        return valid;
    }

    public override void Evaluate()
    {
        Power.Evaluate();
        foreach (var onActivation in OnActivations)
        {
            onActivation.Evaluate();
        }
    }

    public override string ToString()
    {
        return $"Card {Name} (Type: {Type}, Faction: {Faction}, Power: {Power}, Range: [{string.Join(", ", Range)}])";
    }
}

public class OnActivation : ASTNode
{
    public Effect Effect { get; set; }
    public Selector Selector { get; set; }
    public PostAction PostAction { get; set; }

    public OnActivation(CodeLocation location) : base(location) { }

    public override bool CheckSemantic(Context context, Scope scope, List<CompilingError> errors)
    {
        bool valid = Effect.CheckSemantic(context, scope, errors);
        valid &= Selector.CheckSemantic(context, scope, errors);
        if (PostAction != null)
        {
            valid &= PostAction.CheckSemantic(context, scope, errors);
        }
        return valid;
    }

    public override void Evaluate()
    {
        Effect.Evaluate();
        Selector.Evaluate();
        PostAction?.Evaluate();
    }
}
