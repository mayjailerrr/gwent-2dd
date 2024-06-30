using UnityEngine;

[System.Flags]
public enum Faction
{
    None = 0,
    CloudOfFraternity = 1 << 0,
    ReignOfPunishment = 1 << 1,
}

[System.Flags]
public enum AttackType
{
    None = 0,
    Melee = 1 << 0,
    Ranged = 1 << 1,
    Siege = 1 << 2,
    Weather = 1 << 3,
    Lure = 1 << 4,
    Bonus = 1 << 5,

}

[System.Flags]
public enum Rank
{
    None = 0,
    Gold = 1 << 0,
    Silver = 1 << 1,
}

public interface IPlayable
{
    void Play();
}


[System.Serializable]
public class Card
{
    public string cardName;
    public int power;
    public AttackType attackType;
    public Faction faction;
    public Sprite cardImage;
    public Rank rank;
    void Swap();

    public Card(string cardName, int power, AttackType attackType, Faction faction, Sprite cardImage, Rank rank)
    {
        this.cardName = cardName;
        this.power = power;
        this.attackType = attackType;
        this.faction = faction;
        this.cardImage = cardImage;
        this.rank = rank;
    }

}

[System.Serializable]
public class MeleeCard : Card, IPlayable
{
    public override void Play()
    {
        //it rest to implement the logic of the card
    }
}

[System.Serializable]
public class RangedCard : Card, IPlayable
{ 
    public override void Play()
    {
      //it rest to implement the logic of the card
    }
}

[System.Serializable]
public class SiegeCard : Card, IPlayable
{
   public override void Play()
    {
      //it rest to implement the logic of the card
    }
}

[System.Serializable]
public class WeatherCard : Card, IPlayable
{
   public override void Play()
    {
      //it rest to implement the logic of the card
    }
}
