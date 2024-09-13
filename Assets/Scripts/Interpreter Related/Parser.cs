using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Interpreterr;
using Interpreterr.Expressions;
using Interpreterr.Statements;
using System.Linq;

class Parser
{
    EnumeratorOfTokens tokens;
    Stack<Environment> environments;
    string input = "";
    public List<string> Errors { get; private set; } = new List<string>();
    string pos 
    {
        get
        {
           try
           {
                return $"next to {tokens.Previous.Value} in {tokens.Previous.CodeLocation.Item1},{tokens.Previous.CodeLocation.Item2 + tokens.Previous.Value.Length}";
           }
           catch (Exception)
            {
                return $"{tokens.Current.CodeLocation.Item1},{tokens.Current.CodeLocation.Item2}";
            }
        }
    }

    public Parser(List<Token> tokens)
    {
        this.tokens = new EnumeratorOfTokens(tokens);
        this.tokens.MoveNext();
        this.Errors = new List<string>();
        this.environments = new Stack<Environment>();
        this.environments.Push(Environment.Global);
    }

    string Reset()
    {
        string temp = input;
        input = "";
        return temp;
    }


    public Record Parse()
    {
        List<IStatement> cards = new List<IStatement>();
        List<IStatement> effects = new List<IStatement>();


        while (!Consume(TokenType.Sign))
        {
            try
            {
                if (Move(TokenType.Effect) && Move(TokenType.OpenBrace))
                {
                    effects.Add(Effect());
                }

                else if (Move(TokenType.Card) && Move(TokenType.OpenBrace)) 
                {
                    cards.Add(Card());
                }

                else throw new ParsingError($"Wrong Statement" + pos);
            }
            catch(ParsingError error)
            {
                if (PanicMode(error.Message)) break;
            }
        }
        return new Record(cards, effects);
    }

     #region Utils Methods (Token)
    class EnumeratorOfTokens : IEnumerator<Token>
    {
        List<Token> tokens;
        int current = -1;

        public EnumeratorOfTokens(List<Token> tokens)
        {
            this.tokens = tokens;
        }

        public Token Current => current >= 0 ? tokens[current] : null;
        public Token Previous => current >= 1 ? tokens[current - 1] : null;
        public Token TryLookAhead => (current < -1 || current == tokens.Count - 1) ? null : tokens[current + 1];

        object System.Collections.IEnumerator.Current => this.Current;

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool MoveNext()
        {
            if (current < -1 || current == tokens.Count - 1)
            {
                current = -2;
                return false;
            }
            current++;
            return true;
        }

        public void Reset()
        {
            current = -1;
            this.MoveNext();
        }
    }

    bool Move(params TokenType[] types)
    {
        if (types.Contains(tokens.Current.Type))
        {
            input += tokens.Current.Value + " ";
            tokens.MoveNext();
            return true;
        }
        return false;
    }

    bool LookAhead(params TokenType[] types) => tokens.TryLookAhead is null? false : types.Contains(tokens.TryLookAhead.Type);

    bool Consume(params TokenType[] types) => types.Contains(tokens.Current.Type);


   bool PanicMode(string message, TokenType breaker = TokenType.CloseBrace)
   {
       Errors.Add(message);

       while (!Move(breaker))
       {
            if (Consume(TokenType.Sign, TokenType.CloseBrace))
            {
                return true;
            }
            else tokens.MoveNext();
       }
         return false;
   }

   bool Comma(TokenType breaker = TokenType.CloseBrace) => Move(TokenType.Comma) || tokens.Current.Type == breaker;

   IExpression AllocateExpr(bool condition, string message)
   {
       if(!condition) throw new ParsingError($"You are declaring a name that is in use already");

       IExpression newExpression = null;

       if (LookAhead(TokenType.DoubleDot))
       {
            newExpression = BooleanExpr();
       }
        else throw new ParsingError($"You are declaring the {message} in a wrong way");

        if (!Comma()) throw new ParsingError($"You are declaring the {message} in a wrong way");
        return newExpression;
   }

