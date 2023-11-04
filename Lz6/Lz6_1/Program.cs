using System.Security.Cryptography;
using System.Text;

namespace Lz6_1
{
    internal class Program
    {
        delegate byte[] Method(byte[] message);

        static void ShowResult(Method enc, Method dec, byte[] message, string methodname)
        {
            Console.WriteLine(methodname + ":");

            var EncryptedMsg = enc(message);
            Console.WriteLine($"Encrypted: {Convert.ToBase64String(EncryptedMsg)}" +
                $"\nDecrypted: {Encoding.UTF8.GetString(dec(EncryptedMsg))}\n");
        }
        static void Main(string[] args)
        {
            Console.Write("Enter message: ");
            var msgbytes = Encoding.UTF8.GetBytes(Console.ReadLine());
            Encryption.Init();

            Console.WriteLine($"\nKey: {Convert.ToBase64String(Encryption.key)}\n" +
                $"Initialization vector: {Convert.ToBase64String(Encryption.aesiv)}\n");

            ShowResult(Encryption.EncryptAes, Encryption.DecryptAes, msgbytes, "AES");
            ShowResult(Encryption.EncryptDes, Encryption.DecryptDes, msgbytes, "DES");
            ShowResult(Encryption.EncryptTrDes, Encryption.DecryptTrDes, msgbytes, "Triple DES");
        }
    }
}