using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoPals;
using NUnit.Framework;

namespace CryptoPals.UnitTests
{
    internal class BasicTest
    {
        [Test]
        public static void StringToBinaryConversion()
        {
            string expected = "01000101 01111000 01100101 01110010 01100011 01101001 01110011 01100101";
            Assert.AreEqual(expected, Basics.ToBinary(Basics.ConvertToByteArray("Exercise", Encoding.UTF8)));

            expected = "01110100 01101000 01101001 01110011 00100000 01101001 01110011 00100000 01100001 00100000 01110100 01100101 01110011 01110100";
            Assert.AreEqual(expected, Basics.ToBinary(Basics.ConvertToByteArray("this is a test", Encoding.UTF8)));
        }

        [Test]
        public static void TestHammingBinary()
        {
            Assert.AreEqual(37, Basics.BinaryEditDistance("this is a test", "wokka wokka!!!"));
        }
    }
}
