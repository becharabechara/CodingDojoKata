using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PotterKata
{
    public class PotterKataTest
    {
        [Theory]
        [MemberData(nameof(GetData), parameters: 8)]
        public void PriceOfBook(List<int> bookNumb, Decimal price)
        {
            var res = PotterKataManager.CalculPrix(bookNumb);
            Assert.Equal(price, res);
        }

        public static IEnumerable<object[]> GetData(int numTests)
        {
            var allData = new List<object[]>
            {
                new object[] {new List<int>{0},8m},
                new object[] {new List<int> {0,0},16m},
                new object[] {new List<int> {0,1},15.2m},
                new object[] {new List<int> {0,1,2,3,4},30m},
                new object[] {new List<int> {0,0,1},23.2m},
                new object[] {new List<int> {0,1,1,2,3,4},38m},
                new object[] {new List<int> {0,0,1,1,2,2,3,4}, 51.2m },
                new object[] {new List<int> {0,0,0,0,0,1,1,1,1,1,2,2,2,2,3,3,3,3,3,4,4,4,4}, 141.2m }
            };

            return allData.Take(numTests);
        }
    }
}
