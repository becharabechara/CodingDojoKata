using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace TexasHoldem
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
    public class TexasHoldemManager
    {
        private static readonly Dictionary<char, int> dictSuitPower =
            new Dictionary<char, int>()
            {
                {'D',0 },{'C',1},{'H',2 },{'S',3}
            };
        private static List<int[]> comboFiveOfSeven = new List<int[]>();

        private static List<List<Card>> Transform(string input)
        {
            List<List<Card>> ret = new List<List<Card>>();
            string[] arrString = input.Split("\n");
            foreach (var strPlayer in arrString)
            {
                List<string> player = strPlayer.Split(' ').ToList();
                ret.Add(TransformPlayer(player));
            }
            return ret;
        }

        private static List<Card> TransformPlayer(List<string> player)
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

        private static void SortBySuit(ref List<Card> input)
        {
            int s1 = 0, s2 = 0;
            Card tmp = new Card();
            for (int i = 0; i < input.Count; i++)
            {
                for (int j = 0; j < input.Count - 1; j++)
                {
                    if (input[j].Value == input[j + 1].Value)
                    {
                        dictSuitPower.TryGetValue(input[j].Suit, out s1);
                        dictSuitPower.TryGetValue(input[j + 1].Suit, out s2);
                        if(s1 > s2)
                        {
                            tmp = input[j + 1];
                            input[j + 1] = input[j];
                            input[j] = tmp;
                        }
                    }
                }
            }
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
            keys.Sort(1, 6 - n - 1, new Card());
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

        private static long EvaluateDeck(List<Card> deck, out string playerHand)
        {
            var temp = deck;
            temp.Sort();
            SortBySuit(ref temp);
            SortBySuit(ref deck);
            playerHand = string.Empty;
            foreach (var t in temp)
            {
                playerHand += t.ToString() + " ";
            }
            long res = 0;
            Card key;
            List<Card> keys = new List<Card>();
            if (IsStraight(deck) && IsFlush(deck))
            {
                playerHand += "Straight Flush : " + deck.Max().Value + deck[0].Suit;
                res = (long)PointsGained.StraightFlush * deck.Max().getValue();
            }
            else if (IsNOfAKind(4, deck, out keys))
            {
                playerHand += "Four Of A Kind : " + keys[0].Value;
                res = (long)PointsGained.FourOfAKind * keys[0].getValue()
                    + (long)PointsGained.HighCard * keys[1].getValue();
            }
            else if (IsFullHouse(deck, out key))
            {
                playerHand += "Full House : " + key.Value;
                res = (long)PointsGained.FullHouse * key.getValue();
            }
            else if (IsFlush(deck))
            {
                playerHand += "Flush : " + deck.Max().Value + deck[0].Suit;
                res = (long)PointsGained.Flush * deck.Max().getValue();
            }
            else if (IsStraight(deck))
            {
                playerHand += "Straight : " + deck.Max().Value;
                res = (long)PointsGained.Straight * deck.Max().getValue();
            }
            else if (IsNOfAKind(3, deck, out keys))
            {
                playerHand += "Three Of A Kind : " + keys[0].Value;
                res = (long)PointsGained.ThreeOfAKind * keys[0].getValue()
                    + (long)PointsGained.HighCard * keys[1].getValue()
                    + (long)PointsGained.HighCard * keys[2].getValue();
            }
            else if (IsTwoPairs(deck, out keys))
            {
                playerHand += "Two Pairs : " + "Pair1 of " + keys[0].Value
                    + ", Pair2 of " + keys[1].Value
                    + ", " + keys[2].Value;
                res = (long)PointsGained.Pair * keys[0].getValue()
                    + (long)PointsGained.Pair * keys[1].getValue()
                    + (long)PointsGained.HighCard * keys[2].getValue();
            }
            else if (IsNOfAKind(2, deck, out keys))
            {
                playerHand += "Pair : " + keys[0].Value
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
                playerHand += "High Card : " + deck[0].Value
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

        public static string Compute(string input)
        {
            foreach (int[] c in FindCombinations(5, 7))
                comboFiveOfSeven.Add((int[])c.Clone());

            string ret = String.Empty;
            string[] arrString = input.Split("\n");
            var listPlayers = Transform(input);
            var points = new long[listPlayers.Count()];
            string[] playerHands = new string[listPlayers.Count()]; ;
            for (var i = 0; i < listPlayers.Count(); i++)
            {
                var player = listPlayers[i];
                if (player.Count() == 7)
                    points[i] = GetTheMaxPoint(player, out playerHands[i]);
                else
                    playerHands[i] = arrString[i];
            }
            for (var i = 0; i < listPlayers.Count(); i++)
            {
                if (points[i] == points.Max())
                    playerHands[i] += " (Winner)";
                if (i < listPlayers.Count - 1)
                    playerHands[i] += "\n";
            }
            return String.Join(String.Empty, playerHands.ToArray());
        }

        private static long GetTheMaxPoint(List<Card> player, out string playerHand)
        {
            playerHand = String.Empty;
            string maxHand = String.Empty;
            long max = 0L;
            foreach (var i in comboFiveOfSeven)
            {
                var deck = new List<Card>();
                foreach (var index in i)
                    deck.Add(player[index]);
                var note = EvaluateDeck(deck, out playerHand);
                if (max < note)
                {
                    max = note;
                    maxHand = GetUnusedCards(deck, player) + playerHand;
                }
            }
            playerHand = maxHand;
            return max;
        }

        private static string GetUnusedCards(List<Card> combo, List<Card> player)
        {
            List<string> strCombo = combo.Select(x => x.ToString()).ToList();
            var ret = string.Empty;
            foreach (var card in player)
                if (!strCombo.Contains(card.ToString()))
                    ret += card.ToString() + " ";
            return ret;
        }

        public static IEnumerable<int[]> FindCombosRec(int[] buffer, int done, int begin, int end)
        {
            for (int i = begin; i < end; i++)
            {
                buffer[done] = i;

                if (done == buffer.Length - 1)
                    yield return buffer;
                else
                    foreach (int[] child in FindCombosRec(buffer, done + 1, i + 1, end))
                        yield return child;
            }
        }

        public static IEnumerable<int[]> FindCombinations(int m, int n)
        {
            return FindCombosRec(new int[m], 0, 0, n);
        }
    }
}