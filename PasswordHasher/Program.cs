using System.Security.Cryptography;
using System.Text;

namespace PasswordHasher
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int keySize = 64;
            const int interations = 350_000;
            HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

            var saltInBytes = RandomNumberGenerator.GetBytes(keySize);

            Console.Write("Enter password: ");
            var password = Console.ReadLine();

            var hashInBytes = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password!),
                saltInBytes,
                interations,
                hashAlgorithm,
                keySize);

            Console.WriteLine();

            var salt = Convert.ToHexString(saltInBytes);
            var hash = Convert.ToHexString(hashInBytes);

            Console.WriteLine($"Salt: \n{salt}");
            Console.WriteLine();
            Console.WriteLine($"Hash: \n{hash}");
        }
    }
}