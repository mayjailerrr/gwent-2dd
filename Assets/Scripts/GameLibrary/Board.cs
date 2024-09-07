using System.Collections.Generic;
using System.Collections;
using System;


namespace GameLibrary
{
    public class Board
    {
       
        int round = 0;
        bool newRound = true;
        public List<Card> Weather = Enumerable.Repeat<Card>(Tools.MotherCard, 3).ToList<Card>();
        private static Board instance = new Board();
        public Dictionary<string, List<Card>>? ZoneList;
        Stack<Operation> operations = new Stack<Operation>();
        public bool KingPlayed { get; private set; }
        public static Board Instance => instance;
        public int Round { get => round; private set => round = value; }
        public bool Turn { get; set; }

        private Board()
        {
            KingPlayed = true;
        }

        public void SetZoneList()
        {
            this.ZoneList = new Dictionary<string, List<Card>>
            {
                {"Weather", this.Weather },

                {"Reign Augment", Player.Reign.Battlefield.Augment },
                {"Reign Melee", Player.Reign.Battlefield.Melee },
                {"Reign Distance", Player.Reign.Battlefield.Distance },
                {"Reign Siege", Player.Reign.Battlefield.Siege },

                {"Clouds Augment", Player.Clouds.Battlefield.Augment },
                {"Clouds Melee", Player.Clouds.Battlefield.Melee },
                {"Clouds.Distance", Player.Clouds.Battlefield.Distance },
                {"Clouds Siege", Player.Clouds.Battlefield.Siege },
            };
        }

        public void StartRound()
        {
            if (newRound)
            {
                if (Round == 0)
                {
                    Player.Clouds.IsPlaying = !KingPlayed;
                    Player.Reign.IsPlaying = KingPlayed;

                    Player.Clouds.GetCard(10);
                    Player.Reign.GetCard(10);
                    SetZoneList();
                }
                else
                {
                    Player.Clouds.GetCard(2);
                    Player.Reign.GetCard(2);
                    if (!Player.Clouds.Leader.Selection) 
                        Player.Clouds.LeaderEffectUsed = false;
                    if (!Player.Reign.Leader.Selection)
                        Player.Reign.LeaderEffectUsed = false;
                }
                Round++;
                newRound = false;
            }
        }

        public bool SwitchTurns(Player player = null)
        {
            if (!Turn)
                return false;
            Turn = false;

            if (!newRound)
            {
                if (KingPlayed) 
                    KingPlayed = Player.Clouds.RoundEnd;
                else KingPlayed = !Player.Reign.RoundEnd;
            }
            return true;
        }

        public bool EndRound(Player player = null)
        {
            if (player is null) player = GetCurrentPlayer();
            if (Turn)
                return false;
            Turn = true;
            player.RoundEnd = true;

            return true;
        }

        public bool CheckInitRound(out string winner)
        {
            winner = "";

            if (Player.Clouds.RoundEnd && Player.Reign.RoundEnd)
            {
                if (Player.Clouds.TotalScore > Player.Reign.TotalScore || (Player.Reign.TotalScore - Player.Clouds.TotalScore <= 3))
                {
                    AddScore(Player.Clouds, 2, Player.Reign, 0);
                    KingPlayed = false;
                    winner = "Clouds";
                }

                else if (Player.Clouds.TotalScore < Player.Reign.TotalScore)
                {
                    AddScore(Player.Reign, 2, Player.Clouds, 0);
                    KingPlayed = true;
                    winner = "Reign"; 
                }
                else 
                {
                    AddScore(Player.Clouds.IsPlaying ? Player.Reign : Player.Clouds, 1, Player.Clouds.IsPlaying ? Player.Clouds : Player.Reign, 1);
                    KingPlayed = Player.Reign.IsPlaying;
                }

                Player.Clouds.RoundEnd = false;
                Player.Reign.RoundEnd = false;
                newRound = true;

                return true;
            }
            return false;
        }

        public bool CheckEndRound(out string winner)
        {
            winner = "";

            if (Round >= 2)
            {
                if (Player.Clouds.Score >= 4 && Player.Clouds.Score > Player.Reign.Score)
                {
                    winner = "Clouds";
                    return true;
                }
                else if (Player.Reign.Score >= 4 && Player.Reign.Score > Player.Clouds.Score)
                {
                    winner = "Reign";
                    return true;
                }
            }
            return false;
        }

        public void Take(Operation operation)
        {
            operations.Push(operation);
        }

