using System.Collections.Generic;
using System.Linq;

namespace PokerHands.Combinations
{
    public abstract class Combination
    {
        public List<Card> Deck { get; }
        public decimal Score { get; set; }
        public string PlayerHand { get; set; }

        protected Combination(List<Card> deck)
        {
            Deck = deck;
            Score = 0m;
            PlayerHand = string.Empty;
        }


        public static bool IsStraight(List<Card> deck)
        {
            deck.Sort();
            for (int i = 0; i < deck.Count - 1; i++)
                if (deck[i].Value != deck[i + 1].Value - 1)
                    return false;
            return true;
        }

        public static bool IsFlush(List<Card> deck)
        {
            deck.Sort();
            for (int i = 0; i < deck.Count - 1; i++)
                if (deck[i].Suit != deck[i + 1].Suit)
                    return false;
            return true;
        }

        public static bool IsTwoPairs(List<Card> deck, out List<Card> keys)
        {
            keys = new List<Card>();
            var distinct = deck.Distinct().ToList();
            if (distinct.Count() > 3) return false;
            int[] count = new int[3];
            foreach (var v in deck)
                for (var i = 0; i < 3; i++)
                    if (distinct[i] == v)
                    {
                        count[i]++;
                        if (count[i] == 2)
                            keys.Add(distinct[i]);
                    }
            keys.Sort();
            keys.Reverse();
            keys.Add(distinct[count.ToList().IndexOf(1)]);
            return count.ToList().FindAll(x => x == 2).Count() == 2;
        }

        public static bool IsNofAKind(int n, List<Card> deck, out List<Card> keys)
        {
            keys = new List<Card>();
            var distinct = deck.Distinct().ToList();
            if (distinct.Count() > (6 - n)) return false;
            int[] count = new int[6 - n];
            foreach (var v in deck)
                for (var i = 0; i < 6 - n; i++)
                    if (distinct[i] == v)
                        count[i]++;

            int maxIndex = count.ToList().IndexOf(count.Max());
            keys.Add(distinct[maxIndex]);
            foreach (var i in distinct)
                if (i != keys[0])
                    keys.Add(i);
            keys.Sort(1, 6 - n - 1, new Card());
            keys.Reverse(1, 6 - n - 1);
            return count.Max() == n;
        }

        public static bool IsFullHouse(List<Card> deck, out Card key)
        {
            key = new Card();
            var distinct = deck.Distinct().ToList();
            if (distinct.Count() > 2) return false;
            var a = 0;
            var b = 0;
            foreach (var v in deck)
                if (v == distinct[0])
                    a++;
                else
                    b++;
            key = a == 3 ? distinct[0] : distinct[1];
            return (a == 3 && b == 2) ||
                   (a == 2 && b == 3);
        }
    }
}
