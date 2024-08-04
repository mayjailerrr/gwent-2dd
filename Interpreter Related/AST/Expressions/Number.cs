using System;
using System.Collections.Generic;
using System.Text;

namespace Interpreter
{
    public struct Number
    {
        public double Value;
        public Number Opposite => new Number(-Value);
        public Number(double value) => Value = value;
        public override string ToString() => Value.ToString();
        public Number Plus(Number value) => new Number(this.Value + value.Value);
        public Number Minus(Number value) => new Number(this.Value - value.Value);
        public Number Multiply(Number value) => new Number(this.Value * value.Value);
        public Number Divide(Number value) => new Number(this.Value / value.Value);
        public Number Pow(Number value) => new Number(Math.Pow(this.Value, value.Value));
        public bool GreaterThan(object ob) => ob is Number value && this.Value > value.Value;
        public bool LessThan(object ob) => ob is Number value && this.Value < value.Value;
        public bool GreaterOrEqual(object ob) => ob is Number value && this.Value >= value.Value;
        public bool LessOrEqual(object ob) => ob is Number value && this.Value <= value.Value;
        public override bool Equals(object? obj) => obj is Number value && this.Value == value.Value;

        public override int GetHashCode() => HashCode.Combine(Value);
    }
}
