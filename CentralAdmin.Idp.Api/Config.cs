using Duende.IdentityServer.Models;

namespace CentralAdmin.Idp.Api
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
        [
            new IdentityResources.OpenId()
        ];

        public static IEnumerable<ApiScope> ApiScopes =>
        [
            new ApiScope(name: "CentralAdmin.Api", displayName: "Central Admin Api")
        ];

        public static IEnumerable<Client> Clients =>
        [
            new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        // Insert Secret Here
                    },
                    AllowedScopes = { "CentralAdmin.Api" }
                }
        ];
    }
}