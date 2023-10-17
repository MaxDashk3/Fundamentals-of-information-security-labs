using System.Diagnostics;
using System.Security.Authentication;
using System.Security.Cryptography;

namespace Lz5_2
{
    internal class Program
    {
        static void Main(string[] args)
        {   
            Console.Write("Enter password: ");
            PBKDF2.Password = Console.ReadLine();
            PBKDF2.GenerateSalt();
            PBKDF2.SetHashType();
            int iterations = 60000;

            for (int i = 0; i < 10; i++)
            {
                HashWithCounter(iterations);
                iterations += 50000;
            }

        }
        static void HashWithCounter(int iterations)
        {
            var sw = new Stopwatch();
            sw.Start();

            PBKDF2.HashData(iterations);

            sw.Stop();
            PBKDF2.PrintData();
            Console.WriteLine($"Iterations: {iterations}, Elapsed time: {sw.ElapsedMilliseconds}ms\n");
        }
    }
}