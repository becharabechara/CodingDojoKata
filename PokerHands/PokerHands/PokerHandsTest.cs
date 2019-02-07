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
        public void BlackWinsHighCard(List<string> blackPlayerList,List<string> whitePlayerList,string expectedWinnerWithDetails)
        {
            var res = PokerHandsManager.Compute(blackPlayerList, whitePlayerList);
            Assert.Equal(expectedWinnerWithDetails, res);
        }
        public static IEnumerable<object[]> GetData()
        {
            var allData = new List<object[]>
            {
                new object[] {new List<string>{"2H","3D","5S","9C","KD"},new List<string>{"2C","3H","4S","8C","AH"},"White wins. - with high card: Ace"}
            };
            return allData;
        }
    }
}