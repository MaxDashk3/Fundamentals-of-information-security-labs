using System.Security.Cryptography;

namespace Lz1_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var rndNumberGenerator = new RNGCryptoServiceProvider();              
            var randomNumber = new byte[9];
            rndNumberGenerator.GetBytes(randomNumber);
            var ConvertedResult = Convert.ToBase64String(randomNumber);

            Console.WriteLine(ConvertedResult);

        }
    }
}