using System.Text;

namespace CryptoPals
{
    public static class ByteExtensions
    {
        public static string ToBinary(this byte b)
        {
            return Convert.ToString(b, 2).PadLeft(8, '0');
        }
        public static string ToBinary(this byte[] data)
        {
            return string.Join(" ", data.Select(b => b.ToBinary()));
        }

        public static int GetEditDistance(this byte[] left, byte[] right)
        {
            var leftBin = left.ToBinary();
            var rightBin = right.ToBinary();

            var counter = 0;
            for (int i = 0; i < leftBin.Length; i++)
            {
                counter += leftBin[i] ^ rightBin[i];
            }

            return counter;
        }

        public static int GetEditDistance(this Span<byte> left, Span<byte> right)
            => left.ToArray().GetEditDistance(right.ToArray());

        public static bool IsHex(this byte[] input)
        {
            return input.All(_asciiBytes.Contains);
        }

        public static IEnumerable<byte[]> SliceByteArray(this byte[] source, int count = 16, bool padToEnd = true)
        {
            for (var i = 0; i < source.Length; i += count)
            {
                yield return source.CopyArray(count, i);
            }
        }
        public static byte[] CopyArray(this byte[] source, int count, int index)
        {
            var temp = new byte[count];
            Array.Copy(source, index, temp, 0, count);
            return temp;
        }

        private static readonly byte[] _asciiBytes = new byte[]
        {
            48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 97, 98, 99, 100, 101, 102, 65, 66, 67, 68, 69, 70
        };
    }

    public static class StringExtensions
    {
        public static byte[] ToByteArray(this string str, Encoding encoding = null)
        {
            return encoding is not null ? encoding.GetBytes(str) : Encoding.UTF8.GetBytes(str);
        }

        public static byte[] FromHex(this string str)
        {
            var charBuffer = new char[2];
            var outputLength = str.Length / 2;
            var output = new byte[outputLength];

            using (var sr = new StringReader(str))
            {
                for (var i = 0; i < outputLength; i++)
                {
                    sr.Read(charBuffer);
                    output[i] = Convert.ToByte(new string(charBuffer), 16);
                }
            }

            return output;
        }

        public static int GetEditDistance(this string input, string other)
        {
            if (input.Length != other.Length)
            {
                throw new ArgumentException("strings are not the same length");
            }

            return input.ToByteArray().GetEditDistance(other.ToByteArray());
        }

        public static bool IsHex(this string input)
        {
            return input.All("0123456789abcdefABCDEF".Contains);
        }
    }
}
