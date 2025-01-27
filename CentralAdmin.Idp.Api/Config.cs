using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace CentralAdmin.Idp.Api
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
        [
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        ];

        public static IEnumerable<ApiScope> ApiScopes =>
        [
            new ApiScope(name: "CentralAdmin.Api", displayName: "Central Admin Api")
        ];

        public static IEnumerable<Client> Clients(string idpSecret) =>
        [
            new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret(idpSecret.Sha256())
                    },
                    AllowedScopes = { "CentralAdmin.Api" }
                },
            new Client
        {
            ClientId = "web",
            ClientSecrets = { new Secret(idpSecret.Sha256())},

            AllowedGrantTypes = GrantTypes.Code,
            
            // where to redirect to after login
            RedirectUris = { "https://localhost:5002/signin-oidc" },

            // where to redirect to after logout
            PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },

            AllowedScopes =
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile
            }
        }
        ];
    }
}