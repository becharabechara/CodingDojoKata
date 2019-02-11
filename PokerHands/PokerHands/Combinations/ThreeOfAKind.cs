using System;
using System.Collections.Generic;
using System.Text;

namespace PokerHands.Combinations
{
    class ThreeOfAKind : Combination
    {
        public ThreeOfAKind(List<Card> deck) : base(deck)
        {
            if (IsNofAKind(3, deck, out var ends))
            {
                Score += GetValueFromIndex(0) * 4_204_463_544m; // 1614^3
                for (var i = 1; i < ends.Count - 1; i++)
                    Score += GetValueFromIndex(i);
            }
            PlayerHand = "Three Of A Kind : " + ends[0].Value;
            decimal GetValueFromIndex(int index) => ends[index].GetValue();
        }
    }
}
