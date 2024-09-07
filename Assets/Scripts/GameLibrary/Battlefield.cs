using System.Linq;
using System.Collections.Generic;
using GameLibrary;

namespace GameLibrary
{
    public class Battlefield
    {
        public List<Card> Distance { get; private set; } = CreateDefaultZone();
        public List<Card> Siege { get; private set; } = CreateDefaultZone();
        public List<Card> Augment { get; private set; } = CreateDefaultZone();
        public List<Card>[] Zones { get; private set; }
        private List<bool> usedClearance = Enumerable.Repeat(false, 3).ToList();
        private (Card, List<Card>, int) positionInBattlefield;
        public List<bool> UsedClearance => usedClearance;
        private Player owner;
        public List<Card> Graveyard { get; private set; } = new List<Card>();
        public List<Card> Melee { get; private set; } = CreateDefaultZone();
      

        public Battlefield(Player player)
        {
            this.owner = player;
            this.Zones = new List<Card>[] { Melee, Distance, Siege };
        }

        public void SendToGraveyard(Card card, List<Card> list)
        {
            if (IsInvalidCard(card, list)) return;

            HandleSpecialCardRemovals(card, list);

            MoveCardToGraveyard(card, list);
            if (card is UnityCard unit) unit.InitPower();
        }

        public void SendToGraveyard(List<Card> list)
        {
            foreach (var card in list)
            {
                SendToGraveyard(card, list);
            }
        }

        public void SendToGraveyard(Card card)
        {
            if (card.ActualPosition == null)
            {
                SendToRelevantGraveyard(card);
            }
            else
            {
                SendToGraveyard(card, card.ActualPosition);
            }
        }

        public bool Clearance()
        {
            ClearWeather();
            ClearZones();
            SendToGraveyard(Augment);
            ResetClearanceStatus();

            return HandlePositionInBattlefield();
        }

        public bool AllocateCard(Card card, Zone range, int pos = 0) =>
            TryAllocate(card, owner.ListByZone[range], pos);

        public (UnityCard, List<Card>) HighPeekSilverCard(bool highPeekPower)
        {
            UnityCard? unit = null;
            List<Card>? list = null;

            foreach (var zone in Zones)
            {
                CompareToSearchHighPeekSilverCard(ref unit, ref list, zone, highPeekPower);
            }

            return (unit, list);
        }

        public bool RestAtBattle(string name) =>
            positionInBattlefield.Item1?.Name == name;

        public bool RestAtBattleChanger(Card card, List<Card> list)
        {
            if (card is LeaderCard) return false;

            positionInBattlefield = (card, list, list.IndexOf(card));
            return true;
        }

        public void RestAtBattleRemove() =>
            positionInBattlefield = (null, null, -1);

        public List<Card> CardsInBattle
        {
            get
            {
                var cardsInBattle = new List<Card>();

                foreach (var zone in Zones) cardsInBattle.AddRange(zone.Where(card => !card.Equals(Tools.MotherCard)));
                cardsInBattle.AddRange(Augment.Where(card => !card.Equals(Tools.MotherCard)));
                cardsInBattle.AddRange(Board.Instance.Weather
                    .Where(card => !card.Equals(Tools.MotherCard) && (card as IPlayer)?.Owner != null && ((card as IPlayer).Owner.Equals(owner))));


                return cardsInBattle;
            }
        }

        // ---- Private Helpers ----

        private static List<Card> CreateDefaultZone() =>
            Enumerable.Repeat(Tools.MotherCard, 5).ToList();

        private bool IsInvalidCard(Card card, List<Card> list) =>
            card.Equals(Tools.MotherCard) || list.Equals(Graveyard);
        
        public void RemoveClearance(int index)
        {
            if (index >= 0 && index < usedClearance.Count)
            {
                usedClearance[index] = false;
            }
        }

        private void HandleSpecialCardRemovals(Card card, List<Card> list)
        {
            if (this.Equals(owner.Hand))
            {
                owner.EmptyHandAt(list.IndexOf(card));
            }
            else if (list.Equals(owner.Deck))
            {
                owner.Deck.Remove(card);
                Board.Instance.Take(new RemoveOperation(card, list, list.Count - 1, true));
            }
            else
            {
                if (card is ClearanceCard) RemoveClearance(Tools.IndexByZone[owner.ZoneByList[list]]);
                RemoveCardFromList(card, list);
            }
        }

        private void MoveCardToGraveyard(Card card, List<Card> list)
        {
            Graveyard.Add(card);
            card.AllocatePosition(Graveyard);
            Board.Instance.Take(new AddOperation(card, Graveyard, Graveyard.Count - 1, true));
        }

        private void RemoveCardFromList(Card card, List<Card> list)
        {
            Board.Instance.Take(new RemoveOperation(card, list, list.IndexOf(card)));
            list[list.IndexOf(card)] = Tools.MotherCard;
        }

        private void SendToRelevantGraveyard(Card card)
        {
            if (Augment.Contains(card)) SendToGraveyard(card, Augment);
            else if (Board.Instance.Weather.Contains(card)) SendToGraveyard(card, Board.Instance.Weather);
            else if (owner.Hand.Contains(card)) SendToGraveyard(card, owner.Hand);
            else if (owner.Deck.Contains(card)) SendToGraveyard(card, owner.Deck);
            else
            {
                foreach (var zone in Zones)
                {
                    if (zone.Contains(card)) SendToGraveyard(card, zone);
                }
            }
        }

        private void ClearWeather()
        {
            foreach (var card in Board.Instance.Weather.ToList())
            {
                if (card.Owner.Equals(owner)) SendToGraveyard(card, Board.Instance.Weather);
            }
        }

        private void ClearZones()
        {
            foreach (var zone in Zones)
            {
                SendToGraveyard(zone);
            }
        }

        private bool Compare(double a, bool highPeek, double b) =>
            highPeek ? a > b : a < b;

        private void ResetClearanceStatus() =>
            usedClearance = Enumerable.Repeat(false, 3).ToList();

        private bool HandlePositionInBattlefield()
        {
            var (card, list, index) = positionInBattlefield;
            if (card is WeatherCard or LureCard or AugmentCard) list[index] = card;
            else if (!TryAllocate(card, list, index)) return false;

            return true;
        }

        private bool TryAllocate(Card card, List<Card> list, int index = 0)
        {
            int augmentAndClearanceIndex = Tools.IndexByZone[owner.ZoneByList[list]];

            if (card.AttackType == AttackType.Augment && Augment[augmentAndClearanceIndex].Equals(Tools.MotherCard))
            {
                Augment[augmentAndClearanceIndex] = card;
                AllocateCardToBoard(card, Augment, augmentAndClearanceIndex);
                return true;
            }

            if (list[index].Equals(Tools.MotherCard))
            {
                AllocateCardToBoard(card, list, index);
                if (card.AttackType == AttackType.Clearance) usedClearance[augmentAndClearanceIndex] = true;
                return true;
            }

            return false;
        }

        private void AllocateCardToBoard(Card card, List<Card> list, int index)
        {
            card.AllocatePosition(list);
            Board.Instance.Take(new AddOperation(card, list, index));
        }

        private void CompareToSearchHighPeekSilverCard(ref UnityCard unit, ref List<Card> list, List<Card> listToCompare, bool highPeekPower)
        {
            foreach (var card in listToCompare.OfType<UnityCard>().Where(card => card.Rank == Rank.Silver))
            {
                if (unit == null || Compare(unit.InitialPower, highPeekPower, card.InitialPower))
                {
                    unit = card;
                    list = listToCompare;
                }
            }
        }

       
    }
}
