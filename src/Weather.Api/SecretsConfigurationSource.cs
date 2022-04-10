using Microsoft.Extensions.Configuration;

namespace Weather.Api
{
    public class SecretsConfigurationSource : IConfigurationSource
    {
        public string SecretName { get; set; }
        public string Region { get; set; }

        public SecretsConfigurationSource(string secretName, string region)
        {
            SecretName = secretName;
            Region = region;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new SecretsConfigurationProvider(SecretName, Region);
        }
    }
}

