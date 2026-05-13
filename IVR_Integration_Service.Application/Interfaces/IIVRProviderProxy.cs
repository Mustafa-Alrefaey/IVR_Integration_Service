using IVR_Integration_Service.Application.DTOs.Requests;
using IVR_Integration_Service.Application.DTOs.Responses;

namespace IVR_Integration_Service.Application.Interfaces
{
    public interface IIVRProviderProxy
    {
        Task<IVRProviderResponseDto> SendAsync(IVRRequestEnvelopeDto<IVRDataItemDto> request);
    }
}
