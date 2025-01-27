using System.Net.Http.Headers;
using System.Text.Json;
using IdentityModel.Client;

namespace CentralAdmin.Cli
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Press Any Key to Start...");
            Console.ReadKey();

            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");
            if (disco.IsError)
            {
                Console.WriteLine("Discovery Error");
                Console.WriteLine(disco.Error);
                Console.ReadKey();
                return;
            }

            var tokenRequest = new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "client",
                //ClientSecret = ,
                Scope = "CentralAdmin.Api"
            };
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(tokenRequest);

            if (tokenResponse.IsError)
            {
                Console.WriteLine("Token Response Error");

                Console.WriteLine(tokenResponse.Error);
                Console.WriteLine(tokenResponse.ErrorDescription);
                Console.ReadKey();
                return;
            }

            var token = tokenResponse.AccessToken!;

            var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken!);
            
            var response = await apiClient.GetAsync("https://localhost:6001/identity");
            if(!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Client Api Response Error");

                Console.WriteLine(response.StatusCode);
                Console.ReadKey();
                return;
            }
            else
            {
                Console.WriteLine("Client Api Response Success");
                var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync()).RootElement;
                Console.WriteLine(JsonSerializer.Serialize(doc, new JsonSerializerOptions
                {
                    WriteIndented = true
                }));
            }

            Console.WriteLine("Finished Idp Test...");
            Console.ReadKey();
        }
    }
}