   #endregion

    #region Utils Methods (Expr)


    IExpression UnaryExpr()
    {
        while (Move(TokenType.Not, TokenType.Minus))
        {
            return new UnaryOperation(tokens.Previous, Simple());
        }

        return StringConcatenation();
    }

     IExpression BooleanExpr()
    {
        IExpression expression = Comparing();

        while (Move(TokenType.And, TokenType.Or))
        {
            expression = new BooleanExpression(expression, tokens.Previous, Comparing());
        }
        return expression;
    }


     IExpression Part()
    {
        IExpression expression = BooleanExpr();

        while (Move(TokenType.PowerTo))
        {
            expression = new MathExpression(expression, tokens.Previous, BooleanExpr());
        }

        return expression;
    }

    IExpression Term()
    {
        IExpression expression = Part();

        while (Move(TokenType.Plus, TokenType.Minus))
        {
            expression = new MathExpression(expression, tokens.Previous, Part());
        }

        return expression;
    }


      IExpression Comparing()
    {
        IExpression expression = Term();

        if (Move(TokenType.Greater, TokenType.Less, TokenType.GreaterEqual, TokenType.LessEqual, TokenType.Equal, TokenType.NotEqual))
        {
            expression = new ComparisonExpression(expression, tokens.Previous, Term());
        }

        return expression;
    }


    IExpression Simple()
    {
        IExpression expression;

        if (Move(TokenType.OpenParen))
        {
            expression = Comparing();
            if (tokens.Current.Type != TokenType.CloseParen && !(expression is Predicate))
                throw new ParsingError("Closing parenthesis is missing" + pos);
        }
        else 
        {
            if (Consume (TokenType.Sign, TokenType.SemiColon, TokenType.Comma, TokenType.CloseParen))
                throw new ParsingError("Expression is missing" + pos);

            if (Move(TokenType.Identifier))
            {
                Token variable = tokens.Previous;

                if (Move(TokenType.IncreaseOne, TokenType.DecreaseOne))
                {
                    expression = new UnaryDeclaration(new Declaration(variable, environments.Peek(), tokens.Previous));
                }

                else if (Move(TokenType.OpenBracket))
                {
                    expression = new Localizer(new UnaryValue(tokens.Current), Comparing(), tokens.Previous.CodeLocation);

                    if (!Move(TokenType.CloseBracket)) throw new ParsingError("Closing bracket is missing" + pos);
                }

                else if (LookAhead(TokenType.Lambda) && Move(TokenType.CloseParen) && Move(TokenType.Lambda))
                    return Predicate(variable);

                else expression = new UnaryDeclaration(new Declaration(variable, environments.Peek()));
            }
            else 
            {
                expression = new UnaryValue(tokens.Current);
                tokens.MoveNext();
            }

            while (Move(TokenType.Dot))
            {
                Token caller = null;

                if (Move(TokenType.Identifier)) caller = tokens.Previous;
                else throw new ParsingError("Value is missing" + pos);

                if (Move(TokenType.OpenParen))
                {
                    if (!Move(TokenType.CloseParen))
                    {
                        List<IExpression> arguments = new List<IExpression>();

                        while (Move(TokenType.Comma))
                        {
                            arguments.Add(Comparing());
                        } 

                        if (!Move(TokenType.CloseParen)) throw new ParsingError("Closing parenthesis is missing" + pos);

                        expression = new Methods(caller, expression, arguments.ToArray());
                    }

                    else expression = new Methods(caller, expression);
                }
                else expression = new Property(caller, expression);
            }
        }

        return expression;
    }


