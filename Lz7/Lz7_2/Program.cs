﻿using System.Text;

namespace Lz7_2
{
    internal class Program
    {
        static void Main(string[] args)
        {      
            Encryption.AssignNewKey();
            char c = ' ';

            while (true)
            {

                Console.WriteLine("\nDo you want to encrypt (e) or decrypt (d) a message? (press x to exit, press s to save public key): ");
                c = Console.ReadKey().KeyChar;
                Console.WriteLine();
                switch (c)
                {
                    case 'e':
                        Console.Write("Enter message to encrypt: ");
                        byte[] data = Encoding.UTF8.GetBytes(Console.ReadLine());
                        var enc = Encryption.EncryptData(data);
                        Console.WriteLine("Encrypted message: \n" + Convert.ToBase64String(enc));
                        break;

                    case 'd':
                        Console.WriteLine("Enter message to decrypt: ");
                        data = Convert.FromBase64String(Console.ReadLine());
                        Console.WriteLine();
                        Console.WriteLine("Decrypted message: " + Encoding.UTF8.GetString(Encryption.DecryptData(data)));
                        break;

                    case 's':
                        Console.WriteLine("Enter path to save your public key: ");
                        var path = Console.ReadLine().Replace("\"", "") + "\\key.txt";
                        Encryption.SavePublic(path);
                        break;

                    case 'x':
                        break;

                    default:
                        Console.WriteLine("Unrecognized command");
                        break;
                }
                if (c == 'x') break;
            }
        }
    }
}