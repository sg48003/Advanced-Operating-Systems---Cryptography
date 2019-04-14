using System.Security.Cryptography;

namespace NOS_Kriptografija
{
    class AES
    {
        public static byte[] Encrypt(byte[] textArray, byte[] keyArray, byte[] vector, EncryptionMode mode)
        {
            var tdes = new AesCryptoServiceProvider
            {
                Key = keyArray,
                IV = vector
            };
            switch (mode)
            {
                case EncryptionMode.ECB:
                    tdes.Mode = CipherMode.ECB;
                    break;
                case EncryptionMode.CBC:
                    tdes.Mode = CipherMode.CBC;
                    break;
                case EncryptionMode.CFB:
                    tdes.Mode = CipherMode.CFB;
                    break;
                //OFB javlja internal error
                case EncryptionMode.OFB:
                    tdes.Mode = CipherMode.OFB;
                    break;
                default:
                    tdes.Mode = CipherMode.ECB;
                    break;
            }

            tdes.Padding = PaddingMode.PKCS7;

            var cryptoTransform = tdes.CreateEncryptor();
            var resultArray = cryptoTransform.TransformFinalBlock(textArray, 0, textArray.Length);
            tdes.Clear();

            return resultArray;
        }

        public static byte[] Decrypt(byte[] cipherArray, byte[] keyArray, byte[] vector, EncryptionMode mode)
        {
            var tdes = new AesCryptoServiceProvider
            {
                Key = keyArray,
                IV = vector
                
            };
            switch (mode)
            {
                case EncryptionMode.ECB:
                    tdes.Mode = CipherMode.ECB;
                    break;
                case EncryptionMode.CBC:
                    tdes.Mode = CipherMode.CBC;
                    break;
                case EncryptionMode.CFB:
                    tdes.Mode = CipherMode.CFB;
                    break;
                case EncryptionMode.OFB:
                    tdes.Mode = CipherMode.OFB;
                    break;
                default:
                    tdes.Mode = CipherMode.ECB;
                    break;
            }

            tdes.Padding = PaddingMode.PKCS7;

            var cryptoTransform = tdes.CreateDecryptor();
            var resultArray = cryptoTransform.TransformFinalBlock(cipherArray, 0, cipherArray.Length);
            tdes.Clear();

            return resultArray;
        }
    }
}
