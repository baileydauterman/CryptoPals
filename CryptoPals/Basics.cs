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
        public static byte[] ConvertToByteArray(string str, Encoding encoding)
        {
            return encoding.GetBytes(str);
        }

        public static String ToBinary(Byte[] data)
        {
            return string.Join(" ", data.Select(byt => Convert.ToString(byt, 2).PadLeft(8, '0')));
        }

        public static int BinaryEditDistance(string one, string two)
        {
            if(one.Length != two.Length) { throw new ArgumentException("unequal distance for hamming"); }

            string oneBin = ToBinary(ConvertToByteArray(one, Encoding.UTF8));
            string twoBin = ToBinary(ConvertToByteArray(two, Encoding.UTF8));
            int counter = 0;

            for (int i = 0; i < oneBin.Length; i++) { 
                if(oneBin[i] != twoBin[i]) {
                    counter++;
                }
            }

            return counter;
        }


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

            return output;
        }

        public static byte[] EncryptRepeatingKeyXOR(string key, string input)
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

        public static int EditDistance(string one, string two)
        {
            byte[] oneBytes = Encoding.ASCII.GetBytes(one);
            byte[] twoBytes = Encoding.ASCII.GetBytes(two);
            if (oneBytes.Length != twoBytes.Length) { throw new ArgumentException("arrays are not the same length"); }

            int score = 0;

            for(int i=0; i < oneBytes.Length; i++)
            {
                var distance = 0;
                var counter = oneBytes[i] ^ twoBytes[i];
                while (counter > 0)
                {
                    distance++;
                    counter &= counter - 1; 
                }
                score += distance;
            }
            return score;
        }

        public static int EditDistance(byte[] one, byte[] two)
        {
            if (one.Length != two.Length) { throw new ArgumentException("arrays are not the same length"); }

            int score = 0;

            for (int i = 0; i < one.Length; i++)
            {
                var distance = 0;
                var counter = one[i] ^ two[i];
                while (counter > 0)
                {
                    distance++;
                    counter &= counter - 1;
                }
                score += distance;
            }
            return score;
        }

        public static int NormalizedKeySize(byte[] input)
        {
            int lowestKeySize = 0;
            var normalEditDistance = decimal.MaxValue;

            for(var keySize=2; keySize<=40; keySize++)
            {
                var calcCount = 0;
                var editDistance = 0;

                for(var i=1; i<input.Length / keySize; i++)
                {
                    (var left, var right) = SplitArrayInHalf(input, keySize, i);
                    editDistance += EditDistance(left, right);
                    calcCount++;
                }
                var normalizedEditDistance = (decimal)editDistance / (decimal)calcCount / (decimal)keySize;
                if(normalizedEditDistance < normalEditDistance)
                {
                    normalEditDistance = normalizedEditDistance;
                    lowestKeySize = keySize;
                }
            }

            return lowestKeySize;
        }

        public static (byte[], byte[]) SplitArrayInHalf(byte[] input, int keySize, int i)
        {
            var left = new ArraySegment<byte>(input, keySize * (i - 1), keySize).ToArray();
            var right = new ArraySegment<byte>(input, keySize * i, keySize).ToArray();
            return (left, right);
        }
    }
}
