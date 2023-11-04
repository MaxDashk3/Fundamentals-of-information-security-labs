using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Lz6_2
{
    internal static class Encryption
    {
        public static byte[] key;
        public static byte[] deskey;
        public static byte[] aesiv;
        public static byte[] desiv;

        public static void Init(byte[] keymsg, byte[] ivmsg)
        {
            key = RfcHashing(keymsg, 24);
            aesiv = RfcHashing(ivmsg, 16);

            desiv = new byte[8];
            Array.Copy(aesiv, desiv, 8);

            deskey = new byte[8];
            Array.Copy(key, deskey, 8);
        }
  
        static byte[] RfcHashing(byte[] key, int length)
        {
            using (Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(key,GenerateRandom(32),60000))
            {
                return rfc.GetBytes(length);
            }
        }
        public static byte[] GenerateRandom(int length)
        {
            return RandomNumberGenerator.GetBytes(length);
        }

        public static byte[] EncryptAes(byte[] message)
        {
            using (var aes = Aes.Create())
            {
                aes.IV = aesiv;
                aes.Key = key;
                using (var memoryStream = new MemoryStream())
                {
                    var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
                    cryptoStream.Write(message, 0, message.Length);
                    cryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }

        public static byte[] DecryptAes(byte[] message)
        {
            using (var aes = Aes.Create())
            {
                aes.IV = aesiv;
                aes.Key = key;
                using (var memoryStream = new MemoryStream())
                {
                    var cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write);
                    cryptoStream.Write(message);
                    cryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }

        public static byte[] EncryptDes(byte[] message)
        {
            using (var des = DES.Create())
            {
                des.IV = desiv;
                des.Key = deskey;
                using (var memoryStream = new MemoryStream())
                {
                    var cryptoStream = new CryptoStream(memoryStream, des.CreateEncryptor(), CryptoStreamMode.Write);
                    cryptoStream.Write(message);
                    cryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }

        public static byte[] DecryptDes(byte[] message)
        {
            using (var des = DES.Create())
            {
                des.IV = desiv;
                des.Key = deskey;
                using (var memoryStream = new MemoryStream())
                {
                    var cryptoStream = new CryptoStream(memoryStream, des.CreateDecryptor(), CryptoStreamMode.Write);
                    cryptoStream.Write(message);
                    cryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }

        public static byte[] EncryptTrDes(byte[] message)
        {
            using (var des = TripleDES.Create())
            {
                des.IV = desiv;
                des.Key = key;
                using (var memoryStream = new MemoryStream())
                {
                    var cryptoStream = new CryptoStream(memoryStream, des.CreateEncryptor(), CryptoStreamMode.Write);
                    cryptoStream.Write(message);
                    cryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }

        public static byte[] DecryptTrDes(byte[] message)
        {
            using (var des = TripleDES.Create())
            {
                des.IV = desiv;
                des.Key = key;
                using (var memoryStream = new MemoryStream())
                {
                    var cryptoStream = new CryptoStream(memoryStream, des.CreateDecryptor(), CryptoStreamMode.Write);
                    cryptoStream.Write(message);
                    cryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }
    }
}