     IExpression StringConcatenation()
    {
        IExpression expression = Simple();

        if (Move(TokenType.JoinString, TokenType.SpacedString))
        {
            expression = new LiteralExpression(expression, tokens.Previous, Simple());
        }
        return expression;
    }

  
    IExpression Predicate(Token variable = null)
    {
        if(variable is null)
        {
            if (Move(TokenType.OpenParen) && Move(TokenType.Identifier))
            {
                variable = tokens.Previous;
                if (!Move(TokenType.CloseParen)) throw new ParsingError("Closing parenthesis is missing");
            }
            else throw new ParsingError("You are declaring the predicate in a wrong way");
            
            if (!Move(TokenType.Lambda)) throw new ParsingError("You are declaring the predicate in a wrong way");
        }

        environments.Push(new Environment(environments.Peek()));
        IExpression condition = Comparing();


        return new Predicate(environments.Pop(), variable, condition);
    }

    #endregion


    #region Utils Methods (Actions)

    IStatement Declaration()
    {
        IExpression variable = BooleanExpr();

        if (variable is Property property)
        {
            if (Move(TokenType.Assign, TokenType.Increase, TokenType.Decrease, TokenType.DecreaseOne, TokenType. IncreaseOne))
            {
                Token op = tokens.Previous;
                return new PropertyChanger(property, op, (op.Type is TokenType.IncreaseOne || op.Type is TokenType.DecreaseOne) ? null : BooleanExpr());
            }
            throw new ParsingError($"Incorrect: {tokens.Current.Value} in {tokens.Current.CodeLocation.Item1},{tokens.Current.CodeLocation.Item2}");
        }

        // else if (variable is Methods && Consume(TokenType.SemiColon)) 
        //     return variable;
        else if (variable is UnaryDeclaration)
        {
            Token varToken = tokens.Previous;

            if (Consume(TokenType.SemiColon))
            {
                return (new Declaration(varToken, environments.Peek()));
            }
            else if (Move(TokenType.Assign, TokenType.Increase, TokenType.Decrease))
            {
                return (new Declaration(varToken, environments.Peek(), tokens.Previous, BooleanExpr()));
            }
            throw new ParsingError($"Incorrect: {varToken.Value} in {tokens.Current.CodeLocation.Item1},{tokens.Current.CodeLocation.Item2}");

        }
        else if (variable is Localizer localizer)
        {
            if (Move(TokenType.Assign, TokenType.Increase, TokenType.Decrease, TokenType.IncreaseOne, TokenType.DecreaseOne))
            {
                Token op = tokens.Previous;
                return new LocalizerChanger(localizer, op, (op.Type is TokenType.IncreaseOne || op.Type is TokenType.DecreaseOne) ? null : BooleanExpr());
            }
            throw new ParsingError($"Incorrect: {tokens.Current.Value} in {tokens.Current.CodeLocation.Item1},{tokens.Current.CodeLocation.Item2}");
        }
        else throw new ParsingError($"Incorrect declaration in {variable.CodeLocation.Item1},{variable.CodeLocation.Item2}");
    }

    IStatement Statement()
    {
        try
        {
            if (Consume(TokenType.Sign)) 
                throw new ParsingError($"You have not finished yor statement" + pos);

            else if (Move(TokenType.If))
                return If();
            else if (Move(TokenType.While))
                return While();
            else if (Move(TokenType.For))
                return For();

            else return SimpleStatement();
        }
        catch (ParsingError e)
        {
            if (PanicMode(e.Message, TokenType.SemiColon))
                return null;
        }

        throw new ParsingError($"Incorrect statement + pos");
    }

    IStatement SimpleStatement()
    {
        IStatement stmt = null;

        if (Move(TokenType.Log)) stmt = new Log(tokens.Previous.CodeLocation, BooleanExpr());

        else if (Consume(TokenType.Identifier)) stmt = Declaration();

        else throw new ParsingError("You are declaring an empty statement" + pos);

        if (!Move(TokenType.SemiColon)) throw new ParsingError("You are declaring the statement in a wrong way" + pos);

        return stmt;

    }

