using System.Security.Cryptography;

namespace CryptoPals.Crypto
{
    internal class AES
    {
        public static byte[] EncryptECB(string data, byte[] key)
        {
            byte[] encrypted;

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = new byte[16];
                aes.Mode = CipherMode.ECB;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (var ms = new MemoryStream())
                using (var csEncrypt = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                using (var swEncrypt = new StreamWriter(csEncrypt))
                {
                    swEncrypt.Write(data);
                    encrypted = ms.ToArray();
                }
            }

            return encrypted;
        }

        public static string DecryptECB(byte[] encryptedData, byte[] key)
        {
            var plaintext = string.Empty;

            using (var aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = new byte[16];
                aes.Mode = CipherMode.ECB;
                aes.Padding = PaddingMode.None;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (var ms = new MemoryStream(encryptedData))
                using (var csDecrypt = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (var srDecrypt = new StreamReader(csDecrypt))
                {
                    plaintext = srDecrypt.ReadToEnd();
                }
            }

            return plaintext;
        }

        public static byte[] DecryptEcb(byte[] data, byte[] key)
        {
            var keyLen = 0;
            var ret = new byte[data.Length];

            for (var i = 0; i < data.Length; i++)
            {
                if (i % key.Length == 0)
                {
                    keyLen = 0;
                }

                ret[i] = (byte)(data[i] ^ key[keyLen]);

                keyLen++;
            }

            return ret;
        }
    }
}
