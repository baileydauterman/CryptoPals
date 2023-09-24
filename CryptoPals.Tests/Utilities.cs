using NUnit.Framework;
using System.Text;

namespace CryptoPals.Tests
{
    public class Tests
    {
        [TestCase("Exercise", "01000101 01111000 01100101 01110010 01100011 01101001 01110011 01100101")]
        [TestCase("this is a test", "01110100 01101000 01101001 01110011 00100000 01101001 01110011 00100000 01100001 00100000 01110100 01100101 01110011 01110100")]
        public void StringToBinaryConversion(string input, string expected)
        {
            Assert.AreEqual(expected, input.ToByteArray(Encoding.UTF8).ToBinary());
        }

        [Test]
        public void TestHammingBinary()
        {
            var first = "this is a test";
            var second = "wokka wokka!!!";
            Assert.AreEqual(37, first.GetEditDistance(second));
        }
    }
}