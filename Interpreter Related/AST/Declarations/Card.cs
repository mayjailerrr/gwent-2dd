public class Card : IExpression
{
    public string Type { get; set; }
    public string Name { get; set; }
    public string Faction { get; set; }
   // public BinaryExpression Power { get; set; }
    public List<string> Range { get; set; }
    public List<OnActivation> OnActivations { get; set; }

    public Card(CodeLocation location) : base(location)
    {
        Type = string.Empty;
        Name = string.Empty;
        Faction = string.Empty;
        // Power = new BinaryExpression(); nimodo
        Range = new();
        OnActivations = new();
    }

    public override bool CheckSemantic(Context context, Scope scope, List<CompilingError> errors)
    {
        //bool valid = Power.CheckSemantic(context, scope, errors);
        // if (Power.Type != ExpressionType.Number)
        // {
        //     errors.Add(new CompilingError(Location, ErrorCode.Unknown, "The Power must be numerical"));
        //     valid = false;
        // }

        bool valid = true; //for the time being

        foreach (var onActivation in OnActivations)
        {
            valid &= onActivation.CheckSemantic(context, scope, errors);
        }

        return valid;
    }

    public override void Interpret()
    {
       // Power.Interpret();
        foreach (var onActivation in OnActivations)
        {
            onActivation.Interpret();
        }
    }

    public override string ToString()
    {
        //Power is not implemented yet
        return $"Card {Name} (Type: {Type}, Faction: {Faction}, Range: [{string.Join(", ", Range)}])";
    }
}


public class OnActivation : ASTNode
{
    public Effect Effect { get; set; }
    public Selector Selector { get; set; }
    public PostAction PostAction { get; set; }

    public OnActivation(CodeLocation location, Effect effect, Selector selector, PostAction postAction) : base(location)
    {
        Effect = effect ?? throw new ArgumentNullException(nameof(effect));
        Selector = selector ?? throw new ArgumentNullException(nameof(selector));
        PostAction = postAction;
    }

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

    public override void Interpret()
    {
        Effect.Interpret();
        Selector.Interpret();
        PostAction?.Interpret();
    }
}

