using System.Text;

namespace CryptoPals
{
    public static class Extensions
    {
        public static byte[] ToByteArray(this string input)
        {
            if (input.IsASCII())
            {
                return Basics.HexToByteArray(input);
            }
            else
            {
                return Basics.ConvertToByteArray(input, Encoding.UTF8);
            }
        }

        public static bool IsASCII(this string input)
        {
            return input.All("0123456789abcdefABCDEF".Contains);
        }

        public static bool IsASCII(this byte[] input)
        {
            return input.All(_asciiBytes.Contains);
        }

        private static readonly byte[] _asciiBytes = new byte[]
        {
            48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 97, 98, 99, 100, 101, 102, 65, 66, 67, 68, 69, 70
        };
    }
}
