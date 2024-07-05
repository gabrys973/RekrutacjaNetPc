using IdentityServer4.Models;

namespace Rekrutacja.Server;

public class Config
{
    // klasa służąca do skonfigurowania IdentityServer4 - konfigurowane są rzeczy takie jak scope, klienci, dostęp poszczególnego klienta do danych scope'ów
    public static IEnumerable<IdentityResource> IdentityResources =>
        new[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource
            {
                Name = "role",
                UserClaims = new List<string> { "role" }
            }
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new[] { new ApiScope("RekrutacjaAPI.read"), new ApiScope("RekrutacjaAPI.write"), };

    public static IEnumerable<ApiResource> ApiResources =>
        new[]
        {
            new ApiResource("RekrutacjaAPI")
            {
                Scopes = new List<string> { "RekrutacjaAPI.read", "RekrutacjaAPI.write" },
                ApiSecrets = new List<Secret> { new Secret("ScopeSecret".Sha256()) },
                UserClaims = new List<string> { "role" }
            }
        };

    public static IEnumerable<Client> Clients =>
        new[]
        {
            // m2m client
            new Client
            {
                ClientId = "m2m.client",
                ClientName = "Client Credentials Client",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("ClientSecret".Sha256()) },
                AllowedScopes = { "RekrutacjaAPI.read", "RekrutacjaAPI.write" }
            },
            // interactive client
            new Client
            {
                ClientId = "interactive",
                ClientSecrets = { new Secret("ClientSecret".Sha256()) },
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = { "https://localhost:5444/signin-oidc" },
                FrontChannelLogoutUri = "https://localhost:5444/signout-oidc",
                PostLogoutRedirectUris = { "https://localhost:5444/signout-callback-oidc" },
                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "profile", "RekrutacjaAPI.read" },
                RequirePkce = true,
                RequireConsent = true,
                AllowPlainTextPkce = false
            },
        };
}