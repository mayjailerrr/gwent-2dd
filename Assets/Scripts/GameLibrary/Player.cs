using System.Collections.Generic;
using System;

namespace GameLibrary
{
    public class Player
    {
        public bool LeaderClicked = false;
        public bool LeaderEffectUsed = false;
        public bool RoundEnd = false;
        public bool IsPlaying = false;
        public double TotalScore = 0;
        public int Score = 0;
        Faction playerFaction;
        public List<Card> Hand = new List<Card>(10);
        private List<int> emptyCells = new List<int>(10);
        public List<Card> Deck = new List<Card>(25);
        public LeaderCard Leader;
        public Battlefield Battlefield;
        public Context context;
        private static Player? clouds;
        private static Player? reign;

        
        public static Dictionary<string, LeaderCard> Leaders = new Dictionary<string, LeaderCard>
        {
            { "Bran", (LeaderCard)AllCards.cloudsCards[0]},
            { "Mad", (LeaderCard)AllCards.cloudsCards[1]},
            { "King", (LeaderCard)AllCards.reignCards[0]},
            { "Robert", (LeaderCard)AllCards.reignCards[1]}
        };

        
        public static Player Clouds => clouds == null ? SetPlayer(ref clouds, reign, Faction.Clouds) : clouds;
        public static Player Reign => reign == null ? SetPlayer(ref reign, clouds, Faction.Reign) : reign;
        public Faction PlayerFaction => playerFaction;
        public string Name { get; }


        public Dictionary<string, List<Card>> ListByName;
        public Dictionary<Zone, List<Card>> ListByZone;
        public Dictionary<List<Card>, Zone> ZoneByList;

        
        private Player(Faction faction)
        {
            Name = faction == Faction.Clouds ? "Clouds" : "Reign";
            playerFaction = faction;
            Hand = new List<Card>(Enumerable.Repeat(Tools.MotherCard, 10));
            emptyCells.AddRange(Enumerable.Range(0, 10));
        }

        
        private static Player SetPlayer(ref Player player, Player enemy, Faction faction)
        {
            player = new Player(faction);
            player.Battlefield = new Battlefield(player);

            
            player.ListByName = new Dictionary<string, List<Card>>
            {
                { $"{player.Name} Melee", player.Battlefield.Melee },
                { $"{player.Name} Distance", player.Battlefield.Distance },
                { $"{player.Name} Siege", player.Battlefield.Siege },
                { $"{player.Name} Augment", player.Battlefield.Augment },
                { "Weather", Board.Instance.Weather },
                { $"{player.Name} Hand", player.Hand }
            };

            player.ListByZone = new Dictionary<Zone, List<Card>>
            {
                { Zone.Melee, player.Battlefield.Melee },
                { Zone.Distance, player.Battlefield.Distance },
                { Zone.Siege, player.Battlefield.Siege }
            };

            player.ZoneByList = new Dictionary<List<Card>, Zone>
            {
                { player.Battlefield.Melee, Zone.Melee },
                { player.Battlefield.Distance, Zone.Distance },
                { player.Battlefield.Siege, Zone.Siege }
            };

            
            if (enemy != null)
            {
                player.context = new Context(player, enemy);
                enemy.context = new Context(enemy, player);
            }

            return player;
        }

        
        public bool GetCard(int cardToDraw = 1)
        {
            
            if (Deck.Count <= cardToDraw)
            {
                Tools.ShuffleList(Battlefield.Graveyard);

                foreach (var card in Battlefield.Graveyard)
                {
                    card.AllocatePosition(Deck);
                    Deck.Add(card);
                }

                Battlefield.Graveyard.Clear();
            }


            while (emptyCells.Count < cardToDraw)
            {
                int index = new Random().Next(Deck.Count - 1);
                Battlefield.SendToGraveyard(Deck[index], Deck);
                cardToDraw--;
            }

        
            if (cardToDraw == 0 || emptyCells.Count < cardToDraw)
                return false;


            while (cardToDraw > 0)
            {
                int index = Deck.Count - 1;
                AllocateToHand(Deck[index]);
                Board.Instance.Take(new RemoveOperation(Deck[index], Deck, index, true));
                Deck.RemoveAt(index);
                cardToDraw--;
            }

            return true;
        }

        
        public bool PlayCard(int initialPos, int targetPos, Zone range, out bool effect)
        {
            effect = false;

            if (targetPos < 0 || initialPos < 0) return false;

            try
            {
                if (context == null)
                    context = new Context(this, Board.Instance.GetCurrentEnemy());

                if (!(Hand[initialPos] is Card card) || card.Equals(Tools.MotherCard) || card is LureCard || Board.Instance.Turn)
                    return false;

                if (card is WeatherCard weatherCard && Board.Instance.Weather[targetPos].Equals(Tools.MotherCard))
                {
                    Board.Instance.Weather[targetPos] = weatherCard;
                    weatherCard.AllocatePosition(Board.Instance.Weather);
                    Board.Instance.Take(new AddOperation(weatherCard, Board.Instance.Weather, targetPos));
                }
                else if (!Battlefield.AllocateCard(card, range, targetPos))
                    return false;

                EmptyHandAt(initialPos);
                effect = !card.Effect(context.UpdatePlayer(ListByZone[range], card));

                Board.Instance.Turn = true;
                Board.Instance.UpdateTotalScore();
                UpdateEmptySlots();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void EmptyHandAt(int index)
        {
            Board.Instance.Take(new RemoveOperation(Hand[index], Hand, index));
            emptyCells.Add(index);
            Hand[index] = Tools.MotherCard;
        }

        public void AllocateToHand(Card card)
        {
            if (emptyCells.Count == 0)
            {
                Battlefield.SendToGraveyard(card, card.ActualPosition);
                return;
            }

            Hand[emptyCells[0]] = card;
            card.AllocatePosition(Hand);
            Board.Instance.Take(new AddOperation(card, Hand, emptyCells[0]));
            emptyCells.RemoveAt(0);
        }

        public void UpdateEmptySlots()
        {
            emptyCells = new List<int>(10);
            for (int i = 0; i < Hand.Count; i++)
            {
                if (Hand[i].Equals(Tools.MotherCard))
                    emptyCells.Add(i);
            }
        }

        public static void Reset()
        {
            SetPlayer(ref clouds, reign, Faction.Clouds);
            SetPlayer(ref reign, clouds, Faction.Reign);
        }

        public override bool Equals(object other)
        {
            return other is Player another && PlayerFaction == another.PlayerFaction;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
