using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace PokerHands
{
    public enum PointsGained : long
    {
        StraightFlush = 1475789056, //14^8
        FourOfAKind = 105413504,//14^7
        FullHouse = 7529536,//14^6
        Flush = 537824,//14^5
        Straight = 38146,//14^4
        ThreeOfAKind = 2744,//14^3
        TwoPairs = 196,//14^2
        Pair = 14,//14^1
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
            Card key = new Card();
            if (IsStraight(deck) && IsFlush(deck))
            {
                playerHand = "Straight Flush : " + deck.Max().Value.ToString() + deck[0].Suit;
                res = (long)PointsGained.StraightFlush * deck.Max().getValue();
            }
            else if (IsFourOfAKind(deck, out key))
            {
                playerHand = "Four Of A Kind : " + key.Value.ToString();
                res = (long)PointsGained.FourOfAKind * key.getValue();
            }
            else if (IsFullHouse(deck, out key))
            {
                playerHand = "Full House : " + key.Value.ToString();
                res = (long)PointsGained.FullHouse * key.getValue();
            }
            else if (IsFlush(deck))
            {
                playerHand = "Flush : " + deck.Max().Value.ToString() + deck[0].Suit;
                res = (long)PointsGained.Flush * deck.Max().getValue();
            }
            else if (IsStraight(deck))
            {
                playerHand = "Straight : " + deck.Max().Value.ToString() + deck[0].Suit;
                res = (long)PointsGained.Straight * deck.Max().getValue();
            }
            else if (IsThreeOfAKind(deck,out key))
            {
                playerHand = "Three Of A Kind : " + key.Value.ToString();
                res = (long)PointsGained.ThreeOfAKind * key.getValue();
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

        private static bool IsFourOfAKind(List<Card> deck, out Card key)
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

            key = a == 4 ? distinct[0] : distinct[1];

            return (a == 4 && b == 1) || (a == 1 && b == 4);
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

        private static bool IsThreeOfAKind(List<Card> deck, out Card key)
        {
            key = new Card();
            var distinct = deck.Distinct().ToList();
            if (distinct.Count() > 3) return false;
            var a = 0;
            var b = 0;
            var c = 0;
            foreach (var v in deck)
                if (v == distinct[0])
                    a++;
                else if (v == distinct[1])
                    b++;
                else
                    c++;

            key = (a == 3 && b == 1 && c == 1) ? distinct[0]
                : (a == 1 && b == 3 && c == 1) ? distinct[1]
                : distinct[2];

            return ((a == 3 && b == 1 && c == 1) || (a == 1 && b == 3 && c == 1) || (a == 1 && b == 1 && c == 3));
        }

    }
}