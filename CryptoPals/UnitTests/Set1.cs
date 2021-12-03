﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CryptoPals.UnitTests
{
    internal class Set1
    {
        [Test]
        public void Challenge1()
        {
            string hex2base62 = Basics.ConvertHexToBase64("49276d206b696c6c696e6720796f757220627261696e206c696b65206120706f69736f6e6f7573206d757368726f6f6d");
            Assert.IsTrue(hex2base62 == "SSdtIGtpbGxpbmcgeW91ciBicmFpbiBsaWtlIGEgcG9pc29ub3VzIG11c2hyb29t");
        }

        [Test]
        public void Challenge2()
        {
            var output = Basics.XORString("1c0111001f010100061a024b53535009181c", "686974207468652062756c6c277320657965");
            var expected = "746865206b696420646f6e277420706c6179";
            Assert.AreEqual(output.ToLower(), expected);
        }
    }
}
