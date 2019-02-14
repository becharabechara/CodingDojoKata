﻿using System.Collections.Generic;
using TexasHoldem;

namespace PokerHands.Combinations
{
    class FullHouse : Combination
    {
        public FullHouse(List<Card> deck) : base(deck)
        {
            if (IsFullHouse(deck,out var ends))
            {
                Score += ends.GetValue() * 17_677_513_692_825_039_936m; // 1614^6
            }
            PlayerHand = GetCards(deck) + "Full House : " + ends.Value;
        }
    }
}