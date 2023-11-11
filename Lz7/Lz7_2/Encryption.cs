using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml;

namespace Lz7_2
{
    internal static class Encryption
    {
        private static RSAParameters _publicKey, _privateKey;
        public static void AssignNewKey(string path)
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                _publicKey = rsa.ExportParameters(false);
                _privateKey = rsa.ExportParameters(true);

                File.WriteAllText(path, rsa.ToXmlString(false));
            }
        }

        public static byte[] EncryptData(byte[] dataToEncrypt, RSACryptoServiceProvider rsa)
        {
            byte[] cypherBytes;
            rsa.PersistKeyInCsp = true;
            cypherBytes = rsa.Encrypt(dataToEncrypt, true);

            return cypherBytes;
        }

        public static byte[] EncryptData(byte[] dataToEncrypt)
        {
            Console.WriteLine("Do you want to load a custom key? (y/n)");
            char c = Console.ReadKey().KeyChar;
            Console.WriteLine();
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                if (c == 'y')
                {
                    Console.Write("Enter path to key: ");
                    var path = Console.ReadLine().Replace("\"","");
                    rsa.FromXmlString(File.ReadAllText(path));
                }
                else rsa.ImportParameters(_publicKey);

                return EncryptData(dataToEncrypt, rsa);
            }
        }

        public static byte[] DecryptData(byte[] dataToDecrypt)
        {
            byte[] plainBytes;
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.PersistKeyInCsp = true;
                rsa.ImportParameters(_privateKey);
                plainBytes = rsa.Decrypt(dataToDecrypt, true);
            }
            return plainBytes;
        }
    }
}