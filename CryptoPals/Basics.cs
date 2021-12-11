using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoPals
{
    internal class Basics
    {
        public static byte[] HexToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        //Fixed XOR
        public static string XORString(string input, string key)
        {
            byte[] byteInput = HexToByteArray(input);
            byte[] byteKey = HexToByteArray(key);
            byte[] byteResult = new byte[byteInput.Length];

            for (int i = 0; i < byteInput.Length; i++)
            {
                byteResult[i] = (byte)(byteInput[i] ^ byteKey[i]);
            }
            return Convert.ToBase64String(byteResult);
        }


        //Single byte brute force
        public static Dictionary<char,string> SingleByteBruteForce(string input)
        {
            IEnumerable<int> allCharacters = Enumerable.Range(0,256);
            Dictionary<char, string> output = new Dictionary<char, string>();

            foreach (var letter in allCharacters)
            {
                string letters = new string((char)letter, input.Length);
                output.Add((char)letter, XORString(input, letters));
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
