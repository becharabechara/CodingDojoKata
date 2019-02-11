using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Sdk;

namespace PokerHands.Combinations
{
    class Pair : Combination
    {
        public Pair(List<Card> deck) : base(deck)
        {
            PlayerHand = "Pair : ";
            if (IsNofAKind(2, deck, out var ends))
            {
                Score += GetValueFromIndex(0) * 1614m; // 1614^1
                PlayerHand += ends[0].Value + ", ";
                for (var i = 1; i < ends.Count; i++)
                {
                    PlayerHand += ends[i].Value + ", ";
                    Score += GetValueFromIndex(i);
                }
            }
            PlayerHand = PlayerHand.Remove(PlayerHand.Length - 2, 2);
            decimal GetValueFromIndex(int index) => ends[index].GetValue();
        }
    }
}
