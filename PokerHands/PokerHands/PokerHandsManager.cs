using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerHands
{
    public enum PointsGained: int
    {
        StraightFlush = 1475789056, //14^8
        FourOfAKind = 105413504,
        FullHouse = 7529536,
        Flush = 537824,
        Straight =  38146,
        ThreeOfAKind = 2744,
        TwoPairs = 196,
        Pair = 14,
        HighCard = 1
    }
    public class PokerHandsManager
    {
        public static string Compute(List<string> player1, List<string> player2)
        {
            int point1 = EvaluerDeck(Transform(player1));
            int point2 = EvaluerDeck(Transform(player2));
            var ret = point1 > point2 ? "Black wins" : "White wins";
            return ret;
        }

        private static List<Card> Transform(List<string> player)
        {
            List<Card> cards = new List<Card>();
            foreach(var str in player)
            {
                Card c = new Card();
                c.Value = str[0];
                c.Suit = str[1];
                cards.Add(c);
            }
            return cards;
        }

        private static int EvaluerDeck(List<Card> deck)
        {
            int res = 0;
            int key = 0;
            List<int> list_value = new List<int>();
            List<char> list_suit = new List<char>();
            foreach(var card in deck)
            {
                list_value.Add(card.getValue());
                list_suit.Add(card.Suit);
            }
            if (IsStraight(list_value) && IsFlush(list_suit))
                res = (int)PointsGained.StraightFlush * list_value.Max();
            else if(IsFullHouse(list_value, out key))
                res = (int)PointsGained.FullHouse * key;

            return res;
        }

        private static bool IsStraight(List<int> values)
        {
            values.Sort();
            for (int i = 0; i < values.Count-1; i++)
                if (values[i] != values[i+1] - 1)
                    return false;
            return true;
        }

        private static bool IsFlush(List<char> suits) { return suits.Distinct().Count() == 1;}

        private static bool IsFullHouse(List<int> values, out int key)
        {
            key = 0;
            var distinct = values.Distinct().ToList();
            if (distinct.Count() > 2) return false;
            var a = 0;
            var b = 0;
            foreach (var v in values)
                if (v == distinct[0])
                    a++;
                else
                    b++;

            key = a == 3? a:b;

            return (a == 3 && b==2)||
                (a == 2 && b == 3);
        }

    }
}