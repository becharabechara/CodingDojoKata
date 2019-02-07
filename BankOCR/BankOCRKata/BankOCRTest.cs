using System;
using Xunit;

namespace BankOCRKata
{
    public class BankOCRTest
    {
        [Theory]
        [InlineData(" _  _  _  _  _  _  _  _  _ " +
                    "| || || || || || || || || |" +
                    "|_||_||_||_||_||_||_||_||_|" +
                    "                           ", "000000000")]
        [InlineData("                           " +
                    "  |  |  |  |  |  |  |  |  |" +
                    "  |  |  |  |  |  |  |  |  |" +
                    "                           ", "111111111")]
        [InlineData(" _  _  _  _  _  _  _  _  _ " +
                    " _| _| _| _| _| _| _| _| _|"+
                    "|_ |_ |_ |_ |_ |_ |_ |_ |_ " +
                    "                           ", "222222222")]

        [InlineData(" _  _  _  _  _  _  _  _    " +
                    "| || || || || || || ||_   |" +
                    "|_||_||_||_||_||_||_| _|  |" +
                    "                           ", "000000051")]
        [InlineData(" _  _  _  _  _  _  _  _    " +
                    "| || || || || |  || ||_   |" +
                    "|_||_||_||_||_||_||_| _|  |" +
                    "                           ", "00000?051")]


        public void CheckDigital(string str, string expected)
        {
            var res = BankOCRTransformer.Transform(str);
            Assert.Equal(expected, res);
        }

        [Theory]
        [InlineData(" _  _  _  _  _  _  _  _    " +
                    "| || || || || || || ||_   |" +
                    "|_||_||_||_||_||_||_| _|  |" +
                    "                           ")]
        [InlineData(" _  _  _  _  _  _  _  _    " +
                    "| || || || || |  || ||_   |" +
                    "|_||_||_||_||_||_||_| _|  |" +
                    "                           ")]
        public void CheckSum(string str)
        {
            var res = BankOCRTransformer.CalculSum(str);
            Assert.Equal(0, res%11);
        }

        [Theory]
        [InlineData(" _  _  _  _  _  _  _  _    " +
                    "| || || || || || || ||_   |" +
                    "|_||_||_||_||_||_||_| _|  |" +
                    "                           ","000000051")]
        [InlineData(" _  _  _  _  _  _  _  _    " +
                    "| || || || || |  || ||_   |" +
                    "|_||_||_||_||_||_||_| _|  |" +
                    "                           ","000000051")]
        [InlineData(" _  _  _  _  _  _  _  _  _ " +
                    "|_||_||_||_||_||_||_||_||_|" +
                    "|_||_||_||_||_||_||_||_||_|" +
                    "                           ", "888888888 AMB ['888886888', '888888988', '888888880']")]
        public void CheckFile(string str, string exp)
        {
            var res = BankOCRTransformer.WriteToFile(str);
            Assert.Equal(exp, res);
        }
        
    }
}
