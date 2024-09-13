using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;

interface IManageable<T>
{
    T Value { get; }
}

class OwnValue : IManageable<string>
{
   string value;
   public string Value => value;

    public OwnValue(string value)
    {
        this.value = value;
    }

   public static OwnValue Plus(OwnValue left, OwnValue right, bool space = false) => new OwnValue(left.Value + (space ? " " : "") + right.Value);

   public override bool Equals(object obj)
   {
        return (obj is OwnValue own && this.Value == own.Value) || (obj is string str && this.Value == str);
   }

   public override int GetHashCode()
   {
        return HashCode.Combine(value);
   }

   public override string ToString() => value;
   public int Length => value.Length;
}
