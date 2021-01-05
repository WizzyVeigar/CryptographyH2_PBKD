using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace CryptographyH2_PBKD
{
    class HashData
    {
        private const int keySize = 32;

        public static byte[] HashMyPassword(byte[] psw, byte[] salt, int iterations)
        {
            using (Rfc2898DeriveBytes hasher = new Rfc2898DeriveBytes(psw, salt, iterations))
            {
                return hasher.GetBytes(keySize);
            }
        }

        public static byte[] GenerateSalt()
        {
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                var randomNumber = new byte[keySize];
                rng.GetBytes(randomNumber);

                return randomNumber;
            }
        }


        public static bool Login(User dbUser, byte[] pass)
        {
            byte[] dbHash = Convert.FromBase64String(dbUser.UPass);
            byte[] uHash = HashMyPassword(pass, dbUser.USalt, 50000);

            if (CompareByteArrays(dbHash,uHash, dbHash.Length))
            {
                return true;
            }
            return false;
        }

        private static bool CompareByteArrays(byte[] a, byte[] b, int len)
        {
            for (int i = 0; i < len; i++)
            {
                if (a[i] != b[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
