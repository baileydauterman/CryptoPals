using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CryptoPals;
using System.Threading.Tasks;

namespace CryptoPals
{
    internal class RepeatingKey
    {
        public static byte[] Encrypt(string key, string input)
        {
            var output = new byte[input.Length];
            var inputBytes = Encoding.UTF8.GetBytes(input);
            var keyBytes = Encoding.UTF8.GetBytes(key);

            for (int i = 0; i < inputBytes.Length; i++)
            {
                output[i] = (byte)(inputBytes[i] ^ keyBytes[i % key.Length]);
            }

            return output;
        }

        public static byte[] Decrypt(byte[] encryptedData)
        {
            var possibleKeys = NormalizedKeySize(encryptedData);
            var chunkedValues = ChunkData(encryptedData, possibleKeys.Last());
            var transposedBlocks = TransposeChunkData(chunkedValues);
            var encryptionKey = SingleByteXor(transposedBlocks);
            var counter = 0;

            for (int i=0; i<encryptedData.Length; i++)
            {
                encryptedData[i] = (byte)(encryptedData[i] ^ encryptionKey[i % encryptionKey.Length]);
                counter++;
                if (counter > encryptionKey.Length-1)
                {
                    counter = 0;
                }
            }
            return encryptedData;
        }

        private static int EditDistance(byte[] one, byte[] two)
        {
            if (one.Length != two.Length) 
            {
                throw new ArgumentException("arrays are not the same length"); 
            }

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

        public static int[] NormalizedKeySize(byte[] input)
        {
            var lowestKeySizes = new Queue<int>();
            var lowestNormalDistance = decimal.MaxValue;

            for (var keySize = 2; keySize <= 40; keySize++)
            {
                var calcCount = 0;
                var editDistance = 0;

                for (var i = 1; i < input.Length / keySize; i++)
                {
                    (var left, var right) = SplitArrayByKeySize(input, keySize, i);
                    editDistance += EditDistance(left, right);
                    calcCount++;
                }

                decimal normalizedEditDistance = editDistance / calcCount / keySize;
                
                if (normalizedEditDistance < lowestNormalDistance)
                {
                    lowestNormalDistance = normalizedEditDistance;

                    if(lowestKeySizes.Count > 2) 
                    {
                        lowestKeySizes.Dequeue();
                    }

                    lowestKeySizes.Enqueue(keySize);
                }
            }

            return lowestKeySizes.ToArray();
        }

        private static (byte[], byte[]) SplitArrayByKeySize(byte[] input, int keySize, int i)
        {
            var left = new ArraySegment<byte>(input, keySize * (i - 1), keySize).ToArray();
            var right = new ArraySegment<byte>(input, keySize * i, keySize).ToArray();
            return (left, right);
        }

        private static byte[][] ChunkData(byte[] encryptedData, int keySize)
        {
            byte[][] data = new byte[encryptedData.Length / keySize][];
            for(int i=0; i<encryptedData.Length; i+=keySize)
            {
                if(i + keySize < encryptedData.Length)
                {
                    data[i/keySize] = new ArraySegment<byte>(encryptedData, i, keySize).ToArray();
                }
            }

            return data;

        }

        private static byte[,] TransposeChunkData(byte[][] chunkData)
        {
            var data = new byte[chunkData[0].Length, chunkData.Length];

            for (int i = 0; i < chunkData[i].Length; i++)
            {
                for (int j = 0; j < chunkData.Length; j++)
                {
                    data[i, j] = chunkData[j][i];
                }
            }

            return data;
        }

        private static byte[] SingleByteXor(byte[,] transposedBytes)
        {
            string key = "";

            for(int i=0; i <= transposedBytes.GetLength(0) - 1; i++)
            {
                var bestScore = SingleByteKey.Decrypt(Basics.GetRow(transposedBytes, i));
                key +=  PlaintextCore.ScoreByteArray(bestScore);
            }

            return Encoding.UTF8.GetBytes(key);
        }
    }
}
