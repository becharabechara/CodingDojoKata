using Xunit;

namespace Range
{
    public class RangeTest
    {
        [Theory]
        [InlineData("[2,6) contains {2,4}", true)]
        [InlineData("[2,6) doesn't contain {-1,1,6,10}", true)]
        [InlineData("[2,10) contains [3,5]", true)]
        [InlineData("[2,5) doesn't contain [7,10)", true)]
        [InlineData("[2,5) contain [3,6]", false)]
        public void ContainsTest(string input, bool expected)
        {
            var res = RangeManager.VerifyContains(input);
            Assert.Equal(expected, res);
        }
        [Theory]
        [InlineData("[2,6)","{2,3,4,5}")]
        public void GetAllPointsTest(string input, string expected)
        {
            var res = RangeManager.GetAllPoints(input);
            Assert.Equal(expected, res);
        }
        [Theory]
        [InlineData("[2,6)", "{2,5}")]
        [InlineData("(2,6)", "{3,5}")]
        public void GetEndPointsTest(string input, string expected)
        {
            var res = RangeManager.GetEndPoints(input);
            Assert.Equal(expected, res);
        }
        [Theory]
        [InlineData("[2,5) doesn’t overlap with [7,10)", true)]
        [InlineData("[2,10) overlaps with [3,5)", true)]
        public void OverlapsRangeTest(string input, bool expected)
        {
            var res = RangeManager.VerifyOverlapsRange(input);
            Assert.Equal(expected, res);
        }
        [Theory]
        [InlineData("[2,5) doesn’t overlap with [7,10)", true)]
        [InlineData("[2,10) overlaps with [3,5)", true)]
        public void EqualsTest(string input, bool expected)
        {
            var res = RangeManager.VerifyEquals(input);
            Assert.Equal(expected, res);
        }
    }
}
