using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace Weather.Api;

public class LambdaEntryPoint : Amazon.Lambda.AspNetCoreServer.APIGatewayHttpApiV2ProxyFunction
{
    protected override void Init(IWebHostBuilder builder)
    {
        builder.UseStartup<Startup>();
    }

    protected override void Init(IHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((hostingContext, config) =>
        {
            var configuration = config.Build();
            var acceptedSecret = configuration["Secrets:ConnectionStringSecretName"];
            config.AddSecretsManager(configurator: options =>
            {
                options.SecretFilter = entry => entry.ARN.Contains(acceptedSecret);
            });
        });
    }
}