using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Lz5_3
{
    internal class PBKDF2
    {
        public const int saltlength = 32;
        public const int iterations = 60000;
        struct PasswordData
        {
            public byte[] Hash;
            public byte[] Salt;
        }

        static Dictionary<string, PasswordData> DataBase = new Dictionary<string, PasswordData>();

        static byte[] SaltGen ()
        {
            return RandomNumberGenerator.GetBytes(saltlength);
        }

        static byte[] GetHash(byte[] password, byte[] salt)
        {
            using (var rfc = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256))
            {
                return rfc.GetBytes(SHA256.HashSizeInBytes);
            }   
        }

        public static void Authenticate(string login, char c)
        {
            byte[] password;
            PasswordData data;
            switch (c)
            {
                case 'l'://checking if the user is present
                    try
                    {
                        data = DataBase[login];
                        Console.Write("Enter password: ");
                        password = Encoding.UTF8.GetBytes(Console.ReadLine());
                        if (Enumerable.SequenceEqual(GetHash(password, data.Salt), data.Hash))
                        {
                            Console.WriteLine("\n\n\n\nLogin successful!\n\n\n\n");
                        }
                        else Console.WriteLine("\n\nWrong password!!!\n\n");
                    }
                    catch { Console.WriteLine("\n\nNo such user!\n\n"); }
                    break;
                case 's':
                    try
                    {
                        data = DataBase[login];
                        Console.WriteLine("\n\nUser already exists!!!\n\n");
                    }
                    catch {
                        Console.Write("Enter password: ");
                        password = Encoding.UTF8.GetBytes(Console.ReadLine());
                        data.Salt = SaltGen();
                        data.Hash = GetHash(password, data.Salt);
                        DataBase.Add(login, data);
                        Console.WriteLine("\n\n\n\nUser created succesfully!\n\n\n\n");
                    }
                    break;
                default:
                    Console.WriteLine("Error! Unrecognized command");
                    return;
            }
        }
    }
}
