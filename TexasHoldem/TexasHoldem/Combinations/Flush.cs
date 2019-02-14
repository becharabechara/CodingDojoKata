using System.Collections.Generic;
using System.Linq;
using TexasHoldem;

namespace PokerHands.Combinations
{
    class Flush : Combination
    {
        public Flush(List<Card> deck) : base(deck)
        {
            if (IsFlush(deck))
            {
                Score += deck.Max().GetValue() * 10_952_610_714_265_824m; // 1614^5
            }
            PlayerHand = GetCards(deck) + "Flush : " + deck.Max().Value + deck[0].Suit;
        }
    }
}
