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
                    +"7S TS KS KD 9D",
                      "3C 6D 9D 9S KD KC KS Full House : K (Winner)\n"
                    + "3C 6D 9D 9C KD KS AH Two Pairs : Pair1 of K, Pair2 of 9, A\n"
                    + "AC QC KS KD 9D 3C\n"
                    + "9H 5S\n"
                    + "KS 3C 2D 4D 6D 9D KD Flush : KD\n"
                    + "7S TS KS KD 9D")]
        [InlineData(  "KC 9S KS KD 9D 3C 6D\n"
                    + "9C AH KS KD 9D 3C 6D\n"
                    + "AC QC KS KD 9D 3C\n"
                    + "KH KC KS KD 9D 3C 6D\n"
                    + "4D 2D KS KD 9D 3C 6D\n"
                    + "7S TS KS KD 9D",
                      "3C 6D 9D 9S KD KC KS Full House : K\n"
                    + "3C 6D 9D 9C KD KS AH Two Pairs : Pair1 of K, Pair2 of 9, A\n"
                    + "AC QC KS KD 9D 3C\n"
                    + "3C 6D 9D KD KC KH KS Four Of A Kind : K (Winner)\n"
                    + "KS 3C 2D 4D 6D 9D KD Flush : KD\n"
                    + "7S TS KS KD 9D")]
        public void GeneralTests(string input, string expected)
        {
            var actual = TexasHoldemManager.Compute(input);
            Assert.Equal(expected, actual);
        }

        
    }
}
