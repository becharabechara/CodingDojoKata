using System;
using System.Collections.Generic;
using System.Text;

namespace PokerHands.Combinations
{
    class TwoPairs : Combination
    {
        public TwoPairs(List<Card> deck) : base(deck)
        {
            if (IsTwoPairs(deck, out var ends))
            {
                for (var i = 0; i < 2; i++)
                    Score += GetValueFromIndex(i) * 1614m; // 1614^1
                Score += GetValueFromIndex(2);
            }
            PlayerHand =  "Two Pairs : " + "Pair1 of " + ends[0].Value
                + ", Pair2 of " + ends[1].Value
                + ", " + ends[2].Value;
            decimal GetValueFromIndex(int index) => ends[index].GetValue();
        }
    }
}
