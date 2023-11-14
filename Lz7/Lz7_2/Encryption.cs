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
        private readonly static string CspContainerName = "RsaContainer";
        public static void AssignNewKey()
        {
            CspParameters cspParameters = new CspParameters(1)
            {
                KeyContainerName = CspContainerName,
                Flags = CspProviderFlags.UseMachineKeyStore, //Рівень пристрою
                ProviderName = "Microsoft Strong Cryptographic Provider"
            };
            var rsa = new RSACryptoServiceProvider(cspParameters)
            {
                PersistKeyInCsp = true
            };
        }

        public static void SavePublic(string path)
        {
            var cspParams = new CspParameters
            {
                KeyContainerName = CspContainerName,
                Flags = CspProviderFlags.UseMachineKeyStore
            };
            using (var rsa = new RSACryptoServiceProvider(cspParams))
            {
                rsa.PersistKeyInCsp = true;
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
            if (c == 'y')
            {
                using (var rsa = new RSACryptoServiceProvider(2048))
                {
                    Console.Write("Enter path to key: ");
                    var path = Console.ReadLine().Replace("\"", "");
                    rsa.FromXmlString(File.ReadAllText(path));
                    return EncryptData(dataToEncrypt, rsa);
                } 
            }
            else
            {
                var cspParams = new CspParameters
                {
                    KeyContainerName = CspContainerName,
                    Flags = CspProviderFlags.UseMachineKeyStore
                };
                using (var rsa = new RSACryptoServiceProvider(cspParams)) 
                {
                    rsa.PersistKeyInCsp = true;
                    return EncryptData(dataToEncrypt, rsa);
                }
            }
        }

        public static byte[] DecryptData(byte[] dataToDecrypt)
        {
            byte[] plainBytes;
            var cspParams = new CspParameters
            {
                KeyContainerName = CspContainerName,
                Flags = CspProviderFlags.UseMachineKeyStore
            };
            using (var rsa = new RSACryptoServiceProvider(cspParams))
            {
                rsa.PersistKeyInCsp = true;
                plainBytes = rsa.Decrypt(dataToDecrypt, true);
            }
            return plainBytes;

        }
    }
}