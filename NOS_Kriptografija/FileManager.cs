using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace NOS_Kriptografija
{
    class FileManager
    {
        #region Reading

        public static string ReadFile_String(string file)
        {
            return File.ReadAllText(Program.Direktorij + file);
        }

        public static byte[] ReadFile_Byte(string file)
        {
            return File.ReadAllBytes(Program.Direktorij + file);
        }

        public static byte[] ReadFile_IVector(string file)
        {
            var vectorFile = new StreamReader(Program.Direktorij + file);

            string currentLine;
            var vector = "";

            while (vectorFile.ReadLine() != "Initialization vector:") { }
            while ((currentLine = vectorFile.ReadLine()) != "---END OS2 CRYPTO DATA---" && currentLine != "") vector += currentLine?.Substring(4);

            var vectorBytes = HelperFunctions.FromHexToByte(vector);

            return vectorBytes;

        }

        #endregion

        #region RSA

        public static HelperClasses.RSAKey Read_RSAKey(string file)
        {
            var streamReader = new StreamReader(Program.Direktorij + file);

            string currentLine;
            var RSA = new HelperClasses.RSAKey();

            while (streamReader.ReadLine() != "Modulus:") { }

            while ((currentLine = streamReader.ReadLine()) != "")
            {
                RSA.Modulus += currentLine?.Substring(4);
            }

            while ((currentLine = streamReader.ReadLine()) != "Private exponent:" && currentLine != "Public exponent:") { }

            while ((currentLine = streamReader.ReadLine()) != "---END OS2 CRYPTO DATA---" && currentLine != "")
            {
                RSA.Exponent += currentLine?.Substring(4);
            }

            return RSA;

        }

        #endregion

        #region Envelope

        public static HelperClasses.Envelope Read_Envelope(string file)
        {
            var streamReader = new StreamReader(Program.Direktorij + file);
            string currentLine;
            var envelope = new HelperClasses.Envelope();

            while (streamReader.ReadLine() != "Envelope data:") { }
            while ((currentLine = streamReader.ReadLine()) != "")
            {
                envelope.Data += currentLine?.Substring(4);
            }

            while (streamReader.ReadLine() != "Envelope crypt key:") { }
            while ((currentLine = streamReader.ReadLine()) != "---END OS2 CRYPTO DATA---" && currentLine != "")
            {
                envelope.Key += currentLine?.Substring(4);
            }

            streamReader.Close();
            return envelope;
        }

        public static void Write_Envelope(string file, HelperClasses.Envelope envelope,int symetricAlgorithmKeyLenght, int RSAKeyLength, SymetricAlgorithm algorithm)
        {
            var streamWriter = new StreamWriter(Program.Direktorij + file);

            streamWriter.WriteLine("---BEGIN OS 2 CRYPTO DATA---");
            streamWriter.WriteLine();
            streamWriter.WriteLine("Description");
            streamWriter.WriteLine("    Envelope");
            streamWriter.WriteLine();
            streamWriter.WriteLine("File name:");

            var breadCrumbs = file.Split('\\');

            streamWriter.WriteLine("    " + breadCrumbs[breadCrumbs.Length - 1]);
            streamWriter.WriteLine();

            streamWriter.WriteLine("Method:");
            streamWriter.WriteLine(algorithm == SymetricAlgorithm.AES ? "    AES" : "    3DES");
            streamWriter.WriteLine("    RSA");
            streamWriter.WriteLine();
            streamWriter.WriteLine("Key length:");
            streamWriter.WriteLine("    " + HelperFunctions.FromIntToHex(symetricAlgorithmKeyLenght));
            streamWriter.WriteLine("    " + HelperFunctions.FromIntToHex(RSAKeyLength));
            streamWriter.WriteLine();
            streamWriter.WriteLine("Envelope data:");

            var NumLines = (double)envelope.Data.Length / 60;

            if (Math.Truncate(NumLines) < NumLines)
            {
                NumLines++;
            }

            for (var i = 0; i < Math.Truncate(NumLines); i++)
            {
                if (envelope.Data.Length - i * 60 < 60)
                {
                    streamWriter.WriteLine("    " + envelope.Data.Substring(i * 60, envelope.Data.Length - i * 60));
                }
                else
                {
                    streamWriter.WriteLine("    " + envelope.Data.Substring(i * 60, 60));
                }
            }

            streamWriter.WriteLine();
            streamWriter.WriteLine("Envelope crypt key:");

            NumLines = (double)envelope.Key.Length / 60;

            if (Math.Truncate(NumLines) < NumLines)
            {
                NumLines++;
            }

            for (var i = 0; i < Math.Truncate(NumLines); i++)
            {
                if (envelope.Key.Length - i * 60 < 60)
                {
                    streamWriter.WriteLine("    " + envelope.Key.Substring(i * 60, envelope.Key.Length - i * 60));
                }
                else
                {
                    streamWriter.WriteLine("    " + envelope.Key.Substring(i * 60, 60));
                }
            }

            streamWriter.WriteLine();
            streamWriter.WriteLine("---END OS2 CRYPTO DATA---");
            streamWriter.Close();
        }

        #endregion

        #region Signature

        public static string Read_Signature(string file)
        {
            var streamReader = new StreamReader(Program.Direktorij + file);
            string currentLine;
            var Signature = "";

            while (streamReader.ReadLine() != "Signature:") { }
            while ((currentLine = streamReader.ReadLine()) != "---END OS2 CRYPTO DATA---" && currentLine != "")
            {
                Signature += currentLine?.Substring(4);
            }

            streamReader.Close();

            return Signature;
        }

        public static void Write_Signature(string file, string signature, int RSAKeyLength, HashingMode mode)
        {
            var streamWriter = new StreamWriter(Program.Direktorij + file);

            streamWriter.WriteLine("---BEGIN OS 2 CRYPTO DATA---");
            streamWriter.WriteLine();
            streamWriter.WriteLine("Description");
            streamWriter.WriteLine("    Singature");
            streamWriter.WriteLine();
            streamWriter.WriteLine("File name:");

            var breadCrumbs = file.Split('\\');

            streamWriter.WriteLine("    " + breadCrumbs[breadCrumbs.Length - 1]);
            streamWriter.WriteLine();

            streamWriter.WriteLine("Method:");
            int hashLenght;
            if (mode == HashingMode.SHA_1)
            {
                streamWriter.WriteLine("    SHA-1");
                hashLenght = 160;
            }
            else if (mode == HashingMode.SHA_2_256)
            {
                streamWriter.WriteLine("    SHA-2 (256)");
                hashLenght = 256;
            }
            else
            {
                streamWriter.WriteLine("    SHA-2 (512)");
                hashLenght = 512;
            }
            streamWriter.WriteLine("    RSA");
            streamWriter.WriteLine();
            streamWriter.WriteLine("Key length:");
            streamWriter.WriteLine("    " + HelperFunctions.FromIntToHex(hashLenght));
            streamWriter.WriteLine("    " + HelperFunctions.FromIntToHex(RSAKeyLength));
            streamWriter.WriteLine();
            streamWriter.WriteLine("Signature:");

            var NumLines = (double)signature.Length / 60;

            if (Math.Truncate(NumLines) < NumLines)
            {
                NumLines++;
            }

            for (var i = 0; i < Math.Truncate(NumLines); i++)
            {
                if (signature.Length - i * 60 < 60)
                {
                    streamWriter.WriteLine("    " + signature.Substring(i * 60, signature.Length - i * 60));
                }
                else
                {
                    streamWriter.WriteLine("    " + signature.Substring(i * 60, 60));
                }
            }

            streamWriter.WriteLine();
            streamWriter.WriteLine("---END OS2 CRYPTO DATA---");
            streamWriter.Close();
        }

        #endregion

        #region Write

        public static void Write(byte[] text, string file)
        {
            var streamWriter = new StreamWriter(Program.Direktorij + file);

            streamWriter.Write(Encoding.UTF8.GetString(text));

            streamWriter.Close();
        }

        public static void Write(string text, string file)
        {
            var streamWriter = new StreamWriter(Program.Direktorij + file);

            streamWriter.Write(text);

            streamWriter.Close();
        }

        #endregion

    }
}
