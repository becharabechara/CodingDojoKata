using System;
using Xunit;

namespace Range
{
    public class RangeTest
    {
        [Theory]
        [InlineData("[2,6) contains {2,4}", "True")]
        public void ContainsTest(string input, string expected)
        {
            var res = RangeManager.Verify(input);
            Assert.Equal(expected, res);
        }
    }
}
