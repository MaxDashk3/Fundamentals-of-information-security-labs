using System.Text;

namespace Lz3_4_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter path to the file you want to hash:");
            var pathto = Console.ReadLine();
            var message = File.ReadAllBytes(pathto);
            var hash = new Hashing(message);

            string[] hashmetods = { "MD5", "SHA1", "SHA256", "SHA384", "SHA512" };
            byte[][] hashes = new byte[hashmetods.Length][];
            hashes[0] = hash.MD5hash();
            hashes[1] = hash.SHA1hash();
            hashes[2] = hash.SHA256hash();
            hashes[3] = hash.SHA384hash();
            hashes[4] = hash.SHA512hash();

            Console.WriteLine($"Message : {Encoding.UTF8.GetString(message)}\n");

            for ( int i = 0; i < hashmetods.Length; i++) 
            {
                Console.WriteLine(hashmetods[i]+":");
                Console.WriteLine("Hash: " + Convert.ToBase64String(hashes[i]));
                if (i == 0)
                {
                    Console.WriteLine("GUID: " + new Guid(hashes[0]).ToString());
                }
                Console.WriteLine();
            }
        }
    }
}