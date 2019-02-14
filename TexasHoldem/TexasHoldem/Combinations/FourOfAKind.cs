using System;
using System.Collections.Generic;
using TexasHoldem;

namespace PokerHands.Combinations
{
    class FourOfAKind : Combination
    {
        public FourOfAKind(List<Card> deck) : base(deck)
        {
            if (IsNofAKind(4, deck, out var ends))
            {
                Score += ends[0].GetValue() * 28_531_507_100_219_614_456_704m; // 1614^7
                Score += ends[1].GetValue();
            }
            PlayerHand = GetCards(deck) + "Four Of A Kind : " + ends[0].Value;
        }
    }
}