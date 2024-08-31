using System;
using System.Collections.Generic;
using System.Text;
using Interpreter;

class Attention : Exception
{
    public override string Message { get; }
    public Attention(string message)
    {
        Message = message;
    }
}

class RunningError : Exception
{
    public override string Message { get; }
    public RunningError(string message)
    {
        Message = message;
    }
}
class ParsingError : Exception
{
    public override string Message { get; }
    public ParsingError(string message)
    {
        Message = message;
    }
}

