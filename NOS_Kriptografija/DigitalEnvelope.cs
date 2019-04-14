using System;
using System.Text;

namespace NOS_Kriptografija
{
    public class DigitalEnvelope
    {
        public static void CreateDigitalEnvelope(string textFile, string RSApublicKey, string outputFile, EncryptionMode mode, SymetricAlgorithm algorithm, KeySize keySize)
        {
            var envelope = new HelperClasses.Envelope();

            var text = FileManager.ReadFile_Byte(textFile);
            
            var vector = FileManager.ReadFile_IVector("IVector.txt");

            byte[] cryptedText;
            byte[] key;
            if (algorithm == SymetricAlgorithm.THREE_DES)
            {
                key = HelperFunctions.GenerateKey((int)keySize);
                cryptedText = THREE_DES.Encrypt(text, key, mode);
            }
            else
            {
                key = HelperFunctions.GenerateKey((int)keySize);
                cryptedText = AES.Encrypt(text, key, vector, mode);
            }

            var keyHex = HelperFunctions.FromByteToHex(key);

            var publicKey = FileManager.Read_RSAKey(RSApublicKey);

            var RSAcrypted = RSA.Encrypt(keyHex, publicKey.Modulus, publicKey.Exponent);

            var cryptedKey = Convert.FromBase64String(RSAcrypted);
            RSAcrypted = HelperFunctions.FromByteToHex(cryptedKey);

            envelope.Data = Convert.ToBase64String(cryptedText);
            envelope.Key = RSAcrypted;

            FileManager.Write_Envelope(outputFile, envelope, key.Length * 8, publicKey.Modulus.Length * 4, algorithm);
        }

        public static void OpenDigitalEnvelope(string envelopeFile, string RSAprivateKey, string outputFile, EncryptionMode mode, SymetricAlgorithm algorithm)
        {
            var envelope = FileManager.Read_Envelope(envelopeFile);
            var privateKey = FileManager.Read_RSAKey(RSAprivateKey);

            var keyCipher = Convert.ToBase64String(HelperFunctions.FromHexToByte(envelope.Key));
            var key = RSA.Decrypt(keyCipher, privateKey.Modulus, privateKey.Exponent);


            var input = Convert.FromBase64String(envelope.Data);
            var keyBytes = HelperFunctions.FromHexToByte(key);
            var vector = FileManager.ReadFile_IVector("IVector.txt");

            var data = algorithm == SymetricAlgorithm.THREE_DES ? THREE_DES.Decrypt(input, keyBytes, mode) : AES.Decrypt(input, keyBytes, vector, mode);

            var envelopeText = Encoding.ASCII.GetString(data);

            FileManager.Write(envelopeText, outputFile);
        }

    }
}
