using System.Net.Http.Headers;
using System.Text;

namespace MediLabo.Frontend.Web.Extensions;

public static class HttpClientExtensions
{
    extension(IServiceCollection services)
    {
        public IHttpClientBuilder AddGatewayHttpClient<TInterface, TImplementation>(IConfiguration configuration)
            where TInterface : class
            where TImplementation : class, TInterface
        {
            return services.AddHttpClient<TInterface, TImplementation>(client =>
            {
                var gatewayConfig = configuration.GetSection("Gateway");
                client.BaseAddress = new Uri(gatewayConfig["BaseUrl"]!);
                var username = gatewayConfig["Username"]!;
                var password = gatewayConfig["Password"]!;
                var authString = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authString);
            });
        }
    }
}