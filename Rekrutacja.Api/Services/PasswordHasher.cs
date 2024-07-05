using System.Security.Cryptography;

namespace Rekrutacja.Api.Services;

internal class PasswordHasher : IPasswordHasher
{
    private const int SaltSize = 128 / 8;
    private const int KeySize = 256 / 8;
    private const int Iterations = 10000;
    private static readonly HashAlgorithmName _hashAlghoritm = HashAlgorithmName.SHA256;

    // separator, który pomoże nam rozdzielić salt od hash, dzięki czemu nie musimy trzymać dwóch wartości oddzielnie w bazie danych
    private static char Delimiter = ';';

    public string Hash(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(SaltSize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, _hashAlghoritm, KeySize);

        return string.Join(Delimiter, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
    }
}