using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Microsoft.Extensions.Configuration;

namespace Weather.Api
{
    public class SecretsConfigurationProvider : ConfigurationProvider
    {
        private readonly string _secretName;
        private readonly string _region;

        public SecretsConfigurationProvider(string secretName, string region)
        {
            _secretName = secretName;
            _region = region;
        }

        public override void Load()
        {
            var client = new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName(_region));
            var request = new GetSecretValueRequest
            {
                SecretId = _secretName
            };
            var response = client.GetSecretValueAsync(request).Result;
            var data = JsonSerializer.Deserialize<Dictionary<string, string>>(response.SecretString);
            Data = data;
        }
    }
}

