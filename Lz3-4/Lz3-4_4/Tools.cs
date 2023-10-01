using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Lz3_4_4
{
    internal static class Tools
    {
        public static string path;
        private static List<string> logins = new List<string>();
        private static List<byte[]> passwords = new List<byte[]>(); 
        public static bool IsUser (string name)
        {
            if (logins.Contains(name)) return true;
            else return false;
        }
        public static bool IsPassword (string name, byte[] password) 
        {
            int i = logins.IndexOf(name);
            if (Enumerable.SequenceEqual(password, passwords[i])) return true;
            else return false;
        }
        public static byte[] HashPassword(string password)
        {
            return SHA256.HashData(Encoding.UTF8.GetBytes(password));
        }
        public static void AddUser(string name, byte[] password)
        {
            logins.Add(name);
            passwords.Add(password);
        }
        public static void SaveAll()
        {
            string[] AllData = new string[logins.Count*2];
            for (int i = 0; i < logins.Count; i++)
            {
                AllData[i*2] = logins[i];
                AllData[i * 2 + 1] = Convert.ToBase64String(passwords[i]);
            }
            File.WriteAllLines(path, AllData);
        }
        public static void ReadData()
        {
            string[] data = File.ReadAllLines(path);
            for (int i = 0; i < data.Length; i+=2)
            {
                logins.Add(data[i]);
                passwords.Add(Convert.FromBase64String(data[i+1]));
            }
        }

    }
}
