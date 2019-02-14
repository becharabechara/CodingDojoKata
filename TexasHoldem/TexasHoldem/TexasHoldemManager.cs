using PokerHands.Combinations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace TexasHoldem
{
    public class TexasHoldemManager
    {
        private static readonly Dictionary<char, int> DictSuitPower =
            new Dictionary<char, int>()
            {
                {'D',0 },{'C',1},{'H',2 },{'S',3}
            };
        private static readonly List<int[]> ComboFiveOfSeven = new List<int[]>();

        private static List<List<Card>> Transform(string input)
        {
            var ret = new List<List<Card>>();
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
                        DictSuitPower.TryGetValue(input[j].Suit, out s1);
                        DictSuitPower.TryGetValue(input[j + 1].Suit, out s2);
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
        private static decimal EvaluateDeck(List<Card> deck, out string playerHand)
        {
            deck.Sort();
            SortBySuit(ref deck);
            playerHand = string.Empty;
            decimal res = 0;
            Combination var;
            var keys = new List<Card>();
            if (Combination.IsStraight(deck) && Combination.IsFlush(deck))
            {
                var = new StraightFlush(deck);
                playerHand = var.PlayerHand;
                res = var.Score;
            }
            else if (Combination.IsNofAKind(4, deck, out keys))
            {
                var = new FourOfAKind(deck);
                playerHand = var.PlayerHand;
                res = var.Score;
            }
            else if (Combination.IsFullHouse(deck, out Card key))
            {
                var = new FullHouse(deck);
                playerHand = var.PlayerHand;
                res = var.Score;
            }
            else if (Combination.IsFlush(deck))
            {
                var = new Flush(deck);
                playerHand = var.PlayerHand;
                res = var.Score;
            }
            else if (Combination.IsStraight(deck))
            {
                var = new Straight(deck);
                playerHand = var.PlayerHand;
                res = var.Score;
            }
            else if (Combination.IsNofAKind(3, deck, out keys))
            {
                var = new ThreeOfAKind(deck);
                playerHand = var.PlayerHand;
                res = var.Score;
            }
            else if (Combination.IsTwoPairs(deck, out keys))
            {
                var = new TwoPairs(deck);
                playerHand = var.PlayerHand;
                res = var.Score;
            }
            else if (Combination.IsNofAKind(2, deck, out keys))
            {
                var = new Pair(deck);
                playerHand = var.PlayerHand;
                res = var.Score;
            }
            else
            {
                deck.Sort();
                deck.Reverse();
                var = new HighCard(deck);
                playerHand = var.PlayerHand;
                res = var.Score;
            }
            return res;
        }

        public static string Compute(string input)
        {
            foreach (int[] c in FindCombinations(5, 7))
                ComboFiveOfSeven.Add((int[])c.Clone());

            string ret = String.Empty;
            string[] arrString = input.Split("\n");
            var listPlayers = Transform(input);
            var points = new decimal[listPlayers.Count()];
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

        private static decimal GetTheMaxPoint(List<Card> player, out string playerHand)
        {
            playerHand = String.Empty;
            string maxHand = String.Empty;
            decimal max = 0m;
            foreach (var i in ComboFiveOfSeven)
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