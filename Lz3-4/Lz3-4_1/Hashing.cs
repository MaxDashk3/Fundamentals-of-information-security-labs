using System;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lz3_4_1
{
    internal class Hashing
    {
        private byte[] data;
        public Hashing(byte[] data)
        {
            this.data = data;
        }
        public byte[] MD5hash()
        {
            using (var md5 = MD5.Create())
            {
                return md5.ComputeHash(data);
            }
        }
        public byte[] SHA1hash()
        {
            using (var sha1 = SHA1.Create())
            {
                return sha1.ComputeHash(data);
            }
        }

        public byte[] SHA256hash()
        {
            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(data);
            }
        }
        public byte[] SHA384hash()
        {
            using (var sha384 = SHA384.Create())
            {
                return sha384.ComputeHash(data);
            }
        }
        public byte[] SHA512hash()
        {
            using (var sha512 = SHA512.Create())
            {
                return sha512.ComputeHash(data);
            }
        }
    }
}
