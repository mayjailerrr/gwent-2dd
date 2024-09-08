using System;
using System.Collections.Generic;
using System.Text;
using Interpreter;
using GameLibrary;

public class GameList : IList<Card>
{
    Player? player;
    Board? board;
    List<Card> list;

    public GameList()
    {
        list = new List<Card>();
        board = Board.Instance;
    }
    public GameList(List<Card> list, Player? player = null)
    {
        this.list = list;
        if (player is null) board = Board.Instance;
        else this.player = player;
    }

    public Card this[Number index]
    {
        get =>
            list[(int)index.Value];
        set =>
            list[(int)index.Value] = value;
    }

    public Card this[int index]
    {
        get =>
            list[index];
        set =>
            list[index] = value;
    }

    public GameList Find(Predicate<Card> predicate) => new GameList( list.FindAll(predicate), player);

    public void Push(Card item) =>
        list.Add(item);

    public Card Pop()
    {
        Card card = list[list.Count - 1];
        list.RemoveAt(list.Count - 1);
        return card;
    }

    public void SendBottom(Card item) =>
        list.Insert(0, item);

    bool ICollection<Card>.Remove(Card item)
    {
        if(this.Contains(item))
        {
            this.Remove(item);
        }
        return this.Contains(item);
    }

    Card IList<Card>.this[int index] { get => list[index]; set => list[index] = value; }
    public int Count => list.Count;
    public bool IsReadOnly => false;
    public void Add(Card item) =>  throw new NotImplementedException(); 
    public void Clear() => list.Clear();
    public bool Contains(Card item) => list.Contains(item);
    public void CopyTo(Card[] array, int arrayIndex) => list.CopyTo(array, arrayIndex);
    public int IndexOf(Card item) => list.IndexOf(item);
    public void Insert(int index, Card item) => list.Insert(index, item);
   
    public void Remove(Card item)
    {
        if (player is null)
        {
            (item.Faction == Faction.Clouds ? Player.Clouds.Battlefield : Player.Reign.Battlefield).SendToGraveyard(item);
        }
        else 
        {
            player.Battlefield.SendToGraveyard(item);
            list.Remove(item);
        }
    }
    public void RemoveAt(int index)
    {
        if (player is null)
        {
            (list[index].Faction == Faction.Clouds ? Player.Clouds.Battlefield : Player.Reign.Battlefield).SendToGraveyard(list[index]);
        }
        else 
        {
            player.Battlefield.SendToGraveyard(list[index]);
            list.RemoveAt(index);
        }
    }

    IEnumerator<Card> IEnumerable<Card>.GetEnumerator() => this.GetEnumerator(); //todo: does not implement IEnumerable.GetEnumerator()
  
    public IEnumerator<Card> GetEnumerator()
    {
        List<Card> newList = new List<Card>();
        foreach (var cards in list)
        {
            if (!cards.Equals(Tools.MotherCard)) 
            {
                newList.Add(cards);
            }
        }
        return newList.GetEnumerator();
    }
    public void Shuffle() 
    {
        Random rng = new Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            Card value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
