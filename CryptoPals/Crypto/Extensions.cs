namespace CryptoPals.Crypto
{
    internal static class Extensions
    {
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
    }
}
