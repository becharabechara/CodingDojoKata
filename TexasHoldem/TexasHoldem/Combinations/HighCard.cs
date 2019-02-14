using System.Collections.Generic;
using TexasHoldem;

namespace PokerHands.Combinations
{
    class HighCard : Combination
    {
        public HighCard(List<Card> deck) : base(deck)
        {
            PlayerHand = GetCards(deck) + "High Card : ";
            foreach (var card in deck)
            {
                PlayerHand += card.Value + ", ";
                Score += card.GetValue();
            }
            PlayerHand = PlayerHand.Remove(PlayerHand.Length - 2, 2);
        }
    }
}
