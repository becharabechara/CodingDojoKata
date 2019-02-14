using System.Collections.Generic;
using System.Linq;
using TexasHoldem;

namespace PokerHands.Combinations
{
    class Straight : Combination
    {
        public Straight(List<Card> deck) : base(deck)
        {
            if (IsStraight( deck))
            {
                Score += deck.Max().GetValue() * 6_786_004_160_016m; // 1614^4
            }
            PlayerHand = GetCards(deck) + "Straight : " + deck.Max().Value;
        }
    }
}
