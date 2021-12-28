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
        // Set 1 Challenge 1
        // Convert hex to base64
        // The string:

        // 49276d206b696c6c696e6720796f757220627261696e206c696b65206120706f69736f6e6f7573206d757368726f6f6d
        // Should produce:

        // SSdtIGtpbGxpbmcgeW91ciBicmFpbiBsaWtlIGEgcG9pc29ub3VzIG11c2hyb29t
        // So go ahead and make that happen.You'll need to use this code for the rest of the exercises.
        [Test]
        public void Challenge1()
        {
            var given = "49276d206b696c6c696e6720796f757220627261696e206c696b65206120706f69736f6e6f7573206d757368726f6f6d";
            var hex2base62 = Convert.ToBase64String(Basics.HexToByteArray(given));
            Assert.IsTrue(hex2base62 == "SSdtIGtpbGxpbmcgeW91ciBicmFpbiBsaWtlIGEgcG9pc29ub3VzIG11c2hyb29t");
        }
        
        // Set 1 Challenge 2
        // Fixed XOR
        // Write a function that takes two equal-length buffers and produces their XOR combination.

        // If your function works properly, then when you feed it the string:

        // 1c0111001f010100061a024b53535009181c
        // ...after hex decoding, and when XOR'd against:
        // 686974207468652062756c6c277320657965
        // ...should produce:
        // 746865206b696420646f6e277420706c6179
        [Test]
        public void Challenge2()
        {
            var output = Basics.XORString("1c0111001f010100061a024b53535009181c", "686974207468652062756c6c277320657965");
            var expected = "746865206b696420646f6e277420706c6179";
            Assert.AreEqual(Convert.ToBase64String(Basics.HexToByteArray(expected)), output);
        }

        // Set 1 Challenge 3
        // Single-byte XOR cipher
        // The hex encoded string:

        // 1b37373331363f78151b7f2b783431333d78397828372d363c78373e783a393b3736
        // ...has been XOR'd against a single character. Find the key, decrypt the message.

        // You can do this by hand.But don't: write code to do it for you.

        // How? Devise some method for "scoring" a piece of English plaintext.Character frequency is a good metric.Evaluate each output and choose the one with the best score.
        [Test]
        public void Challenge3()
        {
            var given = "1b37373331363f78151b7f2b783431333d78397828372d363c78373e783a393b3736";
            var output = Basics.SingleByteBruteForce(given);
            var bestScore = PlaintextCore.ScoreByteArray(output.Values);
            Assert.AreEqual("Cooking MC's like a pound of bacon", bestScore);
        }

        // Set 1 Challenge 4
        // Detect single-character XOR
        // One of the 60-character strings in this file has been encrypted by single-character XOR.
        // Find it.
        // (Your code from #3 should help.)
        [Test]
        public void Challenge4()
        {
            var input = File.ReadLines("../../../Data/Challenge4.txt");
            byte[][] bestScores = new byte[input.Count()][];
            int count = 0;

            foreach(var line in input)
            {
                var output = Basics.SingleByteBruteForce(line);
                bestScores[count++] = PlaintextCore.ScoreByteArray(output.Values);
            }

            Assert.AreEqual("Now that the party is jumping\n", PlaintextCore.ScoreByteArray(bestScores));
        }

        // Set 1 Challenge 5
        // Implement repeating-key XOR
        // Here is the opening stanza of an important work of the English language:
           
        // Burning 'em, if you ain't quick and nimble
        // I go crazy when I hear a cymbal
        // Encrypt it, under the key "ICE", using repeating-key XOR.
           
        // In repeating-key XOR, you'll sequentially apply each byte of the key; the first byte of plaintext will be XOR'd against I, the next C, the next E, then I again for the 4th byte, and so on.
           
        // It should come out to:
           
        // 0b3637272a2b2e63622c2e69692a23693a2a3c6324202d623d63343c2a26226324272765272
        // a282b2f20430a652e2c652a3124333a653e2b2027630c692b20283165286326302e27282f
        // Encrypt a bunch of stuff using your repeating-key XOR function.Encrypt your mail.Encrypt your password file.Your.sig file.Get a feel for it.I promise, we aren't wasting your time with this.
        [Test]
        public void Challenge5()
        {
            string input = "Burning 'em, if you ain't quick and nimble\nI go crazy when I hear a cymbal";
            string key = "ICE";
            var output = Basics.RepeatingXORCipher(key, input);
            var expected = "0b3637272a2b2e63622c2e69692a23693a2a3c6324202d623d63343c2a26226324272765272" +
                "a282b2f20430a652e2c652a3124333a653e2b2027630c692b20283165286326302e27282f";
            Assert.AreEqual(expected, PlaintextCore.PrintByteArray(output));
        }

        // Set 1 Challenge 6
        // Break repeating-key XOR
        // It is officially on, now.
        // This challenge isn't conceptually hard, but it involves actual error-prone coding. The other challenges in this set are there to bring you up to speed.
        // This one is there to qualify you. If you can do this one, you're probably just fine up to Set 6.
           
        // There's a file here. It's been base64'd after being encrypted with repeating-key XOR.
           
        // Decrypt it.
           
        // Here's how:
           
        // Let KEYSIZE be the guessed length of the key; try values from 2 to(say) 40.
        // Write a function to compute the edit distance/Hamming distance between two strings.The Hamming distance is just the number of differing bits.The distance between:
        // this is a test
        // and
        // wokka wokka!!!
        // is 37. Make sure your code agrees before you proceed.
        // For each KEYSIZE, take the first KEYSIZE worth of bytes, and the second KEYSIZE worth of bytes, and find the edit distance between them.Normalize this result by
        // dividing by KEYSIZE.
        // The KEYSIZE with the smallest normalized edit distance is probably the key.You could proceed perhaps with the smallest 2-3 KEYSIZE values. Or take 4 KEYSIZE blocks
        // instead of 2 and average the distances.
        // Now that you probably know the KEYSIZE: break the ciphertext into blocks of KEYSIZE length.
        // Now transpose the blocks: make a block that is the first byte of every block, and a block that is the second byte of every block, and so on.
        // Solve each block as if it was single-character XOR. You already have code to do this.
        // For each block, the single-byte XOR key that produces the best looking histogram is the repeating-key XOR key byte for that block. Put them together and you have the key.
        // This code is going to turn out to be surprisingly useful later on. Breaking repeating-key XOR ("Vigenere") statistically is obviously an academic exercise, a "Crypto 101"
        // thing.But more people "know how" to break it than can actually break it, and a similar technique breaks something much more important.
           
        // No, that's not a mistake.
        // We get more tech support questions for this challenge than any of the other ones.We promise, there aren't any blatant errors in this text. In particular: the "wokka wokka!!!"
        // edit distance really is 37.
        [Test]
        public void Challenge6()
        {
            Assert.AreEqual(37, Basics.HammingDistance("this is a test", "wokka wokka!!!"));
        }
    }
}
