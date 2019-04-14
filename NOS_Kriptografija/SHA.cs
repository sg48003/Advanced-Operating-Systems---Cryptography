using System.Security.Cryptography;
using System.Text;

namespace NOS_Kriptografija
{
    class SHA
    {
        public static string Hash(string text, HashingMode mode)
        {
            var textBytes = Encoding.UTF8.GetBytes(text);

            byte[] hashBytes;
            switch (mode)
            {
                case HashingMode.SHA_1:
                    var SHA1 = new SHA1CryptoServiceProvider();
                    hashBytes = SHA1.ComputeHash(textBytes);
                    break;
                case HashingMode.SHA_2_256:
                    var SHA2_256 = new SHA256CryptoServiceProvider();
                    hashBytes = SHA2_256.ComputeHash(textBytes);
                    break;
                case HashingMode.SHA_2_512:
                    var SHA2_512 = new SHA512CryptoServiceProvider();
                    hashBytes = SHA2_512.ComputeHash(textBytes);
                    break;
                //case HashingMode.SHA_3_256:
                //    var SHA3_256 = new Sha3Digest(256);
                //    SHA3_256.Update(Convert.ToByte(textBytes));
                //    SHA3_256.DoFinal(hashBytes);
                //    break;
                //case HashingMode.SHA_3_256:
                //    var SHA3_512 = new Sha3Digest(512);
                //    break;
                default:
                    var SHA_default = new SHA1CryptoServiceProvider();
                    hashBytes = SHA_default.ComputeHash(textBytes);
                    break;
            }

            var hash = HelperFunctions.FromByteToHex(hashBytes);
            return hash;

        }
    }
}
