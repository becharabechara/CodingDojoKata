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
                new object[] {new List<string>{"2H","3H","5H","4H","6H"},new List<string>{"2C","2C","2C","8H","8H"},"Black wins. - with Straight Flush : 6H"}
            };
           return allData;
        }
    }
}
