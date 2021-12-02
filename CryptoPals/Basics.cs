using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoPals
{
    internal class Basics
    {
        public static string HexStringToB64(string hex)
        {
            return System.Convert.ToBase64String(HexStringToHex(hex));
        }

        private static byte[] HexStringToHex(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        //Fixed XOR
        public static string XORString(string input, string key)
        {
            byte[] byteInput = ToByteArray(HexStringToB64(input));
            byte[] byteKey = ToByteArray(key);
            byte[] byteResult = new byte[byteInput.Length];

            for (int i = 0; i < byteInput.Length; i++)
            {
                byteResult[i] = (byte)(byteInput[i] ^ byteKey[i]);
            }
            return BitConverter.ToString(byteResult).Replace("-", string.Empty);
        }

        private static byte[] ToByteArray(string input)
        {
            return Convert.FromBase64String(input);
        }

        //Single byte brute force
        public static string[] SingleByteBruteForce(string input)
        {
            string alphabet = "abcdefghijklmnopqrstuvwxyz";
            string decoded = HexStringToB64(input);
            string[] output = new string[alphabet.Length*2];
            var counter = 0;

            foreach (var letter in alphabet)
            {
                string key = new string(letter, decoded.Length);
                output[counter++] += XORString(input, key);

                key = new string(Char.ToUpper(letter), decoded.Length);
                output[counter++] += XORString(input, key);
            }

            return output;
        }

    }
}
