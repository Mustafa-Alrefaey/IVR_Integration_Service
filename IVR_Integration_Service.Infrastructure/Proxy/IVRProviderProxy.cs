using IVR_Integration_Service.Application.DTOs.Requests;
using IVR_Integration_Service.Application.DTOs.Responses;
using IVR_Integration_Service.Application.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;

namespace IVR_Integration_Service.Infrastructure.Proxy
{
    public class IVRProviderProxy : IIVRProviderProxy
    {
        private readonly HttpClient               _httpClient;
        private readonly ILogger<IVRProviderProxy> _logger;

        public IVRProviderProxy(HttpClient httpClient, ILogger<IVRProviderProxy> logger)
        {
            _httpClient = httpClient;
            _logger     = logger;
        }

        public async Task<IVRProviderResponseDto> SendAsync(IVRRequestEnvelopeDto<IVRDataItemDto> request)
        {
            if (_httpClient.BaseAddress == null)
            {
                _logger.LogWarning("IVR provider BaseUrl is not configured. Returning mock success for testing.");
                return new IVRProviderResponseDto { IsSuccess = true, Message = "Mock: IVR provider not configured" };
            }

            var json     = JsonConvert.SerializeObject(request);
            var content  = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("", content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                _logger.LogError("IVR provider returned {StatusCode}: {Error}", response.StatusCode, error);
                return new IVRProviderResponseDto { IsSuccess = false, Message = error };
            }

            return new IVRProviderResponseDto { IsSuccess = true, Message = "Accepted" };
        }
    }
}
