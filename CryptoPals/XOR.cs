namespace CryptoPals
{
    public class XOR
    {
        public XOR(string original, string key, bool isHexData = false)
        {
            if (isHexData)
            {
                Original = original.FromHex();
                Key = key.FromHex();
            }
            else
            {
                Original = original.ToByteArray();
                Key = key.ToByteArray();
            }

            Output = ComputeXOR(Original, Key);
        }

        public XOR(byte[] original, byte[] key)
        {
            Original = original;
            Key = key;
            Output = ComputeXOR(Original, Key);
        }

        public byte[] Original { get; set; }

        public byte[] Key { get; set; }

        public byte[] Output { get; }

        public static byte[] ComputeXOR(byte[] input, byte[] key)
        {
            var result = new byte[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                result[i] = (byte)(input[i] ^ key[i]);
            }

            return result;
        }
    }
}
