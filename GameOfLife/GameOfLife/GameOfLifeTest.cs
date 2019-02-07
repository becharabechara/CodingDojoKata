using System;
using Xunit;

namespace GameOfLife
{
    public class GameOfLifeTest
    {
        [Theory]
        [InlineData(4,8,
            "10000001"+
            "01001010"+
            "00011001"+
            "10000001",
            "00000000" +
            "00001010" +
            "00011001" +
            "00000000")]
        [InlineData(4, 8,
            "11100001" +
            "11101010" +
            "00011001" +
            "10000001",
            "10100000" +
            "10001010" +
            "00011001" +
            "00000000")]
        [InlineData(4, 8,
            "11100001" +
            "11101010" +
            "00011001" +
            "10000001",
            "10110000" +
            "10001111" +
            "10111111" +
            "00000000")]
        public void DieWithTwoLiveNeighbours(int m, int n, string input, string expected)
        {
            GameOfLifeEngine.Create(m, n, input);
            var res = GameOfLifeEngine.Generate();
            Assert.Equal(expected, res);
        }
    }
}
