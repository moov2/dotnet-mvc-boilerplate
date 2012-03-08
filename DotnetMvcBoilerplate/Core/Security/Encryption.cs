using System;
using System.Security.Cryptography;
using System.Linq;

namespace DotnetMvcBoilerplate.Core.Security
{
    public class Encryption : IEncryption
    {
        private static int Iterations = 2000;
        private static int ByteLength = 20;

        /// <summary>
        /// Decrypts a password object and then compares to a string to see if they match.
        /// </summary>
        /// <param name="toCompare">Text to compare to see if it matches the decrypted object.</param>
        /// <param name="toDecrypt">Encrypted password to be decrypted and compared.</param>
        /// <returns>True if there is a match, otherwise false.</returns>
        public bool DecryptCompare(string toCompare, Password toDecrypt)
        {
            if (String.IsNullOrEmpty(toCompare)) 
                return false;

            using (var deriveBytes = new Rfc2898DeriveBytes(toCompare, toDecrypt.Salt, Iterations))
            {
                byte[] newKey = deriveBytes.GetBytes(ByteLength);

                return (newKey.SequenceEqual(toDecrypt.Key));
            }
        }

        /// <summary>
        /// Creates an encrypted password from a string.
        /// </summary>
        /// <param name="toEncrypt">Text to be encrypted.</param>
        /// <returns>Encrypted password.</returns>
        public Password Encrypt(string toEncrypt)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(toEncrypt, ByteLength, Iterations))
            {
                byte[] salt = deriveBytes.Salt;
                byte[] key = deriveBytes.GetBytes(ByteLength);

                return new Password(key, salt);
            }
        }
    }

    public interface IEncryption
    {
        bool DecryptCompare(string toCompare, Password toDecrypt);
        Password Encrypt(string toEncrypt);
    }
}