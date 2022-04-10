using System;
using Microsoft.Extensions.Configuration;

namespace Weather.Api
{
	public static class SecretsConfigurationExtensions
	{
        public static IConfigurationBuilder AddSecretsConfiguration(this IConfigurationBuilder configuration, string secretName)
        {
            configuration.Add(new SecretsConfigurationSource(secretName));
            return configuration;
        }
    }

}

