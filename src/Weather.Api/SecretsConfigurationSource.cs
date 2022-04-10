using Microsoft.Extensions.Configuration;

namespace Weather.Api
{
	public class SecretsConfigurationSource : IConfigurationSource
    {
        public string SecretName { get; set;  }

        public SecretsConfigurationSource(string secretName)
        {
            SecretName = secretName;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new SecretsConfigurationProvider(SecretName);
        }
    }
}

