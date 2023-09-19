using System.Text;

namespace CryptoPals.Crypto
{
    public class ECB
    {
        public static bool IsEcb(byte[] data, out int count)
        {
            if (data.Length < 16)
            {
                throw new InvalidDataException("Data should be larger than 16-bytes per ECB spec");
            }

            byte[] key = Encoding.UTF8.GetBytes("YELLOW SUBMARINE");
            var map = new HashSet<byte[]>();
            count = 1;
            var slices = data.SliceByteArray().ToList();

            foreach (var slice in slices)
            {
                var temp = AES.DecryptECB(slice, key);

                if (map.Contains(slice))
                {
                    count++;
                    continue;
                }

                map.Add(slice);
            }

            return count == data.Length / 16;
        }
    }
}
