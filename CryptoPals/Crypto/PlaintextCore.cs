using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoPals
{
    internal class PlaintextCore
    {
        public static string PrintByteArray(byte[] input)
        {
            var sb = new StringBuilder(input.Length * 2);
            for (var i = 0; i < input.Length; i++)
            {
                sb.AppendFormat("{0:x2}", input[i]);
            }
            return sb.ToString();
        }

        public static string PrintByteArrayToString(byte[] input)
        {
            return Encoding.Default.GetString(input);
        }

        private static readonly Dictionary<char, int> frequencies = new Dictionary<char, int>
        {
            ['e'] = 26,
            ['t'] = 25,
            ['a'] = 24,
            ['o'] = 23,
            ['i'] = 22,
            ['n'] = 21,
            ['s'] = 20,
            ['h'] = 19,
            ['r'] = 18,
            ['d'] = 17,
            ['l'] = 16,
            ['c'] = 15,
            ['u'] = 14
        };

        public static byte[] ScoreByteArray(IEnumerable<byte[]> input)
        {
            return input.Aggregate((highest, next) => ScoreEnglish(next) > ScoreEnglish(highest) ? next : highest);
        }

        public static char ScoreByteArray(Dictionary<char, byte[]> input)
        {
            int highestScore = 0;
            KeyValuePair<char, byte[]> output = new KeyValuePair<char, byte[]>();
            foreach (var pair in input)
            {
                var score = ScoreEnglish(pair.Value);
                if(score > highestScore)
                {
                    highestScore = score;
                    output = pair;
                }
            }
            return output.Key;
        }

        public static int ScoreEnglish(byte[] input)
        {
            var tempScore = 0;
            return input.Select<byte, int>(i =>
            {
                frequencies.TryGetValue((char)i, out tempScore);
                return tempScore;
            }).Sum() / input.Length;
        }
    }
}
