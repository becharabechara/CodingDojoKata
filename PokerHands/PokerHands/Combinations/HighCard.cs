using System.Collections.Generic;

namespace PokerHands.Combinations
{
    class HighCard : Combination
    {
        public HighCard(List<Card> deck) : base(deck)
        {
            PlayerHand = "High Card : ";
            foreach (var card in deck)
            {
                PlayerHand += card.Value + ", ";
                Score += card.GetValue();
            }
            PlayerHand = PlayerHand.Remove(PlayerHand.Length - 2, 2);
        }
    }
}