        public void UpdateTotalScore(Player? player = null)
        {
            if (player is null)
            {
                UpdateTotalScore(Player.Reign);
                player = Player.Clouds;
            }
            else 
            {
                foreach (Card card in this.Weather)
                {
                    if (card is WeatherCard weather)
                    {
                        if (weather.Owner.context is null) weather.Owner.context = new Context(weather.Owner, Tools.GetEnemyOf(weather.Owner));

                        weather.WeatherEffect(weather.Owner.context.UpdatePlayer(this.Weather, weather));
                    }
                }
            }

            player.TotalScore = 0;

            double[] augment = new double[3];
            for (int i = 0; i < augment.Length; i++)
            {
                augment[i] = (player.Battlefield.Augment[i] is AugmentCard) ? ((AugmentCard)player.Battlefield.Augment[i]).Bonus : 1; 

            }

            foreach (Card card in this.Weather)
            {
                if (card is WeatherCard weather) 
                    weather.WeatherEffect(player.context.UpdatePlayer(this.Weather, weather));
            }

            for (int i = 0; i < player.Battlefield.Zones.Length; i++)
            {
                foreach (Card card in player.Battlefield.Zones[i])
                {
                    if (card is UnityCard unit)
                    {
                        player.TotalScore += unit.Rank == Rank.Gold ? unit.Power : augment[i] * unit.Power;
                        unit.ResetPower();
                    }
                }
            }

            if (player.TotalScore < 0) player.TotalScore = 0;
        }

        public Player GetCurrentPlayer() => KingPlayed ? Player.Reign : Player.Clouds;
        public Player GetCurrentEnemy() => KingPlayed ? Player.Clouds : Player.Reign;

        private void AddScore(Player winner, int winnerScore, Player loser, int loserScore)
        {
            winner.Score += winnerScore;
            winner.IsPlaying = true;
            loser.Score += loserScore;
            loser.IsPlaying = false;
        }

        public static void Reset()
        {
            instance = new Board();
            Player.Reset();
        }

       

    }

    public abstract class Operation
    {
        protected Card card;
        protected List<Card> targets;
        protected int pos;

        public abstract void Execute();
    }

    public class AddOperation : Operation
    {
        bool valid;
        public AddOperation(Card card, List<Card> targets, int pos, bool valid = false)
        {
            this.card = card;
            this.targets = targets;
            this.pos = pos;
            this.valid = valid;
        }

        public override void Execute()
        {
            if (valid) targets.RemoveAt(pos);
            else targets[pos] = Tools.MotherCard;

            if (targets.Equals(card.Owner.Hand)) card.Owner.UpdateEmptySlots();
            else if (card is ClearanceCard clearance && !valid) clearance.Owner.Battlefield.RemoveClearance(Tools.IndexByZone[clearance.Owner.ZoneByList[targets]]);
        }
    }

    public class RemoveOperation : Operation
    {
        bool valid;
        public RemoveOperation(Card card, List<Card> targets, int pos, bool valid = false)
        {
            this.card = card;
            this.targets = targets;
            this.pos = pos;
            this.valid = valid;
        }

        public override void Execute()
        {
            if(valid) targets.Add(card);
            else targets[pos] = card;

            card.AllocatePosition(targets);
            if (targets.Equals(card.Owner.Hand)) card.Owner.UpdateEmptySlots();
            else if (card is ClearanceCard clearance) clearance.Owner.Battlefield.UsedClearance[Tools.IndexByZone[clearance.Owner.ZoneByList[targets]]] = true;
        }
    }

    public class LureOperation : Operation
    {
        Card removeCard;
        public LureOperation(LureCard card, Card removeCard, List<Card> targets, int pos)
        {
            this.card = card;
            this.targets = targets;
            this.pos = pos;
            this.removeCard = removeCard;
        }

        public override void Execute()
        {
            card.Owner.Hand[card.Owner.Hand.IndexOf(card)] = card;
            targets[pos] = removeCard;
            removeCard.AllocatePosition(targets);
            card.AllocatePosition(card.Owner.Hand);
            if (card is ClearanceCard clearance) clearance.Owner.Battlefield.UsedClearance[Tools.IndexByZone[clearance.Owner.ZoneByList[targets]]] = true;
        }
    }

    public class PowerNotification : Operation
    {
        public PowerNotification(UnityCard unit)
        {
            this.card = unit;
        }

        public override void Execute()
        {
            ((UnityCard)card).RestoreLast();
        }
    }

    public class LeaderEffectUsed : Operation
    {
        public LeaderEffectUsed(LeaderCard leader)
        {
            this.card = leader;
        }

        public override void Execute()
        {
            card.Owner.LeaderEffectUsed = false;
            if (((LeaderCard)card).Selection) card.Owner.Battlefield.RestAtBattleRemove();
        }
    }

    public class ShuffleOperation : Operation
    {
        List<Card> list;
        public ShuffleOperation(List<Card> targets, List<Card> list)
        {
            this.targets = targets;
            this.list = list;
        }

        public override void Execute()
        {
            for (int i = 0; i < list.Count; i++)
            {
                targets[i] = list[i];
            }
        }
    }

}
