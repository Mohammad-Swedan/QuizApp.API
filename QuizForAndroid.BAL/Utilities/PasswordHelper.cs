// File: BLL/Helpers/PasswordHelper.cs

using System;
using System.Security.Cryptography;

namespace eCampus_Prototype.BLL.Helpers
{
    /// <summary>
    /// Provides methods for creating and verifying password hashes using PBKDF2.
    /// </summary>
    public static class PasswordHelper
    {
        // Constants for PBKDF2 configuration
        private const int SaltSize = 16;     // Size of the salt in bytes (128 bits)
        private const int HashSize = 32;     // Size of the hash in bytes (256 bits)
        private const int Iterations = 10000; // Number of iterations (adjust for security/performance)

        /// <summary>
        /// Creates a password hash and salt using PBKDF2 with SHA256.
        /// </summary>
        /// <param name="password">The plaintext password to hash.</param>
        /// <param name="salt">The generated salt.</param>
        /// <param name="hash">The generated password hash.</param>
        public static void CreatePasswordHash(string password, out byte[] salt, out byte[] hash)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be empty or whitespace.", nameof(password));

            // Generate a cryptographic random salt
            using (var rng = RandomNumberGenerator.Create())
            {
                salt = new byte[SaltSize];
                rng.GetBytes(salt);
            }

            // Generate the hash using PBKDF2 with SHA256
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
            {
                hash = pbkdf2.GetBytes(HashSize);
            }
        }

        /// <summary>
        /// Verifies a password against a stored hash and salt.
        /// </summary>
        /// <param name="password">The plaintext password to verify.</param>
        /// <param name="storedHash">The stored password hash.</param>
        /// <param name="storedSalt">The stored salt.</param>
        /// <returns>True if the password matches; otherwise, false.</returns>
        public static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be empty or whitespace.", nameof(password));

            if (storedHash == null || storedHash.Length != HashSize)
                throw new ArgumentException($"Invalid length of password hash (expected {HashSize} bytes).", nameof(storedHash));

            if (storedSalt == null || storedSalt.Length != SaltSize)
                throw new ArgumentException($"Invalid length of password salt (expected {SaltSize} bytes).", nameof(storedSalt));

            // Generate hash from the input password and stored salt
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, storedSalt, Iterations, HashAlgorithmName.SHA256))
            {
                var computedHash = pbkdf2.GetBytes(HashSize);

                // Compare the computed hash with the stored hash securely
                return CryptographicOperations.FixedTimeEquals(computedHash, storedHash);
            }
        }
    }
}
