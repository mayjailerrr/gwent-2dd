
using System;
using System.Collections.Generic;
using System.Text;

namespace Interpreter
{
    public interface IExpression
    {
        public CodeLocation Location { get; }
        public bool CheckSemantic(out List<string> errors);
        public bool CheckSemantic(out string error);
        object Interpret();
        string? ToString();
        ExpressionType Return { get; }
    }
    public interface IVisitable<T>
    {
        T Accept(IVisitor<T> visitor);
    }

    public interface IVisitor<T>
    {
        T Visit(IVisitable<T> visitable);
    }

    public abstract class Expression<T>: IVisitable<T>, IExpression
    {
        public virtual T Accept(IVisitor<T> visitor) => visitor.Visit(this);
        public abstract bool CheckSemantic(out List<string> errors);
        public abstract bool CheckSemantic(out string error);
        public abstract ExpressionType Return { get; }
        public abstract CodeLocation Location { get; protected set;}
        public abstract object Interpret();
    }

    public enum ExpressionType
    {
        Number,
        String,
        Boolean,
        Card,
        Void,
        List,
        Object,
        Context,
        Predicate
    }

}
