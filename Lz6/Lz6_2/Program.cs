using System.Security.Cryptography;
using System.Text;

namespace Lz6_2
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
            Console.Write("Enter message to encrypt: ");
            var msgbytes = Encoding.UTF8.GetBytes(Console.ReadLine());

            Console.Write("Enter message to generate key: ");
            var keymsg = Encoding.UTF8.GetBytes(Console.ReadLine());

            Console.Write("Enter message to generate initialization vector: ");
            var ivmsg = Encoding.UTF8.GetBytes(Console.ReadLine());

            Encryption.Init(keymsg, ivmsg);

            Console.WriteLine($"\nKey: {Convert.ToBase64String(Encryption.key)}\n" +
                $"Initialization vector: {Convert.ToBase64String(Encryption.aesiv)}\n");

            ShowResult(Encryption.EncryptAes, Encryption.DecryptAes, msgbytes, "AES");
            ShowResult(Encryption.EncryptDes, Encryption.DecryptDes, msgbytes, "DES");
            ShowResult(Encryption.EncryptTrDes, Encryption.DecryptTrDes, msgbytes, "Triple DES");
        }
    }
}