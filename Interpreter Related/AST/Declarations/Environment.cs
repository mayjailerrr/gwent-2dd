using System;
using System.Collections.Generic;
using System.Text;
using Interpreter;

class Environment
{
    static Environment? global;
    Environment? parent;
    Dictionary<string, IExpression> variables = new Dictionary<string, IExpression>();
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
                global.variables.Add("context", InterpreterContext.Context);
               
            }
            return global;
        }
    }

    IExpression Get(string key)
    {
        if (parent.variables.ContainsKey(key))
        {
            return parent.variables[key];
        }
        else
        {
            return parent.Get(key);
        }
    }
    public void Set(IExpression value, Token variable)
    {
        if (variable.Value == "context") throw new ParsingError($"I already planned some uses for the word 'context', sorry not sorry, change it {variable.Location}");
        if (variables.ContainsKey(variable.Value))
        {
            this.variables[variable.Value] = value ?? throw new ParsingError($"This variable is already in use, pay attention to what you wrote in {variable.Location}");
        }
        else if (Search(variable.Value))
        {
            Change(variable.Value, value);
        } 
        else
        {
            this.variables.Add(variable.Value, value);
        }
    }
    void Change(string key, IExpression value)
    {
        if (parent.variables.ContainsKey(key))
        {
            parent.variables[key] = value;
        }
        else
        {
            parent.Change(key, value);
        }
    }
    public bool Contains(string key) => variables.ContainsKey(key) || Search(key);
    bool Search(string key) => parent is null ? false : parent.Contains(key);
    public void Reset()
    {
        global = new Environment();
    }

    public IExpression this[string variable]
    {
        get
        {
            try
            {
                 if (variables.ContainsKey(variable))
                {
                    return variables[variable];
                }
                else if (Search(variable))
                {
                    return Get(variable);
                }
                else
                {
                    throw new KeyNotFoundException();
                }
            }
            catch (KeyNotFoundException)
            {
                throw new ParsingError($"You have not declared this variable: {variable}");
            }
           
        }
        private set
        {
            try
            {
                 if (variables.ContainsKey(variable))
                {
                    variables[variable] = value;
                }
                else if (Search(variable))
                {
                    Change(variable, value);
                }
                else
                {
                    throw new KeyNotFoundException();
                }
            }
            catch (KeyNotFoundException)
            {
                throw new ParsingError($"You have not declared this variable: {variable}");
            }
           
        }
    }
}