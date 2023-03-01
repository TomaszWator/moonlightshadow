using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Extension.Security
{
    enum Option
    {
        number,
        bigLetter,
        smallLetter,
    };

    public static class Hasher
    {
        public static string GetRandomAlphaNumericString(int lenght)
        {
            StringBuilder randomString = new StringBuilder(lenght);

            char randomCharacter = ' ';
            Option option;

            for (int i = 0; i < lenght; i++)
            {
                option = (Option)RandomNumberGenerator.GetInt32(3);

                if (option == Option.number)
                {
                    randomCharacter = (char)(RandomNumberGenerator.GetInt32(58 - 48) + 48);
                }

                if (option == Option.bigLetter)
                {
                    randomCharacter = (char)(RandomNumberGenerator.GetInt32(91 - 65) + 65);
                }

                if (option == Option.smallLetter)
                {
                    randomCharacter = (char)(RandomNumberGenerator.GetInt32(123 - 97) + 97);
                }

                randomString.Append(randomCharacter);
            }

            return randomString.ToString();
        }

        public static string GetRandomString(int lenght)
        {
            StringBuilder randomString = new StringBuilder(lenght);

            for (int i = 0; i < lenght; i++)
            {
                var randomCharacter = (char)(RandomNumberGenerator.GetInt32(char.MaxValue));

                randomString.Append(randomCharacter);
            }

            return randomString.ToString();
        }

        public static (string hash, byte[] salt) Encrypt(string password)
        {
            var salt = Hasher.GetHash(GetRandomString(16)).Take(16).ToArray();

            string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 1000,
                numBytesRequested: 256 / 8));

            return (hash, salt);
        }

        public static (string hash, byte[] salt) Encrypt(string password, byte[] salt)
        {
            string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 1000,
                numBytesRequested: 256 / 8));

            return (hash, salt);
        }

        public static byte[] GetHash(string inputString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public static string GetToken()
        {
            var randomString = GetRandomString(16);

            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(randomString))
                sb.Append(b.ToString("X2"));

            return sb.ToString().Substring(0, 30).ToLower();
        }
    }
}