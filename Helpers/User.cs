using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace Gero.API.Helpers
{
    public class User
    {
        public static void CreatePassword(string typedPassword, out byte[] hashPasswordSalt, out byte[] hashPassword)
        {
            if (typedPassword == null) throw new ArgumentNullException();

            if (string.IsNullOrEmpty(typedPassword)) throw new ArgumentException();

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                hashPasswordSalt = hmac.Key;
                hashPassword = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(typedPassword));
            }
        }

        public static bool VerifyPassword(string typedPassword, byte[] hashPasswordSalt, byte[] hashPassword)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(hashPasswordSalt))
            {
                var computedPassword = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(typedPassword));

                for (int i = 0; i < computedPassword.Length; i++)
                {
                    if (computedPassword[i] != hashPassword[i]) return false;
                }
            }

            return true;
        }
    }
}
