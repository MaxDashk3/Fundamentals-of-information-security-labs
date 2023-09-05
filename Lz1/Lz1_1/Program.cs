using System;
namespace Lz1_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random(1234567890);

            for (int i = 0; i < 10; i++)
            {
                int num = random.Next(1, 10);
                Console.WriteLine(num);

            }
        }
    }
}