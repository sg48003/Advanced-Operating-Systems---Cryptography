using System.Windows.Forms;

namespace NOS_Kriptografija
{
    class DigitalSeal
    {
        public static void CreateDigitalSeal(string inputFile, string RSApublicReciever, string RSAprivateSender, string envelopeFile, string signatureFile, EncryptionMode encryptionMode, HashingMode hashingMode, SymetricAlgorithm algorithm, KeySize keySize)
        {
            DigitalEnvelope.CreateDigitalEnvelope(inputFile, RSApublicReciever, envelopeFile, encryptionMode, algorithm, keySize);

            var envelope = FileManager.Read_Envelope(envelopeFile);

            var hash = SHA.Hash(envelope.Data + envelope.Key, hashingMode);

            DigitalSignature.CreateDigitalSignature_FromString(hash, RSAprivateSender, signatureFile, hashingMode);

        }

        public static void CheckDigitalSeal(string outputFile, string RSApublicSender, string RSAprivateReciever, string envelopeFile, string signatureFile, TextBox sealCheck, EncryptionMode encryptionMode, HashingMode hashingMode, SymetricAlgorithm algorithm)
        {
            DigitalEnvelope.OpenDigitalEnvelope(envelopeFile, RSAprivateReciever, outputFile, encryptionMode, algorithm);

            var envelope = FileManager.Read_Envelope(envelopeFile);

            var hash = SHA.Hash(envelope.Data + envelope.Key, hashingMode);

            DigitalSignature.CheckDigitalSignature_FromString(hash, signatureFile, RSApublicSender, sealCheck, hashingMode);

        }
    }
}
