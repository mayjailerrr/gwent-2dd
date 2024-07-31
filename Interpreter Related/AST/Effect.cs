public class Effect : ASTNode
{
    public string Name { get; set; }
    public Dictionary<string, object> Parameters { get; set; }

    public Effect(string name, CodeLocation location) : base(location)
    {
        Name = name;
        Parameters = new Dictionary<string, object>();
    }

    public override bool CheckSemantic(Context context, Scope scope, List<CompilingError> errors)
    {
        // Semantic checks for the effect
        return true;
    }

    public override void Evaluate()
    {
        // Evaluate effect
    }

    public override string ToString()
    {
        return Name;
    }
}

public class Selector : ASTNode
{
    public string Source { get; set; }
    public bool Single { get; set; }
    public Predicate<unit> Predicate { get; set; }

    public Selector(string source, bool single, Predicate<unit> predicate, CodeLocation location) : base(location)
    {
        Source = source;
        Single = single;
        Predicate = predicate;
    }

    public override bool CheckSemantic(Context context, Scope scope, List<CompilingError> errors)
    {
        // Semantic checks for the selector
        return true;
    }

    public override void Evaluate()
    {
        // Evaluate selector
    }

    public override string ToString()
    {
        return $"{Source} (Single: {Single})";
    }
}

public class PostAction : ASTNode
{
    public string Type { get; set; }
    public Selector Selector { get; set; }

    public PostAction(string type, Selector selector, CodeLocation location) : base(location)
    {
        Type = type;
        Selector = selector;
    }

    public override bool CheckSemantic(Context context, Scope scope, List<CompilingError> errors)
    {
        bool valid = Selector.CheckSemantic(context, scope, errors);
        return valid;
    }

    public override void Evaluate()
    {
        // Evaluate post-action
    }

    public override string ToString()
    {
        return $"PostAction {Type} with Selector {Selector}";
    }
}

   
