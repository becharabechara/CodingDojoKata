using System.Collections.Generic;
using System.Linq;
using TexasHoldem;

namespace PokerHands.Combinations
{
    class StraightFlush : Combination
    {
        public StraightFlush(List<Card> deck) : base(deck)
        {
            if (IsStraight(deck) && IsFlush(deck))
            {
                Score += deck.Max().GetValue() * 46_049_852_459_754_457_733_120_256m; // 1614^8
            }
            PlayerHand = GetCards(deck) + "Straight Flush : " + deck.Max().Value + deck[0].Suit;
        }
    }
}
