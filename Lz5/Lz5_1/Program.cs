using System.Text;

namespace Lz5_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter password: ");
            var password = Console.ReadLine();

            Console.WriteLine($"Password: {password}\n");

            byte[] salt = SaltedHash.SaltGenerator();
            Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}\n");

            byte[] hash = SaltedHash.Hash(Encoding.UTF8.GetBytes(password), salt);
            var hashstr = Convert.ToBase64String(hash);
            Console.WriteLine($"Hash: {hashstr}");
        }
    }
}