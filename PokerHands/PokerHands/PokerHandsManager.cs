using System.Collections.Generic;
using System.Linq;
using PokerHands.Combinations;

namespace PokerHands
{
    public class PokerHandsManager
    {
        private List<Combination> listDecks = new List<Combination>();
        public static string Compute(List<string> player1, List<string> player2)
        {
            string ret = string.Empty;
            decimal point1 = EvaluateDeck(Transform(player1), out var player1Hand);
            decimal point2 = EvaluateDeck(Transform(player2), out var player2Hand);
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

        private static decimal EvaluateDeck(List<Card> deck, out string playerHand)
        {
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
    }
}