using IdentityModel.Client;

namespace Rekrutacja.Client.Services;

public interface ITokenService
{
    Task<TokenResponse> GetToken(string scope);
}