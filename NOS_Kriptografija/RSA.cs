using System;
using System.Collections.Generic;
using System.Text;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;

namespace NOS_Kriptografija
{
    internal class RSA
    {
        public static string Encrypt(string PlainText, string mod, string exp)
        {
            var Modulus = HelperFunctions.FromHexToByte(mod);
            var Exponent = HelperFunctions.FromHexToByte(exp);

            var modulus = new BigInteger(1, Modulus);
            var exponent = new BigInteger(1, Exponent);

            var Parameters = new RsaKeyParameters(false, modulus, exponent);
            var RSAengine = new RsaEngine();
            RSAengine.Init(true, Parameters);

            var blockSize = RSAengine.GetInputBlockSize();

            var output = new List<byte>();

            var PlainTextBytes = Encoding.UTF8.GetBytes(PlainText);

            for (var chunkPosition = 0; chunkPosition < PlainTextBytes.Length; chunkPosition += blockSize)
            {
                var chunkSize = Math.Min(blockSize, PlainTextBytes.Length - chunkPosition * blockSize);
                output.AddRange(RSAengine.ProcessBlock(PlainTextBytes, chunkPosition, chunkSize));
            }

            var CipherText = Convert.ToBase64String(output.ToArray());
            return CipherText;
        }

        public static string Decrypt(string CipherText, string mod, string exp)
        {
            var Modulus = HelperFunctions.FromHexToByte(mod);
            var Exponent = HelperFunctions.FromHexToByte(exp);

            var CipherTextBytes = Convert.FromBase64String(CipherText);

            var modulus = new BigInteger(1, Modulus);
            var exponent = new BigInteger(1, Exponent);

            var Parameters = new RsaKeyParameters(true, modulus, exponent);
            var RSAengine = new RsaEngine();
            RSAengine.Init(false, Parameters);

            var blockSize = RSAengine.GetInputBlockSize();

            var output = new List<byte>();

            for (var chunkPosition = 0; chunkPosition < CipherTextBytes.Length; chunkPosition += blockSize)
            {
                var chunkSize = Math.Min(blockSize, CipherTextBytes.Length - chunkPosition * blockSize);
                output.AddRange(RSAengine.ProcessBlock(CipherTextBytes, chunkPosition, chunkSize));
            }

            var output2 = output.ToArray();
            var PlainText = Encoding.ASCII.GetString(output2);
            return PlainText;
        }

    }
}
