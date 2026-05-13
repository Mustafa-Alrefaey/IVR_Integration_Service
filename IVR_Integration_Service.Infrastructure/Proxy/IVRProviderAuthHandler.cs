using Microsoft.Extensions.Configuration;

namespace IVR_Integration_Service.Infrastructure.Proxy
{
    public class IVRProviderAuthHandler : DelegatingHandler
    {
        private readonly IConfiguration _configuration;

        public IVRProviderAuthHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var apiKey = _configuration["IVRProvider:ApiKey"];

            if (!string.IsNullOrEmpty(apiKey))
                request.Headers.Add("X-API-Key", apiKey);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
