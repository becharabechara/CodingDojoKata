using System;
using System.Collections.Generic;

namespace TexasHoldem
{
    public class Card : IComparable<Card>, IComparer<Card>

    {
        private static readonly Dictionary<char, decimal> DictValue = new Dictionary<char, decimal>()
        {
            {'2', 1m},
            {'3', 2m},
            {'4', 3m},
            {'5', 5m},
            {'6', 8m},
            {'7', 13m},
            {'8', 30m},
            {'9', 58m},
            {'T', 112m},
            {'J', 218m},
            {'Q', 426m},
            {'K', 827m},
            {'A', 1613m}
        };

        public char Suit { get; set; }
        public char Value { get; set; }

        public decimal GetValue()
        {
            return DictValue[Value];
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
            DictValue.TryGetValue(card1.Value, out decimal val1);
            DictValue.TryGetValue(card2.Value, out decimal val2);
            if (val1 < val2) return -1;
            else if (val1 == val2) return 0;
            else if (val1 > val2) return 1;
            return 0;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }

        public int Compare(Card x, Card y)
        {
            return Comparison(x, y);
        }
    }
}