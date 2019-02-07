using System;
using Xunit;

namespace FooBarQix
{
    public class FooBarQixTest
    {
        [Theory]
        [InlineData(1, "1")]
        [InlineData(3, "FooFoo")]
        [InlineData(5, "BarBar")]
        [InlineData(6, "Foo")]
        [InlineData(7, "QixQix")]
        [InlineData(9, "Foo")]
        [InlineData(10, "Bar*")]
        [InlineData(13, "Foo")]
        [InlineData(15, "FooBarBar")]
        [InlineData(21, "FooQix")]
        [InlineData(33, "FooFooFoo")]
        [InlineData(51, "FooBar")]
        [InlineData(53, "BarFoo")]
        [InlineData(101, "1*1")]
        [InlineData(303, "FooFoo*Foo")]
        [InlineData(105, "FooBarQix*Bar")]
        [InlineData(10101, "FooQix**")]
        public void DigitCheck(int input, string expected)
        {
            var res = FooBarQix.Analyze(input);
            Assert.Equal(expected, res);
        }
    }
}
