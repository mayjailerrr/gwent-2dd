public abstract class Expression : ASTNode
{
    public ExpressionType Type { get; protected set; }

    protected Expression(CodeLocation location) : base(location) { }

    public abstract void Evaluate();
}

public enum ExpressionType
{
    Number,
    String,
    Boolean
}