     IStatement If()
    {
        IStatement stmt = null;
        (int, int) codeLocation = tokens.Current.CodeLocation;

        if (!Move(TokenType.OpenParen)) throw new ParsingError("You are declaring the if statement in a wrong way" + pos);
        IExpression condition = Comparing();
        if (!Move(TokenType.CloseParen)) throw new ParsingError("You are declaring the if statement in a wrong way" + pos);

        if (Move(TokenType.OpenBrace))
        {
            stmt = ActionBody();
        }
        else stmt = SimpleStatement();

        if(Move(TokenType.Else))
        {
            if (Move(TokenType.OpenBrace))
            {
                stmt = new If(condition, stmt, codeLocation, ActionBody());
            }
            else stmt = new If(condition, stmt, codeLocation, SimpleStatement());
        }
        else stmt = new If(condition, stmt, codeLocation);

        return stmt;
    }

    IStatement While()
    {
        (int, int) codeLocation = tokens.Current.CodeLocation;

        if (!Move(TokenType.OpenParen)) throw new ParsingError("You are declaring the while statement in a wrong way" + pos);
        IExpression condition = Comparing();
        if (!Move(TokenType.CloseParen)) throw new ParsingError("You are declaring the while statement in a wrong way" + pos);

        IStatement body = null;

        if (Move(TokenType.OpenBrace))
        {
            body = ActionBody();
        }
        else body = SimpleStatement();

        return new While(condition, body, codeLocation);
    }

    IStatement For()
    {
        Token item = null;

        if (Move(TokenType.Identifier)) item = tokens.Previous;
        else throw new ParsingError("You are declaring the for statement in a wrong way" + pos);

        if (!Move(TokenType.In))
            throw new ParsingError("You are declaring the for statement in a wrong way" + pos);
        IExpression collection = BooleanExpr();

        IStatement body = null;
        if (Move(TokenType.OpenBrace))
        {
            body = ActionBody();
        }
        else body = Statement();

        return new For(item, collection, environments.Peek(), body);
    }


    IStatement ActionBody()
    {
        List<IStatement> statements = new List<IStatement>();
        if (environments.Count > 1) environments.Push(new Environment(environments.Peek()));

        while (!Move(TokenType.CloseBrace))
        {
            statements.Add(Statement());
        } 

        Move(TokenType.SemiColon);

        if (environments.Count > 1) environments.Pop();

        return new Block(statements);
    }
   
    #endregion


