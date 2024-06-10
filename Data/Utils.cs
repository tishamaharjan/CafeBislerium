using System.Security.Cryptography;

namespace CafeBislerium.Data
{
    public static class Utils
    {
        private const char _divider = ':';

        //Hashing password
        public static string HashKey(string input)
        {
            var saltSize = 16;
            var loop = 100_000;
            var keySize = 32;
            HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA256;
            byte[] salt = RandomNumberGenerator.GetBytes(saltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(input, salt, loop, hashAlgorithm, keySize);

            return string.Join(
                _divider,
                Convert.ToHexString(hash),
                Convert.ToHexString(salt),
                loop,
                hashAlgorithm
            );
        }

        //Verifying hashed password 
        public static bool VerifyPassword(string input, string hashValue)
        {
            string[] sections = hashValue.Split(_divider);
            byte[] hash = Convert.FromHexString(sections[0]);
            byte[] salt = Convert.FromHexString(sections[1]);
            int looping = int.Parse(sections[2]);
            HashAlgorithmName hashAlgorithm = new(sections[3]);
            byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(
                input,
                salt,
                looping,
                hashAlgorithm,
                hash.Length
            );

            return CryptographicOperations.FixedTimeEquals(inputHash, hash);
        }

        //For getting the app folder
        public static string GetAppFolder()
        {
            return Path.Combine(
                FileSystem.AppDataDirectory,
                "TishaMaharjanCafe"
            );
        }

        //Getting users.json file
        public static string GetUserFile()
        {
            return Path.Combine(GetAppFolder(), "users.json");
        }

        //Getting items.json file
        public static string GetItemFile()
        {
            return Path.Combine(GetAppFolder(), "items.json");
        }

        //Getting customers.json
        public static string GetCustFile()
        {
            return Path.Combine(GetAppFolder(), "customers.json");
        }

        //Getting orders.json file
        public static string GetOrdersFile()
        {
            return Path.Combine(GetAppFolder(), "orders.json");
        }
    }
}