using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoPals
{
    internal class Basics
    {
        public static string ConvertHexToBase64(string hex)
        {
            return Convert.ToBase64String(HexStringToHex(hex));
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
            byte[] byteInput = ToByteArray(ConvertHexToBase64(input));
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
        public static Dictionary<char,byte[]> SingleByteBruteForce(string input)
        {
            string alphabet = "abcdefghijklmnopqrstuvwxyz";
            Dictionary<char, byte[]> output = new Dictionary<char, byte[]>();

            foreach (var letter in alphabet)
            {
                output.Add(letter, SingleByteXORCipher(letter, ToByteArray(input)));
                output.Add(Char.ToUpper(letter), SingleByteXORCipher(Char.ToUpper(letter), ToByteArray(input)));
            }

            return output;
        }

        public static byte[] SingleByteXORCipher(char key, byte[] input)
        {
            byte[] output = new byte[input.Length];
            int i = -1;

            foreach(var index in input)
            {
                output[++i] = (byte)(input[i] ^ key);
            }

            return output;
        }

    }
}
