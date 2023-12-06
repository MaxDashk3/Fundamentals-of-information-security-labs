using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Lz13_14
{
    internal static class Protector
    {
        static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        private static Dictionary<string, User> _users = new Dictionary<string, User>();

        public static byte[] GenerateSalt()
        {
            Logger.Trace("Generating salt...");
            return RandomNumberGenerator.GetBytes(32);
        }

        public static User Register(string userName, string password, string[] roles = null)
        {
            Logger.Trace("Adding user...");
            string rolesDebug = "";
            if (roles != null)
            foreach (string role in roles)
            {
                rolesDebug = rolesDebug + role;
            }

            Logger.Debug("Username: "+userName+"; Password: "+password+"; Roles: "+ rolesDebug);
            if (_users.ContainsKey(userName))
            {
                var e = new Exception("User already exists!");
                Logger.Warn("Wrong username used!!!", e);
                throw e;
            }

            var salt = GenerateSalt();
            Logger.Debug("Current salt: " + Convert.ToBase64String(salt));

            var hash = HashPassword(Encoding.Unicode.GetBytes(password), salt, 10000);
            Logger.Debug("Current hash: " + Convert.ToBase64String(hash));

            var user = new User
            {
                Login = userName,
                Roles = roles,
                Salt = Convert.ToBase64String(salt),
                PasswordHash = Convert.ToBase64String(hash)
            };

            _users.Add(user.Login, user);
            Logger.Info("Added new user\n");

            return user;

        }

        public static bool CheckPassword(string userName, string password)
        {
            Logger.Trace("Checking password...");
            if (!_users.ContainsKey(userName))
            {
                Logger.Warn($"No such user found: \"{userName}\"!!!");
                return false;
            }

            var user = _users[userName];

            var salt = Convert.FromBase64String(user.Salt);
            Logger.Debug("Current salt: "+user.Salt);

            var hash = Convert.ToBase64String(HashPassword(Encoding.Unicode.GetBytes(password), salt, 10000));
            Logger.Debug("Current hash: "+hash);

            return string.Equals(hash, user.PasswordHash);
        }

        public static void LogIn(string userName, string password)
        {
            Logger.Trace("Logging in...");
            Logger.Debug("Username: " + userName + "; Password: " + password);
            if (CheckPassword(userName, password))
            {
                var identity = new GenericIdentity(userName);
                var principal = new GenericPrincipal(identity, _users[userName].Roles);
                Logger.Trace("Login sucessful, setting current principal...");

                Thread.CurrentPrincipal = principal;
            }
            else Logger.Warn($"Wrong password used: \"{password}\"!!!");
        }

        public static byte[] HashPassword(byte[] toBeHashed, byte[] salt, int numberOfRounds)
        {
            using (var rfc = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds))
            {
                Logger.Trace("Hashing password...");
                return rfc.GetBytes(64);
            }
        }
    }
}
