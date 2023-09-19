using System.Text;

namespace CryptoPals
{
    public class Basics
    {
        public static byte[] ConvertToByteArray(string str, Encoding encoding)
        {
            return encoding.GetBytes(str);
        }

        public static string ToBinary(byte[] data)
        {
            return string.Join(" ", data.Select(byt => Convert.ToString(byt, 2).PadLeft(8, '0')));
        }

        public static int BinaryEditDistance(string one, string two)
        {
            if (one.Length != two.Length) { throw new ArgumentException("strings are not the same length"); }

            string oneBin = ToBinary(ConvertToByteArray(one, Encoding.UTF8));
            string twoBin = ToBinary(ConvertToByteArray(two, Encoding.UTF8));
            int counter = 0;

            for (int i = 0; i < oneBin.Length; i++)
            {
                counter += oneBin[i] ^ twoBin[i];
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
                {
                    output[i] = Convert.ToByte(new string(new char[2] { (char)sr.Read(), (char)sr.Read() }), 16);
                }
            }

            return output;
        }

        //Fixed XOR
        public static string XORString(string input, string key)
        {
            byte[] byteInput = HexToByteArray(input);
            byte[] byteKey = new byte[key.Length];
            byte[] byteResult = new byte[byteInput.Length];

            if (key.All("0123456789abcdefABCDEF".Contains))
            {
                byteKey = HexToByteArray(key);
            }
            else
            {
                byteKey = ConvertToByteArray(key, Encoding.UTF8);
            }

            for (int i = 0; i < byteInput.Length; i++)
            {
                byteResult[i] = (byte)(byteInput[i] ^ byteKey[i]);
            }

            return Convert.ToBase64String(byteResult);
        }

        public static byte[] GetRow(byte[,] matrix, int rowNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(1))
                    .Select(x => matrix[rowNumber, x])
                    .ToArray();
        }
    }
}
