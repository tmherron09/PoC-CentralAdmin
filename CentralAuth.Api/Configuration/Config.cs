using Duende.IdentityServer.Models;
using System.Collections.Generic;

namespace CentralAuth.Api.Configuration
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("CentralAdmin", "Central Admin"),
                new ApiScope("scope2")
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "test.client",
                    ClientName = "Client Credentials Test Client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    ClientSecrets =
                        {
                            new Secret("872F2AE5-8A07-4E7A-AF96-175795623118".Sha256())
                        },

                    AllowedScopes = { "scope2" },
                },
                new Client
                {
                    ClientId = "central-admin-ui",
                    ClientName = "Central Admin UI",
                    AllowedGrantTypes = GrantTypes.Code,


                    //RedirectUris = { "https://localhost:4200/auth-callback"},
                    RedirectUris = { "https://localhost:4200/signin-oidc"},
                    FrontChannelLogoutUri = "https://localhost:4200/signout-oidc",
                    PostLogoutRedirectUris = { "https://localhost:4200/signout-callback-oidc" },
                    //AllowedCorsOrigins = { "https://localhost:4200" },
                    AllowedScopes = { "openid", "profile", "CentralAdmin", "scope2" },
                    //AllowAccessTokensViaBrowser = true
                    AllowOfflineAccess = true,
                }
            };

    }
}
