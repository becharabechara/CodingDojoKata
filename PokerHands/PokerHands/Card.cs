using System;
using System.Collections.Generic;

namespace PokerHands
{
    public class Card : IComparable<Card>

    {
        private static readonly Dictionary<char, int> dictValue = new Dictionary<char, int>()
    {
        {'2', 2},
        {'3', 3},
        {'4', 4},
        {'5', 5},
        {'6', 6},
        {'7', 7},
        {'8', 8},
        {'9', 9},
        {'T', 10},
        {'J', 11},
        {'Q', 12},
        {'K', 13},
        {'A', 14}
    };

        public char Suit { get; set; }
        public char Value { get; set; }

        public int getValue()
        {
            return dictValue[Value];
        }

        public override string ToString()
        {
            return string.Empty + Value + Suit;
        }

        public static bool operator <(Card card1, Card card2)
        {
            return Comparison(card1, card2) < 0;
        }

        public static bool operator >(Card card1, Card card2)
        {
            return Comparison(card1, card2) > 0;
        }

        public static bool operator ==(Card card1, Card card2)
        {
            return Comparison(card1, card2) == 0;
        }

        public static bool operator !=(Card card1, Card card2)
        {
            return Comparison(card1, card2) != 0;
        }

        public int CompareTo(Card c)
        {
            return Comparison(this, c);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Card)) return false;
            return this == (Card)obj;
        }

        public static bool operator <=(Card card1, Card card2)
        {
            return Comparison(card1, card2) <= 0;
        }

        public static bool operator >=(Card card1, Card card2)
        {
            return Comparison(card1, card2) >= 0;
        }

        public static int Comparison(Card card1, Card card2)
        {
            int val1, val2;
            dictValue.TryGetValue(card1.Value, out val1);
            dictValue.TryGetValue(card2.Value, out val2);

            if (val1 < val2) return -1;
            else if (val1 == val2) return 0;
            else if (val1 > val2) return 1;
            return 0;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }
    }
}
