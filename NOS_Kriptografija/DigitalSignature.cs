using System;
using System.Windows.Forms;

namespace NOS_Kriptografija
{
    class DigitalSignature
    {
        public static void CreateDigitalSignature_FromFile(string inputFile, string RSAprivateKeyFile, string outputFile, HashingMode mode)
        {
            var plainText = FileManager.ReadFile_String(inputFile);

            var hash = SHA.Hash(plainText, mode);

            var privateKey = FileManager.Read_RSAKey(RSAprivateKeyFile);
            var signature = RSA.Encrypt(hash, privateKey.Modulus, privateKey.Exponent);

            var signatureBytes = Convert.FromBase64String(signature);
            signature = HelperFunctions.FromByteToHex(signatureBytes);

            FileManager.Write_Signature(outputFile, signature, privateKey.Modulus.Length * 4, mode);
        }

        public static void CreateDigitalSignature_FromString(string plainText, string RSAprivateKeyFile, string outputFile, HashingMode mode)
        {
            var hash = SHA.Hash(plainText, mode);

            var privateKey = FileManager.Read_RSAKey(RSAprivateKeyFile);
            var signature = RSA.Encrypt(hash, privateKey.Modulus, privateKey.Exponent);

            var signatureBytes = Convert.FromBase64String(signature);
            signature = HelperFunctions.FromByteToHex(signatureBytes);

            FileManager.Write_Signature(outputFile, signature, privateKey.Modulus.Length * 4, mode);
        }

        public static void CheckDigitalSignature_FromFile(string textFile, string signatureFile, string RSApublicKeyFile, TextBox outputTextBox, HashingMode mode)
        {
            var text = FileManager.ReadFile_String(textFile);
            var signature = FileManager.Read_Signature(signatureFile);

            var publicKey = FileManager.Read_RSAKey(RSApublicKeyFile);
            var signatureBytes = HelperFunctions.FromHexToByte(signature);

            var decoded = RSA.Decrypt(Convert.ToBase64String(signatureBytes), publicKey.Modulus, publicKey.Exponent);
            var hash = SHA.Hash(text, mode);

            outputTextBox.Text = hash == decoded ? "Potpis je valjan!" : "Potpis nije valjan!";
        }

        public static void CheckDigitalSignature_FromString(string text, string signatureFile, string RSApublicKeyFile, TextBox outputTextBox, HashingMode mode)
        {
            var signature = FileManager.Read_Signature(signatureFile);

            var publicKey = FileManager.Read_RSAKey(RSApublicKeyFile);
            var signatureBytes = HelperFunctions.FromHexToByte(signature);

            var decoded = RSA.Decrypt(Convert.ToBase64String(signatureBytes), publicKey.Modulus, publicKey.Exponent);
            var hash = SHA.Hash(text, mode);

            outputTextBox.Text = hash == decoded ? "Potpis je valjan!" : "Potpis nije valjan!";
        }
    }
}
