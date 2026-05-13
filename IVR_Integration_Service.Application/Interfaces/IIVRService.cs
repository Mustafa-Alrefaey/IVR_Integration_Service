using IVR_Integration_Service.Application.DTOs.Requests;
using IVR_Integration_Service.Application.DTOs.Responses;

namespace IVR_Integration_Service.Application.Interfaces
{
    public interface IIVRService
    {
        Task<IVROutboundResponseDto> SendAsync(IVRRequestEnvelopeDto<IVRDataItemDto> request);
        Task ProcessCallbackAsync(IVRRequestEnvelopeDto<IVRCallbackItemDto> callback);
    }
}
