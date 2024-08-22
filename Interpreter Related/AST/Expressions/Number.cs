using System;
using System.Collections.Generic;
using System.Text;
using Interpreter;
public struct Number
{
    public double Value;
    public Number Opposite => new Number(-Value);
    public Number(double value) => Value = value;
    public override string ToString() => Value.ToString();

    public bool Greater(object ob) => ob is Number value && this.Value > value.Value;
    public bool Less(object ob) => ob is Number value && this.Value < value.Value;
    public bool GreaterEqual(object ob) => ob is Number value && this.Value >= value.Value;
    public bool LessEqual(object ob) => ob is Number value && this.Value <= value.Value;

    public Number Plus(Number value) => new Number(this.Value + value.Value);
    public Number Minus(Number value) => new Number(this.Value - value.Value);
    public Number Multiply(Number value) => new Number(this.Value * value.Value);
    public Number Divide(Number value) => new Number(this.Value / value.Value);
    public Number PowerTo(Number value) => new Number(Math.Pow(this.Value, value.Value));
   
    public override bool Equals(object? obj) => obj is Number value && this.Value == value.Value;
    public override int GetHashCode() => HashCode.Combine(Value);
}

