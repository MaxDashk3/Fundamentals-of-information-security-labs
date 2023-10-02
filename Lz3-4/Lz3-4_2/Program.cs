﻿using System.Security.Cryptography;
using System.Text;

namespace Lz3_4_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string md5hash = "po1MVkAE7IjUUwu61XxgNg==";
            Console.WriteLine("Searching password with hash " + md5hash + " (Unicode encoding)");
            byte[] bytes = Convert.FromBase64String(md5hash);

            for (int i = 0; i < 100000000; i++)
            {
                string s = Convert.ToString(i).PadLeft(8, '0');
                //Console.WriteLine(s);
                var b = Encoding.Unicode.GetBytes(s);
                
                if (Enumerable.SequenceEqual(MD5.HashData(b),bytes))
                {
                    Console.WriteLine("It is " + s);
                    break;
                }
            }
            Console.WriteLine("Done");
        }
    }
}