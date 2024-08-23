using System;
using System.Collections.Generic;
using System.Text;
using Interpreter;

class Environment
{
    static Environment? global;
    Environment? parent;
    Dictionary<string, IExpression> expr = new Dictionary<string, IExpression>();
    public Environment(Environment? parent = null)
    {
        if(parent != null)
        {
            this.parent = parent;
        }
    }

    public static Environment Global
    {
        get
        {
            if (global is null)
            {
                global = new Environment();
                global.parent = null;
                global.expr.Add("context", InterpreterContext.Context); //to-do
               
            }
            return global;
        }
    }

    IExpression Get(string key)
    {
        if (parent.expr.ContainsKey(key))
        {
            return parent.expr[key];
        }
        else
        {
            return parent.Get(key);
        }
    }
    public void Set(IExpression value, Token token)
    {
        if (token.Value == "context") throw new ParsingError($"I already planned some uses for the word 'context', sorry not sorry, change it {token.CodeLocation.Item1},{token.CodeLocation}");
        if (expr.ContainsKey(token.Value))
        {
            this.expr[token.Value] = value ?? throw new ParsingError($"This token is already in use, pay attention to what you wrote in {token.CodeLocation.Item1},{token.CodeLocation.Item2 + token.Value.Length}");
        }
        else if (Search(token.Value))
        {
            Change(token.Value, value);
        } 
        else
        {
            this.expr.Add(token.Value, value);
        }
    }
    void Change(string key, IExpression value)
    {
        if (parent.expr.ContainsKey(key))
        {
            parent.expr[key] = value;
        }
        else
        {
            parent.Change(key, value);
        }
    }
    public bool Contains(string key) => expr.ContainsKey(key) || Search(key);
    bool Search(string key) => parent is null ? false : parent.Contains(key);
    public void Reset()
    {
        global = new Environment();
    }

    public IExpression this[string token]
    {
        get
        {
            try
            {
                 if (expr.ContainsKey(token))
                {
                    return expr[token];
                }
                else if (Search(token))
                {
                    return Get(token);
                }
                else
                {
                    throw new KeyNotFoundException();
                }
            }
            catch (KeyNotFoundException)
            {
                throw new ParsingError($"You have not declared this token: {token}");
            }
           
        }
        private set
        {
            try
            {
                 if (expr.ContainsKey(token))
                {
                    expr[token] = value;
                }
                else if (Search(token))
                {
                    Change(token, value);
                }
                else
                {
                    throw new KeyNotFoundException();
                }
            }
            catch (KeyNotFoundException)
            {
                throw new ParsingError($"You have not declared this token: {token}");
            }
           
        }
    }
}