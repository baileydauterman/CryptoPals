using CryptoPals.Crypto;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CryptoPals.Tests
{
    internal class Set1 : ISet
    {
        /// <summary>
        /// <br></br>
        /// <br>Set 1 Challenge 1</br>
        /// <br>Convert hex to base64</br>
        /// <br></br>
        /// <br>Input:</br>
        /// <br>49276d206b696c6c696e6720796f757220627261696e206c696b65206120706f69736f6e6f7573206d757368726f6f6d</br>
        /// <br></br>
        /// <br>Output:</br>
        /// <br>SSdtIGtpbGxpbmcgeW91ciBicmFpbiBsaWtlIGEgcG9pc29ub3VzIG11c2hyb29t</br>
        /// <br>So go ahead and make that happen.You'll need to use this code for the rest of the exercises.</br>
        /// </summary>
        [Test]
        public void Challenge1()
        {
            // SSdtIGtpbGxpbmcgeW91ciBicmFpbiBsaWtlIGEgcG9pc29ub3VzIG11c2hyb29t
            byte[] expected = new byte[]
            {
                73,39,109,32,107,105,108,108,105,110,103,32,121,
                111,117,114,32,98,114,97,105,110,32,108,105,107,
                101,32,97,32,112,111,105,115,111,110,111,117,115,
                32,109,117,115,104,114,111,111,109
            };

            var givenText = GetFileData(1);
            var hexBytes = givenText.FromHex();
            Assert.True(expected.SequenceEqual(hexBytes));
        }

        /// <summary>
        /// <br></br>
        /// <br>Set 1 Challenge 2</br>
        /// <br>Fixed XOR</br>
        /// <br>Write a function that takes two equal-length buffers and produces their XOR combination.</br>
        /// <br></br>
        /// <br>Input:</br>
        /// <br>1c0111001f010100061a024b53535009181c and 686974207468652062756c6c277320657965</br>
        /// <br></br>
        /// <br>Output:</br>
        /// <br>746865206b696420646f6e277420706c6179</br>
        /// </summary>
        [Test]
        public void Challenge2()
        {
            var expected = "746865206b696420646f6e277420706c6179".FromHex();
            var input = new XOR("1c0111001f010100061a024b53535009181c", "686974207468652062756c6c277320657965", true);
            Assert.True(expected.SequenceEqual(input.Output));
        }

        /// <summary>
        /// <br></br>
        /// <br>Set 1 Challenge 3</br>
        /// <br>Single-byte XOR cipher</br>
        /// <br>Input has been XOR'd against a single character. Find the key, decrypt the message</br>
        /// <br></br>
        /// <br>Input:</br>
        /// <br>1b37373331363f78151b7f2b783431333d78397828372d363c78373e783a393b3736</br>
        /// <br></br>
        /// <br>Output:</br>
        /// <br>Cooking MC's like a pound of bacon</br>
        /// </summary>
        [Test]
        public void Challenge3()
        {
            var input = "1b37373331363f78151b7f2b783431333d78397828372d363c78373e783a393b3736";
            var expected = "Cooking MC's like a pound of bacon";
            var output = SingleByteKey.Decrypt(input);

            Assert.AreEqual(expected, PlaintextCore.ScoreByteArray(output));
        }


        /// <summary>
        /// <br>Set 1 Challenge 4</br>
        /// <br>Detect single-character XOR</br>
        /// <br>One of the 60-character strings in this file has been encrypted by single-character XOR.</br>
        /// <br>Find it.</br>
        /// <br>(Your code from #3 should help.)</br>
        /// </summary>
        [Test]
        public void Challenge4()
        {
            var expected = "Now that the party is jumping\n";
            byte[][] bestScores = new byte[327][];
            int count = 0;

            using (var stream = GetFileStream(4))
            using (var sr = new StreamReader(stream))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine() ?? throw new ArgumentNullException();
                    var output = SingleByteKey.Decrypt(line);
                    bestScores[count++] = PlaintextCore.ScoreByteArray(output);
                }
            }

            Assert.AreEqual(expected, PlaintextCore.ScoreByteArray(bestScores));
        }

        /// <summary>
        /// <br>Set 1 Challenge 5</br>
        /// <br>Implement repeating-key XOR</br>
        /// <br>Here is the opening stanza of an important work of the English language:</br>
        /// <br></br>
        /// <br>Burning 'em, if you ain't quick and nimble</br>
        /// <br>I go crazy when I hear a cymbal</br>
        /// <br>Encrypt it, under the key "ICE", using repeating-key XOR.</br>
        /// <br></br>
        /// <br>In repeating-key XOR, you'll sequentially apply each byte of the key; the first byte of plaintext will be XOR'd against I, the next C, the next E, then I again for the 4th byte, and so on.</br>
        /// <br></br>
        /// <br>It should come out to:</br>
        /// <br></br>
        /// <br>0b3637272a2b2e63622c2e69692a23693a2a3c6324202d623d63343c2a26226324272765272</br>
        /// <br>a282b2f20430a652e2c652a3124333a653e2b2027630c692b20283165286326302e27282f</br>
        /// <br>Encrypt a bunch of stuff using your repeating-key XOR function.Encrypt your mail.Encrypt your password file.Your.sig file.Get a feel for it.I promise, we aren't wasting your time with this.</br>
        /// </summary>
        [Test]
        public void Challenge5()
        {
            var input = "Burning 'em, if you ain't quick and nimble\nI go crazy when I hear a cymbal";
            var key = "ICE";
            var expected = "0b3637272a2b2e63622c2e69692a23693a2a3c6324202d623d63343c2a26226324272765272a282b2f20430a652e2c652a3124333a653e2b2027630c692b20283165286326302e27282f";

            var repeatingXOR = new RepeatingKeyXOR(input, key);

            Assert.AreEqual(expected, PlaintextCore.PrintByteArray(repeatingXOR.Output));
        }

        /// <summary>
        /// <br>Set 1 Challenge 6</br>
        /// <br>Break repeating-key XOR</br>
        /// <br>It is officially on, now.</br>
        /// <br>This challenge isn't conceptually hard, but it involves actual error-prone coding. The other challenges in this set are there to bring you up to speed.</br>
        /// <br>This one is there to qualify you. If you can do this one, you're probably just fine up to Set 6.</br>
        /// <br></br>
        /// <br>There's a file here. It's been base64'd after being encrypted with repeating-key XOR.</br>
        /// <br></br>
        /// <br>Decrypt it.</br>
        /// <br></br>
        /// <br>Here's how:</br>
        /// <br></br>
        /// <br>Let KEYSIZE be the guessed length of the key; try values from 2 to (say) 40.</br>
        /// <br>Write a function to compute the edit distance/Hamming distance between two strings.The Hamming distance is just the number of differing bits.The distance between:</br>
        /// <br>this is a test</br>
        /// <br>and</br>
        /// <br>wokka wokka!!!</br>
        /// <br>is 37. Make sure your code agrees before you proceed.</br>
        /// <br>For each KEYSIZE, take the first KEYSIZE worth of bytes, and the second KEYSIZE worth of bytes, and find the edit distance between them. Normalize this result by</br>
        /// <br>dividing by KEYSIZE.</br>
        /// <br>The KEYSIZE with the smallest normalized edit distance is probably the key.You could proceed perhaps with the smallest 2-3 KEYSIZE values. Or take 4 KEYSIZE blocks</br>
        /// <br>instead of 2 and average the distances.</br>
        /// <br>Now that you probably know the KEYSIZE: break the ciphertext into blocks of KEYSIZE length.</br>
        /// <br>Now transpose the blocks: make a block that is the first byte of every block, and a block that is the second byte of every block, and so on.</br>
        /// <br>Solve each block as if it was single-character XOR. You already have code to do this.</br>
        /// <br>For each block, the single-byte XOR key that produces the best looking histogram is the repeating-key XOR key byte for that block. Put them together and you have the key.</br>
        /// <br>This code is going to turn out to be surprisingly useful later on. Breaking repeating-key XOR ("Vigenere") statistically is obviously an academic exercise, a "Crypto 101"</br>
        /// <br>thing.But more people "know how" to break it than can actually break it, and a similar technique breaks something much more important.</br>
        /// <br></br>
        /// <br>No, that's not a mistake.</br>
        /// <br>We get more tech support questions for this challenge than any of the other ones.We promise, there aren't any blatant errors in this text. In particular: the "wokka wokka!!!"</br>
        /// <br>edit distance really is 37.</br>
        /// </summary>
        [Test]
        public void Challenge6()
        {
            var expected = "I'm back and I'm ringin' the bell";

            using (var stream = GetFileStream(6))
            using (var sr = new StreamReader(stream))
            {
                var dat = RepeatingKeyXORDecryptor.Decrypt(Convert.FromBase64String(sr.ReadToEnd()));
                Assert.IsTrue(PlaintextCore.PrintByteArrayToString(dat).Contains(expected));
            }
        }

        /// <summary>
        /// <br>Set 1 Challenge 7</br>
        /// <br>AES in ECB mode</br>
        /// <br>The Base64-encoded content in this file has been encrypted via AES-128 in ECB mode under the key</br>
        /// <br></br>
        /// <br>"YELLOW SUBMARINE".</br>
        /// <br>(case-sensitive, without the quotes; exactly 16 characters; I like "YELLOW SUBMARINE" because it's exactly 16 bytes long, and now you do too).</br>
        /// <br></br>
        /// <br>Decrypt it.You know the key, after all.</br>
        /// <br></br>
        /// <br>Easiest way: use OpenSSL::Cipher and give it AES-128-ECB as the cipher.</br>
        /// <br></br>
        /// <br>Do this with code.</br>
        /// <br>You can obviously decrypt this using the OpenSSL command-line tool, but we're having you get ECB working in code for a reason. You'll need it a lot later on, and not just for attacking ECB.</br>
        /// </summary>
        [Test]
        public void Challenge7()
        {
            var key = "YELLOW SUBMARINE".ToByteArray();
            var expected = "I'm back and I'm ringin' the bell";

            using (var stream = File.OpenRead(FileData[7]))
            using (var sr = new StreamReader(stream))
            {
                var data = Convert.FromBase64String(sr.ReadToEnd());

                var dat = AES.ECB.DecryptEcb(data, key);
                var mine = Encoding.ASCII.GetString(dat);

                Assert.IsTrue(mine.StartsWith(expected));
            }
        }


        /// <summary>
        /// <br>Set 1 Challenge 8</br>
        /// <br>Detect AES in ECB mode</br>
        /// <br>In this file are a bunch of hex-encoded ciphertexts.</br>
        /// <br>One of them has been encrypted with ECB.</br>
        /// <br>Detect it.</br>
        /// <br>Remember that the problem with ECB is that it is stateless and deterministic; </br>
        /// <br>the same 16 byte plaintext block will always produce the same 16 byte ciphertext.</br>
        /// </summary>
        [Test]
        public void Challenge8()
        {
            var best = string.Empty;
            var higestScore = 0;

            using (var stream = File.OpenRead(FileData[8].ToString()))
            using (var sr = new StreamReader(stream))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().FromHex();

                    if (ECB.IsEcb(line, out var score))
                    {
                        if (score > higestScore)
                        {
                            higestScore = score;
                            best = line.ToString();
                        }
                    }
                }

                Assert.AreEqual(best, Challenge8Expeccted);
            }
        }

        private string GetFileData(int i)
        {
            return File.ReadAllText(FileData[i]);
        }

        private Stream GetFileStream(int i)
        {
            return File.OpenRead(FileData[i]);
        }

        private const string Challenge8Expeccted = "d880619740a8a19b7840a8a31c810a3d08649af70dc06f4fd5d2d69c744cd283e2dd052f6b641dbf9d11b0348542bb5708649af70dc06f4fd5d2d69c744cd2839475c9dfdbc1d46597949d9c7e82bf5a08649af70dc06f4fd5d2d69c744cd28397a93eab8d6aecd566489154789a6b0308649af70dc06f4fd5d2d69c744cd283d403180c98c8f6db1f2a3f9c4040deb0ab51b29933f2c123c58386b06fba186a";

        public readonly Dictionary<int, string> FileData = new()
        {
            { 1, "../../../Data/Set 1/1.txt" },
            { 4, "../../../Data/Set 1/4.txt" },
            { 6, "../../../Data/Set 1/6.txt"},
            { 7, "../../../Data/Set 1/7.txt"},
            { 8, "../../../Data/Set 1/8.txt"},
        };
    }
}
