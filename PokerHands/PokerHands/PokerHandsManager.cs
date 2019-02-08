using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace PokerHands
{
    public enum PointsGained : long
    {
        StraightFlush = 2562890625, //15^8
        FourOfAKind = 170859375,//15^7
        FullHouse = 11390625,//15^6
        Flush = 759375,//15^5
        Straight = 50625,//15^4
        ThreeOfAKind = 3375,//15^3
        Pair = 15,//15^1
        HighCard = 1//14^0
    }
    public class PokerHandsManager
    {
        public static string Compute(List<string> player1, List<string> player2)
        {
            var ret = string.Empty;
            var player1Hand = string.Empty;
            var player2Hand = string.Empty;
            long point1 = EvaluateDeck(Transform(player1), out player1Hand);
            long point2 = EvaluateDeck(Transform(player2), out player2Hand);
            if (point1 > point2) ret = "Black wins. - with " + player1Hand;
            else if (point1 == point2) ret = "Tie.";
            else ret = "White wins. - with " + player2Hand;
            return ret;
        }

        private static List<Card> Transform(List<string> player)
        {
            List<Card> cards = new List<Card>();
            foreach (var str in player)
            {
                Card c = new Card();
                c.Value = str[0];
                c.Suit = str[1];
                cards.Add(c);
            }
            return cards;
        }

        private static long EvaluateDeck(List<Card> deck, out string playerHand)
        {
            playerHand = string.Empty;
            long res = 0;
            Card key;
            List<Card> keys = new List<Card>();
            if (IsStraight(deck) && IsFlush(deck))
            {
                playerHand = "Straight Flush : " + deck.Max().Value + deck[0].Suit;
                res = (long)PointsGained.StraightFlush * deck.Max().getValue();
            }
            else if (IsNOfAKind(4, deck, out keys))
            {
                playerHand = "Four Of A Kind : " + keys[0].Value;
                res = (long)PointsGained.FourOfAKind * keys[0].getValue()
                    + (long)PointsGained.HighCard * keys[1].getValue();
            }
            else if (IsFullHouse(deck, out key))
            {
                playerHand = "Full House : " + key.Value;
                res = (long)PointsGained.FullHouse * key.getValue();
            }
            else if (IsFlush(deck))
            {
                playerHand = "Flush : " + deck.Max().Value + deck[0].Suit;
                res = (long)PointsGained.Flush * deck.Max().getValue();
            }
            else if (IsStraight(deck))
            {
                playerHand = "Straight : " + deck.Max().Value;
                res = (long)PointsGained.Straight * deck.Max().getValue();
            }
            else if (IsNOfAKind(3, deck, out keys))
            {
                playerHand = "Three Of A Kind : " + keys[0].Value;
                res = (long)PointsGained.ThreeOfAKind * keys[0].getValue()
                    + (long)PointsGained.HighCard * keys[1].getValue()
                    + (long)PointsGained.HighCard * keys[2].getValue();
            }
            else if (IsTwoPairs(deck, out keys))
            {
                playerHand = "Two Pairs : " + "Pair1 of " + keys[0].Value
                    + ", Pair2 of " + keys[1].Value
                    + ", " + keys[2].Value;
                res = (long)PointsGained.Pair * keys[0].getValue()
                    + (long)PointsGained.Pair * keys[1].getValue()
                    + (long)PointsGained.HighCard * keys[2].getValue();
            }
            else if (IsNOfAKind(2, deck, out keys))
            {
                playerHand = "Pair : " + keys[0].Value
                    + ", " + keys[1].Value
                    + ", " + keys[2].Value
                    + ", " + keys[3].Value;
                res = (long)PointsGained.Pair * keys[0].getValue()
                    + (long)PointsGained.HighCard * keys[1].getValue()
                    + (long)PointsGained.HighCard * keys[2].getValue()
                    + (long)PointsGained.HighCard * keys[3].getValue();
            }
            else
            {
                deck.Sort();
                deck.Reverse();
                playerHand = "High Card : " + deck[0].Value
                    + ", " + deck[1].Value
                    + ", " + deck[2].Value
                    + ", " + deck[3].Value
                    + ", " + deck[4].Value;
                res = (long)PointsGained.HighCard * deck[0].getValue()
                    + (long)PointsGained.HighCard * deck[1].getValue()
                    + (long)PointsGained.HighCard * deck[2].getValue()
                    + (long)PointsGained.HighCard * deck[3].getValue()
                    + (long)PointsGained.HighCard * deck[4].getValue();
            }
            return res;
        }

        private static bool IsStraight(List<Card> deck)
        {
            deck.Sort();
            for (int i = 0; i < deck.Count - 1; i++)
                if (deck[i].Value != deck[i + 1].Value - 1)
                    return false;
            return true;
        }

        private static bool IsFlush(List<Card> deck)
        {
            deck.Sort();
            for (int i = 0; i < deck.Count - 1; i++)
                if (deck[i].Suit != deck[i + 1].Suit)
                    return false;
            return true;
        }

        private static bool IsTwoPairs(List<Card> deck, out List<Card> keys)
        {
            int index1 = 0, index2 = 0;
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

        private static bool IsNOfAKind(int n, List<Card> deck, out List<Card> keys)
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
            keys.Sort(1,6-n-1,new Card());
            keys.Reverse(1, 6 - n - 1);
            return count.Max() == n;
        }

        private static bool IsFullHouse(List<Card> deck, out Card key)
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