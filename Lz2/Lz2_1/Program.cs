using System.Security.Cryptography;
using System.Text;

namespace Lz2_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter path to the file to encrypt:");
            var path = Console.ReadLine();
            var msg = File.ReadAllText(path);
            var msgbytes = Encoding.UTF8.GetBytes(msg);

            //generating key
            var key = new byte[msgbytes.Length];
            key = RandomNumberGenerator.GetBytes(key.Length);

            var encryptbytes = EncodeFunc(msgbytes, key);
            string savepath = path.Remove(path.LastIndexOf(@"\"));

            File.WriteAllBytes(savepath + @"\encrypted.dat", encryptbytes);
            File.WriteAllBytes(savepath + @"\key.dat", key);

            Console.WriteLine("\n\nMessage: " + msg);
            Console.WriteLine("\n\nEncrypted message: " + Encoding.UTF8.GetString(encryptbytes));
            Console.WriteLine("\n\nKey: " + Encoding.UTF8.GetString(key));
        }
        static public byte[] EncodeFunc(byte[] msg,byte[] key)
        {
            var encrypted = new byte[msg.Length];
            for (int i = 0; i < msg.Length; i++)
            {
                encrypted[i] = (byte)(msg[i] ^ key[i]);
            }
            return encrypted;
        }
    }
}