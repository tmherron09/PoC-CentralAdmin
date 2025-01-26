using CentralAdmin.Api.Providers;

namespace CentralAdmin.Api.Extensions
{
    public static class ConfigurationBuilderExtensions
    {
        public static void AddAmazonSecretsManager(this IConfigurationBuilder configurationBuilder,
            string region,
            string secretName)
        {
            var configurationSource =
                new AmazonSecretsManagerConfigurationSource(region, secretName);

            configurationBuilder.Add(configurationSource);
        }

        public static IConfigurationBuilder AddAmazonSecretsManager(this IConfigurationBuilder configurationBuilder)
        {
            var configurationSource =
                new AmazonSecretsManagerConfigurationSource();

            configurationBuilder.Add(configurationSource);

            return configurationBuilder;
        }
    }
}
