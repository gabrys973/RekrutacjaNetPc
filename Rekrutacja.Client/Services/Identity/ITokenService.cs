using IdentityModel.Client;

namespace Rekrutacja.Client.Services.Identity;

public interface ITokenService
{
    Task<TokenResponse> GetToken(string scope);
}