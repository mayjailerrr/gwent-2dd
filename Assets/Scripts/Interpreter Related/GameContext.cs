using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using Interpreterr;
using System;
using GameLibrary;

class GameContext : IExpression
{
    public (int, int) CodeLocation => throw new NotImplementedException();
    static GameContext context;
    public Dictionary<Faction, Player> Players;
    Board board;
    public Player TriggerPlayer => board.GetCurrentPlayer();

    GameContext()
    {
        Players = new Dictionary<Faction, Player>
        {
            { Faction.Clouds, Player.Clouds },
            { Faction.Reign, Player.Reign }
        };
        board = Player.Clouds.context.Board;
    }
   
    public static GameContext Context
    {
        get 
        {
            if (context is null) context = new GameContext();
            return context;
        }
    }

    public GameList Board
    {
        get 
        {
            List<Card> list = new List<Card>();
            foreach (var player in Players.Values)
            {
                list.AddRange(player.Battlefield.CardsInBattle);
            }
            return new GameList(list);
        }
    }

    public ExpressionType Type => ExpressionType.Context;
     public bool CheckSemantic(out List<string> errorsList)
    {
        errorsList = new List<string>();
        return true;
    }
    public bool CheckSemantic(out string error) 
    {
        error = "";
        return true;
    }
    public object Interpret() => this;
    public void RunIt() => this.Interpret();
   

    public GameList Hand(Player player) => new GameList(player.Hand, player);
    public GameList PlayerHand => Hand(TriggerPlayer);
    public GameList RivalHand => Hand(board.GetCurrentEnemy());


    public GameList Field(Player player) => new GameList(player.Battlefield.CardsInBattle, player);
    public GameList PlayerBattlefield => Field(TriggerPlayer);
    public GameList RivalBattlefield => Field(board.GetCurrentEnemy());


    public GameList Graveyard(Player player) => new GameList(player.Battlefield.Graveyard, player);
    public GameList PlayerGraveyard => Graveyard(TriggerPlayer);
    public GameList RivalGraveyard => Graveyard(board.GetCurrentEnemy());


    public GameList Deck(Player player) => new GameList(player.Deck, player);
    public GameList PlayerDeck => Deck(TriggerPlayer);
    public GameList RivalDeck => Deck(board.GetCurrentEnemy());


}