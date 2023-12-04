using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Lz11_12
{
    internal static class Protector
    {
        private static Dictionary<string, User> _users = new Dictionary<string, User>();

        public static byte[] GenerateSalt()
        {
            return RandomNumberGenerator.GetBytes(32);
        }

        public static User Register(string userName, string password, string[] roles = null)
        {
            if (_users.ContainsKey(userName))
            {
                throw new Exception("User already exists!");
            }

            var salt = GenerateSalt();

            var hash = HashPassword(Encoding.Unicode.GetBytes(password), salt, 10000);

            var user = new User
            {
                Login = userName,
                Roles = roles,
                Salt = Convert.ToBase64String(salt),
                PasswordHash = Convert.ToBase64String(hash)
            };

            _users.Add(user.Login, user);

            return user;

        }

        public static bool CheckPassword(string userName, string password)
        {
            if (!_users.ContainsKey(userName))
            {
                return false;
            }

            var user = _users[userName];

            var salt = Convert.FromBase64String(user.Salt);

            var hash = Convert.ToBase64String(HashPassword(Encoding.Unicode.GetBytes(password), salt, 10000));

            return string.Equals(hash, user.PasswordHash);
        }

        public static void LogIn(string userName, string password)
        {
            if (CheckPassword(userName, password))
            {
                var identity = new GenericIdentity(userName);
                var principal = new GenericPrincipal(identity, _users[userName].Roles);

                Thread.CurrentPrincipal = principal;
            }
        }

        public static byte[] HashPassword(byte[] toBeHashed, byte[] salt, int numberOfRounds)
        {
            using (var rfc = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds))
            {
                return rfc.GetBytes(64);
            }
        }
    }
}
