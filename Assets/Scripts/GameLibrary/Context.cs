using System.Collections.Generic;


namespace GameLibrary
{
    public class Context
    {
        public Player RivalPlayer { get; private set; }
        public List<Card> ActualPos { get; private set; }
        public Card ActualCard { get; private set; }
        public Board Board { get; private set; }
        public Player ActualPlayer { get; private set; }
      

        public Context(Player player, Player enemy)
        {
            ActualPos = this.Board.Weather;
            ActualCard = Tools.MotherCard;
            this.Board = Board.Instance;
            ActualPlayer = player;
            RivalPlayer = enemy;
        }

        public Context UpdatePlayer(List<Card> pos, Card card)
        {
            ActualPos = pos;
            ActualCard = card;

            return this;
        }
    }
}

