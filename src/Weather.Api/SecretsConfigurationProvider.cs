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

        public SecretsConfigurationProvider(string secretName)
        {
            _secretName = secretName;
        }

        public override void Load()
        {
            var client = new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName("us-east-1"));
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

