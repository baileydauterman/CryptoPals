using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoPals
{
    internal class PlaintextCore
    {
        public static double EnglishScore(string message)
        {
            return message.Aggregate(0.0, (current, c) =>
            {
                if (char.IsControl(c))
                {
                    return current - 5;
                }

                if (c == ' ')
                {
                    return current + 10;
                }

                if (char.IsUpper(c))
                {
                    return current + 2;
                }

                if (char.IsLetterOrDigit(c))
                {
                    return current + 5;
                }

                return current;
            });
        }

        public static double CheckStringRatio(string input)
        {
            char[] alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            double positiveMatch = 0.0;
            foreach (char c in input)
            {
                if(alpha.Contains(char.ToUpper(c)) || c == (char)32) {
                    positiveMatch++;
                }
            }
            return positiveMatch / input.Length;
        }

        private static Dictionary<char, int> frequencies = new Dictionary<char, int>
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
