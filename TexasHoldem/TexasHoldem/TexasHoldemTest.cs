using System;
using System.Collections.Generic;
using Xunit;

namespace TexasHoldem
{
    public class TexasHoldemTest
    {
        [Theory]

        [InlineData("KC 9S KS KD 9D 3C 6D\n"
                    +"9C AH KS KD 9D 3C 6D\n"
                    +"AC QC KS KD 9D 3C\n"
                    +"9H 5S\n"
                    +"4D 2D KS KD 9D 3C 6D\n"
                    +"7S TS KS KD 9D\n",
                    "KC 9S KS KD 9D 3C 6D Full House : K (Winner)\n"
                    + "9C AH KS KD 9D 3C 6D Two Pairs : Pair1 of K, Pair2 of 9, A\n"
                    + "AC QC KS KD 9D 3C \n"
                    + "9H 5S\n"
                    + "4D 2D KS KD 9D 3C 6D Flush : D\n"
                    + "7S TS KS KD 9D\n")]
        public void GeneralTests(string input, string expected)
        {
            var actual = TexasHoldemManager.Compute(input);
            Assert.Equal(expected, actual);
        }

        
    }
}
