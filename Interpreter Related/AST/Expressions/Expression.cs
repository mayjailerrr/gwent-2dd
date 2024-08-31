
using System;
using System.Collections.Generic;
using System.Text;

namespace Interpreter
{
    interface IExpression
    {
        ExpressionType Type { get; }
        (int, int) CodeLocation { get; }
        object Interpret();
        string? ToString();
        public bool CheckSemantic(out List<string> errorsList);
        public bool CheckSemantic(out string error);
    }
    interface IVisitable<T>
    {
        T Accept(IVisitor<T> visitor);
    }

    interface IVisitor<T>
    {
        T Visit(IVisitable<T> visitable);
    }

    abstract class Expression<T>: IVisitable<T>, IExpression
    {
        public virtual T Accept(IVisitor<T> visitor) => visitor.Visit(this);
        public abstract bool CheckSemantic(out List<string> errorsList);
        public abstract bool CheckSemantic(out string error);
        public abstract ExpressionType Type { get; }
        public abstract (int, int) CodeLocation { get; protected set;}
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
