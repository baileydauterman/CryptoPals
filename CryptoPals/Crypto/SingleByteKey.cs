namespace CryptoPals
{
    internal class SingleByteKey
    {
        private static byte[] Encrypt(char key, byte[] input)
        {
            byte[] output = new byte[input.Length];
            int i = -1;

            foreach (var index in input)
            {
                output[++i] = (byte)((input[i] ^ key));
            }

            return output;
        }

        public static Dictionary<char, byte[]> Decrypt(string input)
        {
            var output = new Dictionary<char, byte[]>();
            var inputArray = Basics.HexToByteArray(input);

            for (char i = ' '; i <= '~'; i++)
            {
                output.Add(i, Encrypt(i, inputArray));
            }

            return output;
        }

        public static Dictionary<char, byte[]> Decrypt(byte[] input)
        {
            var output = new Dictionary<char, byte[]>();

            for (char i = ' '; i <= '~'; i++)
            {
                output.Add(i, Encrypt(i, input));
            }

            return output;
        }
    }
}
