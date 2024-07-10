namespace Rekrutacja.Api.Services.PasswordsHasher;

public interface IPasswordHasher
{
    string Hash(string password);
}