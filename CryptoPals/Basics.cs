using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading.Tasks;

namespace CryptoPals
{
    internal class Basics
    {
        public static byte[] HexToByteArray(string hex)
        {
            var outputLength = hex.Length / 2;
            var output = new byte[outputLength];
            using (var sr = new StringReader(hex))
            {
                for (var i = 0; i < outputLength; i++)
                    output[i] = Convert.ToByte(new string(new char[2] { (char)sr.Read(), (char)sr.Read() }), 16);
            }
            return output;
        }

        //Fixed XOR
        public static string XORString(string input, string key)
        {
            byte[] byteInput = HexToByteArray(input);
            byte[] byteKey = new byte[key.Length];
            if (key.All("0123456789abcdefABCDEF".Contains))
            {
                byteKey = HexToByteArray(key);
            } else
            {
                byteKey = Encoding.ASCII.GetBytes(key);
            }
            byte[] byteResult = new byte[byteInput.Length];

            for (int i = 0; i < byteInput.Length; i++)
            {
                byteResult[i] = (byte)(byteInput[i] ^ byteKey[i]);
            }
            return Convert.ToBase64String(byteResult);
        }


        //Single byte brute force
        public static Dictionary<char,byte[]> SingleByteBruteForce(string input)
        {
            Dictionary<char, byte[]> output = new Dictionary<char, byte[]>();
            var inputArray = HexToByteArray(input);

            for (char i = ' '; i <= '~'; i++)
            {
                output.Add(i, SingleByteXORCipher(i, inputArray));
            }

            return output;
        }

        public static byte[] SingleByteXORCipher(char key, byte[] input)
        {
            byte[] output = new byte[input.Length];
            int i = -1;

            foreach(var index in input)
            {
                output[++i] = (byte)((input[i] ^ key));
            }

            //string retString = Convert.ToBase64String(Basics.HexToByteArray(BitConverter.ToString(output).Replace("-", "")));

            return output;
        }

        public static byte[] RepeatingXORCipher(string key, string input)
        {
            var output = new byte[input.Length];
            var inputBytes = Encoding.UTF8.GetBytes(input);
            var keyBytes = Encoding.UTF8.GetBytes(key);

            for(int i = 0; i < inputBytes.Length; i++)
            {
                output[i] = (byte)(inputBytes[i] ^ keyBytes[i % key.Length]);
            }

            return output;
        }

        public static int HammingDistance(string one, string two)
        {
            if (one.Length != two.Length) { throw new ArgumentException(); }

            int counter = 0;

            for(int i=0; i < one.Length; i++)
            {
                counter += Math.Abs((byte)one[i] - (byte)two[i]);
            }

            return counter;
        }

    }
}
