using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Lz3_4_3
{
    internal static class Tools
    {
        public static byte[] data;

        public static byte[] key;

        public static string savepath;

        private static bool Comparebytes(byte[] a, byte[] b)
        {
            bool comp = true;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i]) { comp = false; break; }
            }
            return comp;
        }
        public static byte[] GenerateKey(int length)
        {
            return RandomNumberGenerator.GetBytes(length);
        }
        public static byte[] Hash()
        {
            using (var hmacsha256 = new HMACSHA256(key))
            {
                return hmacsha256.ComputeHash(data);
            }
        }
        public static void GetKey(char choice)
        {
            switch (choice)
            {
                case 'f' or 'F':
                    Console.WriteLine("\nEnter path to the key:");
                    key = File.ReadAllBytes(Console.ReadLine().Replace("\"", ""));
                    break;

                case 's' or 'S':
                    Console.WriteLine("\nEnter key:");
                    key = Encoding.UTF8.GetBytes(Console.ReadLine());
                    break;

                case 'g' or 'G':
                    Console.Write("\nHow many bytes do you want the key to be? : ");
                    int length = Convert.ToInt32(Console.ReadLine());
                    key = GenerateKey(length);
                    break;
                default:
                    Console.WriteLine("Error! Unrecognized command!");
                    break;
            }
        }

        public static bool Verify()
        {
            byte[] hash;
            Console.WriteLine("\nEnter path to hash:");
            var hpath = Console.ReadLine().Replace("\"","");
            hash = File.ReadAllBytes(hpath);
            if (Comparebytes(hash, Hash())) { return true; }
            return false;
        }

        public static void ComputeHash()
        {
            var hash = Hash();
            Console.WriteLine("\nHash: " + Convert.ToBase64String(hash));
            Console.WriteLine("\nKey: " + Convert.ToBase64String(key));
            File.WriteAllBytes(savepath + """\hash.dat""", hash);
            Console.WriteLine("\nDo you want to save key (y/n)?");
            char a = Console.ReadKey().KeyChar;
            bool savekey = a == 'y' ? true : false;
            if (savekey)
            {
                File.WriteAllBytes(savepath + """\key.dat""", key);
            }
        }
    }
}