    #region Card
    public IStatement Card()
    {
        (int, int) codeLocation = tokens.Previous.CodeLocation;
        IExpression name = null;
        IExpression type = null;
        List<IExpression> range = new List<IExpression>();
        IExpression faction = null;
        IExpression power = null;
        OnActivation onActivation = null;


        while (!Move(TokenType.CloseBrace))
        {
            try
            {
                if (Consume(TokenType.Sign)) throw new ParsingError($"This statement is not complete {pos} ");

                else if (Move(TokenType.Name)) name = AllocateExpr(name is null, "name");
                    
                else if (Move(TokenType.Type)) type = AllocateExpr(type is null, "type");

                else if (Move(TokenType.Faction)) faction = AllocateExpr(faction is null, "faction");

                else if (Move(TokenType.Power)) power = AllocateExpr(power is null, "power");

                else if (Move(TokenType.Range))
                {
                    if (!Move(TokenType.DoubleDot)) throw new ParsingError("You are declaring the range in a wrong way" + pos);

                    if (Move(TokenType.OpenBracket))
                    {
                        while (!Move(TokenType.CloseBracket))
                        {
                            range.Add(Comparing());
                            if (!Comma(TokenType.CloseBracket)) throw new ParsingError("You are declaring the range in a wrong way" + pos);

                        }
                    }
                    else range.Add(Comparing());

                    if (!Comma()) throw new ParsingError("You are declaring the range in a wrong way" + pos);
                }

                else if (Move(TokenType.OnActivation))
                {
                    if (!Move(TokenType.DoubleDot)) throw new ParsingError("You are declaring the OnActivation in a wrong way" + pos);

                    (int, int) location = tokens.Previous.CodeLocation;
                    List<(Activation, Activation)> effects = new List<(Activation, Activation)>();

                    if (Move(TokenType.OpenBracket))
                    {
                        while(!LookAhead(TokenType.CloseBracket))
                        {
                            effects.Add(EffectAllocation());
                            if (!Comma(TokenType.CloseBracket)) throw new ParsingError("You are declaring the OnActivation in a wrong way" + pos);

                        }
                    }
                    else effects.Add(EffectAllocation());

                    onActivation = new OnActivation( effects, location);
                    if (!Comma()) throw new ParsingError("You are declaring the onactivation in a wrong way" + pos);
                }

                else throw new ParsingError("You are declaring the card in a wrong way" + pos);
            }
            catch (ParsingError error)
            {
                if (PanicMode(error.Message, TokenType.Comma)) break;
            }
        } 

        if (name is null) throw new ParsingError($"You are declaring the card in a wrong way {codeLocation.Item1},{codeLocation.Item2}");
        if (type is null) throw new ParsingError($"You are declaring the card in a wrong way {codeLocation.Item1},{codeLocation.Item2}");
        if (range is null) throw new ParsingError($"You are declaring the card in a wrong way {codeLocation.Item1},{codeLocation.Item2}");
        if (faction is null) throw new ParsingError($"You are declaring the card in a wrong way {codeLocation.Item1},{codeLocation.Item2}");
       
        return new CardState(name, type, range, faction, power,  onActivation, codeLocation, Reset());
    }

    (Activation, Activation) EffectAllocation()
    {
        if (!Move(TokenType.OpenBrace)) throw new ParsingError("You are assigning the effect in a wrong way" + pos);

        (int, int) codeLocation = (0,0);
        (int, int) codeLocationPA = (0,0); 

        IExpression effect = null;
        IExpression effectPA = null;

        List<(Token, IExpression)> _params = new List<(Token, IExpression)>();
        List<(Token, IExpression)> _paramsPA = new List<(Token, IExpression)>();

        IExpression selector = null;
        IExpression selectorPA = null;

        if (Move(TokenType.DoubleDot))
        {
            codeLocation = tokens.Previous.CodeLocation;
            effect = BooleanExpr();
            selector = new Selector(codeLocation, new UnaryValue(new Token(TokenType.String, "board", codeLocation.Item1, codeLocation.Item2)), new UnaryValue(new Token(TokenType.True, "true", codeLocation.Item1, codeLocation.Item2)));
        }

        else while (!Move(TokenType.CloseBrace))
        {
            try
            {
                if (Consume(TokenType.Sign)) throw new ParsingError("this statement is not complete" + pos);

                else if (Move(TokenType.EffectParam))
                {
                    codeLocation = tokens.Previous.CodeLocation;
                    if (!Move(TokenType.DoubleDot)) throw new ParsingError("You are declaring the effect in a wrong way" + pos);

                    if (Move(TokenType.OpenBrace)) EffectAllocationBody(ref effect, ref _params, TokenType.Name);
                    else effect = Comparing();

                    if (!Comma()) throw new ParsingError("You are declaring the effect in a wrong way" + pos);
                }

                else if (Move(TokenType.Selector))
                {
                    if (!Move(TokenType.DoubleDot)) throw new ParsingError("You are declaring the selector in a wrong way" + pos);
                    if (!Move(TokenType.OpenBrace)) throw new ParsingError("You are declaring the selector in a wrong way" + pos);
                    selector = Selector();
                }

                else if (Move(TokenType.PostAction))
                {
                    codeLocationPA = tokens.Previous.CodeLocation;
                    if (!Move(TokenType.DoubleDot)) throw new ParsingError("You are declaring the post action in a wrong way");

                    if (Move(TokenType.OpenBrace)) selectorPA = EffectAllocationBody(ref effectPA, ref _paramsPA, TokenType.Type, selector);
                    else effect = Comparing();

                    if (!Comma()) throw new ParsingError("You are declaring the post action in a wrong way" + pos);
                }

                else throw new ParsingError("You are declaring the effect in a wrong way" + pos);
            }
            catch (ParsingError error)
            {
                if (PanicMode(error.Message, TokenType.Comma)) break;
            }
        } 

        if (effect is null) throw new ParsingError($"You are declaring the effect in a wrong way {codeLocation.Item1},{codeLocation.Item2}");
        if (selector is null) throw new ParsingError($"You are declaring the effect in a wrong way {codeLocation.Item1},{codeLocation.Item2}");
        if (codeLocationPA != (0,0) && effectPA is null) throw new ParsingError($"You are declaring the post action in a wrong way {codeLocationPA.Item1},{codeLocationPA.Item2}");

        return (new Activation( effect, selector, _params, codeLocation), 
                new Activation(effectPA, selectorPA is null? selector : selectorPA, _paramsPA, codeLocationPA));
    }

