using System.Text;
using CryptoPals.Crypto;
using NUnit.Framework;

namespace CryptoPals.UnitTests
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
            var hex2base62 = Convert.ToBase64String(Basics.HexToByteArray(Challenge1Given));
            Assert.AreEqual(Challenge1Expected, hex2base62);
        }

        private const string Challenge1Given = "49276d206b696c6c696e6720796f757220627261696e206c696b65206120706f69736f6e6f7573206d757368726f6f6d";
        private const string Challenge1Expected = "SSdtIGtpbGxpbmcgeW91ciBicmFpbiBsaWtlIGEgcG9pc29ub3VzIG11c2hyb29t";

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
            Assert.AreEqual(Convert.ToBase64String(Basics.HexToByteArray(Challenge2Expected)),
                            Basics.XORString(Challenge2Input));
        }

        private readonly string[] Challenge2Input = new[] { "1c0111001f010100061a024b53535009181c", "686974207468652062756c6c277320657965" };
        private const string Challenge2Expected = "746865206b696420646f6e277420706c6179";

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
            Assert.AreEqual(Challenge3Expected, PlaintextCore.ScoreByteArray(SingleByteKey.Decrypt(Challenge3Input).Values));
        }

        private const string Challenge3Input = "1b37373331363f78151b7f2b783431333d78397828372d363c78373e783a393b3736";
        private const string Challenge3Expected = "Cooking MC's like a pound of bacon";

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
            var input = File.ReadLines("../../../Data/Set 1/4.txt");
            byte[][] bestScores = new byte[input.Count()][];
            int count = 0;

            foreach (var line in input)
            {
                var output = SingleByteKey.Decrypt(line);
                bestScores[count++] = PlaintextCore.ScoreByteArray(output.Values);
            }

            Assert.AreEqual("Now that the party is jumping\n", PlaintextCore.ScoreByteArray(bestScores));
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
            string input = "Burning 'em, if you ain't quick and nimble\nI go crazy when I hear a cymbal";
            string key = "ICE";
            var output = RepeatingKey.Encrypt(key, input);
            var expected = "0b3637272a2b2e63622c2e69692a23693a2a3c6324202d623d63343c2a26226324272765272" +
                "a282b2f20430a652e2c652a3124333a653e2b2027630c692b20283165286326302e27282f";
            Assert.AreEqual(expected, PlaintextCore.PrintByteArray(output));
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
        /// <br>Let KEYSIZE be the guessed length of the key; try values from 2 to(say) 40.</br>
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
            Assert.AreEqual(37, Basics.BinaryEditDistance("this is a test", "wokka wokka!!!"));
            var fileData = File.ReadAllText("../../../Data/Set 1/6.txt");
            var dat = RepeatingKey.Decrypt(Convert.FromBase64String(fileData));
            Assert.IsTrue(PlaintextCore.PrintByteArrayToString(dat).Contains("I'm back and I'm ringin' the bell"));
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
            var fileData = File.ReadAllText("../../../Data/Set 1/7.txt");
            var key = "YELLOW SUBMARINE";
            var dat = AES.DecryptECB(Convert.FromBase64String(fileData), Encoding.UTF8.GetBytes(key));
            Assert.IsTrue(dat.Contains("I'm back and I'm ringin' the bell"));
        }

        /// <summary>
        /// Set 1 Challenge 8
        /// Detect AES in ECB mode
        /// In this file are a bunch of hex-encoded ciphertexts.
        /// One of them has been encrypted with ECB.
        /// Detect it.
        /// Remember that the problem with ECB is that it is stateless and deterministic; the same 16 byte plaintext block will always produce the same 16 byte ciphertext.
        /// </summary>
        [Test]
        public void Challenge8()
        {
            //var fileData = File.ReadAllLines("../../../Data/Set 1/8.txt");
            var fileData = File.ReadAllText("../../../Data/Set 1/8.txt");
            //var output = new byte[fileData.Length][];
            var i = 0;
            //foreach(var line in fileData)
            //{
            //    output[i] = Encoding.UTF8.GetBytes(AES.DecryptECB(Basics.HexToByteArray(line), Encoding.UTF8.GetBytes("YELLOW SUBMARINE")));
            //    i++;
            //}
            //var bestScore = PlaintextCore.ScoreByteArray(output);
            //var hmmm = AES.EncryptECB(PlaintextCore.PrintByteArrayToString(bestScore), Encoding.UTF8.GetBytes("YELLOW SUBMARINE"));
            //PlaintextCore.PrintByteArrayToString(hmmm);
        }
    }
}
