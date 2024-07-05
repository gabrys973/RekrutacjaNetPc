namespace Rekrutacja.Api.Services;

public interface IPasswordHasher
{
    string Hash(string password);
}