using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using System.Text.Json;

namespace CentralAdmin.Api.Providers
{
    public class AmazonSecretsManagerConfigurationProvider : ConfigurationProvider
    {
        private readonly string _region;
        private readonly string _secretName;

        public AmazonSecretsManagerConfigurationProvider()
        {
            _region = "us-east-1";
            _secretName = "dev/testing/secrets_merge";
        }

        public AmazonSecretsManagerConfigurationProvider(string region, string secretName)
        {
            _region = region;
            _secretName = secretName;
        }

        public override void Load()
        {
            try
            {
                var secret = GetSecret();

                Data = JsonSerializer.Deserialize<Dictionary<string, string>>(secret)!;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }

        private string GetSecret()
        {
            IAmazonSecretsManager client = new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName(_region));

            GetSecretValueRequest request = new GetSecretValueRequest
            {
                SecretId = _secretName,
                VersionStage = "AWSCURRENT", // VersionStage defaults to AWSCURRENT if unspecified.
            };

            GetSecretValueResponse response;

            try
            {
                response = client.GetSecretValueAsync(request).Result;
            }
            catch (Exception e)
            {
                // For a list of the exceptions thrown, see
                // https://docs.aws.amazon.com/secretsmanager/latest/apireference/API_GetSecretValue.html
                throw e;
            }

            return response.SecretString;

            // Your code goes here
        }

    }
}
