using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Lz9_10
{
    internal static class Signature
    {
        private readonly static string CspContainerName = "RsaContainer";
        private static CspParameters cspParams = new CspParameters
        {
            KeyContainerName = CspContainerName,
            Flags = CspProviderFlags.UseMachineKeyStore,
        };

        public static byte[] Generate(byte[] data)
        {
            using (var rsa = new RSACryptoServiceProvider(cspParams))
            {
                rsa.PersistKeyInCsp = true;
                var rsaFormatter = new RSAPKCS1SignatureFormatter(rsa);

                rsaFormatter.SetHashAlgorithm(nameof(SHA512));

                byte[] hashOfData = SHA512.HashData(data);

                return rsaFormatter.CreateSignature(hashOfData);
            }
        }

        public static bool CheckSignature(string publicKeyPath, byte[] data, byte[] signature)
        {
            using (var rsa = new RSACryptoServiceProvider(cspParams))
            {
                rsa.FromXmlString(File.ReadAllText(publicKeyPath));
                rsa.PersistKeyInCsp = true;

                var rsaDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
                rsaDeformatter.SetHashAlgorithm(nameof(SHA512));

                byte[] hashOfData = SHA512.HashData(data);

                return rsaDeformatter.VerifySignature(hashOfData, signature);
            }
        }
        public static void SavePublic(string path)
        {
            using (var rsa = new RSACryptoServiceProvider(cspParams))
            {
                rsa.PersistKeyInCsp = true;
                File.WriteAllText(path, rsa.ToXmlString(false));
            }
        }
    }
}
