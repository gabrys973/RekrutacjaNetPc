using IdentityModel.Client;
using Microsoft.Extensions.Options;

namespace Rekrutacja.Client.Services.Identity;

public class TokenService : ITokenService
{
    public readonly IOptions<IdentityServerSettings> _identityServerSettings;
    public readonly DiscoveryDocumentResponse _discoveryDocument;
    private readonly HttpClient _httpClient;
    private readonly ILogger<TokenService> _logger;

    public TokenService(IOptions<IdentityServerSettings> identityServerSettings, ILogger<TokenService> logger)
    {
        _logger = logger;
        _identityServerSettings = identityServerSettings;
        _httpClient = new HttpClient();
        _discoveryDocument = _httpClient.GetDiscoveryDocumentAsync(_identityServerSettings.Value.DiscoveryUrl).Result;

        if(_discoveryDocument.IsError)
            _logger.LogError(_discoveryDocument.Exception, "Unable to get discovery document");
    }

    public async Task<TokenResponse> GetToken(string scope)
    {
        var tokenResponse = await _httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
        {
            Address = _discoveryDocument.TokenEndpoint,
            ClientId = _identityServerSettings.Value.ClientName,
            ClientSecret = _identityServerSettings.Value.ClientPassword,
            Scope = scope
        });

        if(tokenResponse.IsError)
        {
            _logger.LogError(tokenResponse.Exception, "Unable to get token");
            return null;
        }

        return tokenResponse;
    }
}