    IExpression EffectAllocationBody(ref IExpression effect, ref List<(Token, IExpression)> _params, TokenType name, IExpression parentSelector = null)
    {
        IExpression selector = null;

        while (!Move(TokenType.CloseBrace))
        {
            try
            {
                if (Consume(TokenType.Sign)) throw new ParsingError("This statement is not complete" + pos);

                else if (Move(name))
                {
                    effect = AllocateExpr(effect is null, "name");
                }

                else if (Move(TokenType.Identifier)) _params.Add((tokens.Previous, AllocateExpr(true, "parameter")));

                else if (name is TokenType.Type && Move(TokenType.Selector))
                {
                    if (!Move(TokenType.DoubleDot)) throw new ParsingError("You are declaring the selector in a wrong way" + pos);
                    if (!Move(TokenType.OpenBrace)) throw new ParsingError("You are declaring the selector in a wrong way" + pos);
                    selector = Selector(parentSelector);
                }

                else throw new ParsingError("You are declaring the card in a wrong way" + pos);

            }

            catch (ParsingError error)
            {
                if (PanicMode(error.Message, TokenType.Comma)) break;
            }
        }

        return selector;
    }

    IExpression Selector(IExpression parent = null)
    {
        (int, int) codeLocation = tokens.Previous.CodeLocation;
        IExpression source = null;
        IExpression single = null;
        IExpression predicate = null;

        while (!Move(TokenType.CloseBrace))
        {
            try
            {
                if (Consume(TokenType.Sign)) throw new ParsingError("This statement is not complete" + pos);

                else if (Move(TokenType.Source)) source = AllocateExpr(source is null, "source");

                else if (Move(TokenType.Single)) single = AllocateExpr(single is null, "single");

                else if (Move(TokenType.Predicate)) predicate = AllocateExpr(predicate is null, "predicate");

                else throw new ParsingError("You are declaring the selector in a wrong way" + pos);
            }
            catch (ParsingError error)
            {
                if (PanicMode(error.Message, TokenType.Comma)) break;
            }
        }

        if (source is null) throw new ParsingError($"You are declaring the selector in a wrong way {codeLocation.Item1},{codeLocation.Item2}");
        if (predicate is null) throw new ParsingError($"You are declaring the selector in a wrong way {codeLocation.Item1},{codeLocation.Item2}");

        return new Selector(codeLocation, source, predicate, parent, single);
    }
    #endregion




