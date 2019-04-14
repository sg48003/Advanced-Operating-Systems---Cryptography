using System;
using System.Security.Cryptography;
using System.Text;

namespace NOS_Kriptografija
{
    public static class HelperFunctions
    {
        public static byte[] FromHexToByte(string hexstring)
        {
            var text = new byte[hexstring.Length / 2];

            for (var i = 0; i < hexstring.Length / 2; i++)
            {
                var word = hexstring.Substring(i * 2, 2);
                text[i] = (byte)Convert.ToInt16(word, 16);
            }

            return text;
        }

        public static string FromByteToHex(byte[] input)
        {
            var stringBuilder = new StringBuilder(input.Length * 2);

            foreach (var b in input)
            {
                if (b < 0x10)
                {
                    stringBuilder.Append("0" + Convert.ToString(b, 16));
                }
                else
                {
                    stringBuilder.Append(Convert.ToString(b, 16));
                }
                    
            }

            return stringBuilder.ToString();
        }

        public static string FromIntToHex(int input)
        {
            var sizeHex = input.ToString("X");

            if (sizeHex.Length % 2 != 0)
            {
                sizeHex = "0" + sizeHex;
            }

            return sizeHex;
        }

        public static byte[] GenerateKey(int keySize)
        {
            var rng = new RNGCryptoServiceProvider();
            var key = new byte[keySize / 8];
            rng.GetBytes(key);

            for (var i = 0; i < key.Length; i++)
            {
                var keyByte = key[i] & 0xFE;
                var parity = 0;
                for (var j = keyByte; j != 0; j >>= 1)
                {
                    parity ^= j & 1;
                }
                key[i] = (byte)(keyByte | (parity == 0 ? 1 : 0));
            }

            return key;
        }

    }
}
