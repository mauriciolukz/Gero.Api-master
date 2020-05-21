using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gero.API.Helpers
{
    public class RandomCode
    {
        public static string Generate(PasswordOptions passwordOptions = null)
        {
            if (passwordOptions == null)
                passwordOptions = new PasswordOptions()
                {
                    RequiredLength = 8,
                    RequiredUniqueChars = 4,
                    RequireDigit = true,
                    RequireLowercase = true,
                    RequireNonAlphanumeric = true,
                    RequireUppercase = true
                };

            string[] randomChars = new[] {
                "ABCDEFGHJKLMNOPQRSTUVWXYZ",
                "abcdefghijkmnopqrstuvwxyz",
                "0123456789",
                "!@$?_-"
            };

            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();
            List<int> indexes = new List<int>();

            if (passwordOptions.RequireUppercase)
            {
                chars.Insert(rand.Next(0, chars.Count), randomChars[0][rand.Next(0, randomChars[0].Length)]);
                indexes.Add(0);
            }
                

            if (passwordOptions.RequireLowercase)
            {
                chars.Insert(rand.Next(0, chars.Count), randomChars[1][rand.Next(0, randomChars[1].Length)]);
                indexes.Add(1);
            }
                

            if (passwordOptions.RequireDigit)
            {
                chars.Insert(rand.Next(0, chars.Count), randomChars[2][rand.Next(0, randomChars[2].Length)]);
                indexes.Add(2);
            }
                

            if (passwordOptions.RequireNonAlphanumeric)
            {
                chars.Insert(rand.Next(0, chars.Count), randomChars[3][rand.Next(0, randomChars[3].Length)]);
                indexes.Add(3);
            }

            for (int i = chars.Count; i < passwordOptions.RequiredLength || chars.Distinct().Count() < passwordOptions.RequiredUniqueChars; i++)
            {
                string rcs = randomChars[indexes[rand.Next(indexes.Count)]];
                chars.Insert(rand.Next(0, chars.Count), rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }
    }
}
