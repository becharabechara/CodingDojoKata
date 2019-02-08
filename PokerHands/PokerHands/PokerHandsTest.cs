using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PokerHands
{
    public class PokerHandsTest
    {
        [Theory]
        [MemberData(nameof(GetData))]
        public void GeneralTests(List<string> blackPlayerList,List<string> whitePlayerList,string expectedWinnerWithDetails)
        {
            var res = PokerHandsManager.Compute(blackPlayerList, whitePlayerList);
            Assert.Equal(expectedWinnerWithDetails, res);
        }

        public static IEnumerable<object[]> GetData()
        {
            var allData = new List<object[]>
            {
                new object[] {new List<string>{ "2C", "3H", "4S", "8C", "AH"},new List<string>{ "AH", "AD", "AS", "AC", "5D" },"White wins. - with Four Of A Kind : A"},
                new object[] {new List<string>{"2H","3H","5H","4H","6H"},new List<string>{"2C","2C","2C","8H","8H"},"Black wins. - with Straight Flush : 6H"},
                new object[] {new List<string>{"2H","3H","5H","4H","6D"},new List<string>{"2C","2C","2C","8H","8H"},"White wins. - with Full House : 2"},
                new object[] {new List<string>{"2H","3H","AH","4H","6H"},new List<string>{"2C","2C","5C","8H","8H"},"Black wins. - with Flush : AH"},
                new object[] {new List<string>{"7H","7H","7D","4H","6H"},new List<string>{"2C","3C","4C","5H","6H"}, "White wins. - with Straight : 6" },
                new object[] {new List<string>{"2H","3H","6D","6H","6H"},new List<string>{"2C","2C","AC","8H","8H"},"Black wins. - with Three Of A Kind : 6"},
                new object[] {new List<string>{"2H","3H","6D","6H","9H"},new List<string>{"2C","2C","AC","8H","8H"},"White wins. - with Two Pairs : Pair1 of 8, Pair2 of 2, A"},
                new object[] {new List<string>{"2H","3H","4D","6H","6H"},new List<string>{"2C","3C","5C","8H","AH"},"Black wins. - with Pair : 6, 4, 3, 2"},
                new object[] {new List<string>{"2H","3H","6D","TH","JH"},new List<string>{"2C","5C","KC","7H","8H"},"White wins. - with High Card : K, 8, 7, 5, 2"}
            };
           return allData;
        }
    }
}
