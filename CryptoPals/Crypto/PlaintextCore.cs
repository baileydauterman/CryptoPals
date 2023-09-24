using System.Text;

namespace CryptoPals
{
    public class PlaintextCore
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
                if (score > highestScore)
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

            foreach (var b in input)
            {
                if (_frequencies.TryGetValue(b, out var temp))
                {
                    tempScore += temp;
                }
            }
            return tempScore / input.Length;
        }

        /// <summary>
        /// ETAOIN SHRDLU
        /// </summary>
        private static readonly Dictionary<byte, int> _frequencies = new()
        {
            // e
            {101, 26},
            // t
            {116, 25},
            // a
            {97, 24},
            // o
            {111, 23},
            // i
            {105, 22},
            // n
            {110, 21},
            // s
            {115, 20},
            // h
            {104, 19},
            // r
            {114, 18},
            // d
            {100, 17},
            // l
            {108, 16},
            // c
            {99, 15},
            // u
            {117, 14 }
        };
    }
}