    #region Effect
    public IStatement Effect()
    {
        if (Consume(TokenType.CloseBrace)) throw new ParsingError("There's no effect here, plz look out for what you are doing");

        environments.Push(new Environment(environments.Peek()));
        IExpression name = null;
        List<(Token, Token)> paramsAndType = new List<(Token, Token)>();
        IStatement body = null;
        (int, int) codeLocation = tokens.Current.CodeLocation;
        Token targets = null;
        Token context = null;

        while (!Move(TokenType.CloseBrace))
        {
            try
            {
                if (Consume(TokenType.Sign)) throw new ParsingError($"This statement is not complete {pos}");

                else if (Move(TokenType.Name)) name = AllocateExpr(name is null, "name");

                else if (Move(TokenType.Params))
                {
                    if (Move(TokenType.DoubleDot))
                    {
                        if (Move(TokenType.OpenBrace))
                        {
                            while (Consume(TokenType.Identifier))
                            {
                                paramsAndType.Add(Param());
                                Move(TokenType.Comma);
                            }
                                if (!Move(TokenType.CloseBrace)) 
                                throw new ParsingError("You are declaring the params in a wrong way" + pos);
                        }
                        else if (Consume(TokenType.Identifier))
                        {
                            paramsAndType.Add(Param());
                        }
                        else throw new ParsingError("You are declaring the params in a wrong way" + pos);
                    }
                    else throw new ParsingError("You are declaring the params in a wrong way" + pos);

                    if (!Comma()) throw new ParsingError("You are declaring the params in a wrong way" + pos);
                }

                 else if (Move(TokenType.Action))
                {
                    if (!(body is null)) throw new ParsingError("There is already an action with this name" + pos);

                    if (Move(TokenType.DoubleDot))
                    {
                        if (Move(TokenType.OpenBrace))
                        {
                            if (Move(TokenType.Identifier)) targets = tokens.Previous;
                            else throw new ParsingError("You are declaring the action in a wrong way" + pos);

                            if (!Move(TokenType.Comma)) throw new ParsingError("You are declaring the action in a wrong way" + pos);
                            
                            if (Move(TokenType.Identifier)) context = tokens.Previous;
                            else throw new ParsingError("You are declaring the action in a wrong way" + pos);

                            if (!Move(TokenType.CloseParen)) throw new ParsingError("You are declaring the action in a wrong way" + pos);
                        }

                        if (!Move(TokenType.Lambda)) throw new ParsingError("You are declaring the action in a wrong way" + pos);

                        if (Move(TokenType.OpenBrace))
                        {
                            body = ActionBody();
                        }
                        else body = SimpleStatement();

                        if (body is null) throw new ParsingError("You are declaring the action in a wrong way" + pos);
                    }
                    else throw new ParsingError("You are declaring the action in a wrong way" + pos);

                    if (!Comma()) throw new ParsingError("You are declaring the action in a wrong way" + pos);
                }

                else throw new ParsingError("You are declaring the action in a wrong way" + pos);
            } 
            catch (ParsingError error)
            {
                if (PanicMode(error.Message, TokenType.Comma)) break;
            }
        } 

        if (name is null) throw new ParsingError($"You are declaring the effect in a wrong way {codeLocation.Item1},{codeLocation.Item2}");
        if (body is null) throw new ParsingError($"You are declaring the effect in a wrong way {codeLocation.Item1},{codeLocation.Item2}");

        return new EffectState(Reset(), name, body, paramsAndType, environments.Pop(), codeLocation, targets, context);           
    }  

    (Token, Token) Param()
    {
        Token param = null;
        Token type = null;

        if (Move(TokenType.Identifier)) param = tokens.Previous;
        else throw new ParsingError("You are declaring the params in a wrong way" + pos);

        if (!Move(TokenType.DoubleDot)) throw new ParsingError("You are declaring the params in a wrong way" + pos);

        if (Move(TokenType.Identifier)) type = tokens.Previous;
        else throw new ParsingError("You are declaring the params in a wrong way" + pos);

        return (param, type);
    }    

    #endregion
  
   
}