using IVR_Integration_Service.Application.DTOs.Requests;
using IVR_Integration_Service.Application.DTOs.Responses;
using IVR_Integration_Service.Application.Interfaces;
using IVR_Integration_Service.Domain.Entities;
using IVR_Integration_Service.Domain.Enums;
using IVR_Integration_Service.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace IVR_Integration_Service.Application.Services
{
    public class IVRService : IIVRService
    {
        private readonly IIVRRequestRepository _requestRepository;
        private readonly IIVRItemRepository    _itemRepository;
        private readonly IIVRLogRepository     _logRepository;
        private readonly IIVRProviderProxy     _providerProxy;
        private readonly ILogger<IVRService>   _logger;

        private static readonly JsonSerializerSettings _jsonSettings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            ContractResolver      = new CamelCasePropertyNamesContractResolver()
        };

        public IVRService(
            IIVRRequestRepository requestRepository,
            IIVRItemRepository    itemRepository,
            IIVRLogRepository     logRepository,
            IIVRProviderProxy     providerProxy,
            ILogger<IVRService>   logger)
        {
            _requestRepository = requestRepository;
            _itemRepository    = itemRepository;
            _logRepository     = logRepository;
            _providerProxy     = providerProxy;
            _logger            = logger;
        }

        public async Task<IVROutboundResponseDto> SendAsync(IVRRequestEnvelopeDto<IVRDataItemDto> request)
        {
            var ivrRequest = new IVRRequest
            {
                Source      = request.Source,
                ServiceCode = request.ServiceCode,
                RequestBody = JsonConvert.SerializeObject(request, _jsonSettings),
                Status      = IVRRequestStatus.SENT,
                CreatedDate = DateTime.UtcNow,
                Items       = request.Data.Select(d => new IVRItem
                {
                    PhoneNumber = d.PhoneNumber,
                    ReferenceId = d.ReferenceId,
                    CallResult  = IVRCallResult.PENDING
                }).ToList()
            };

            var providerResponse = await _providerProxy.SendAsync(request);
            ivrRequest.ProviderResponse = JsonConvert.SerializeObject(providerResponse, _jsonSettings);

            if (!providerResponse.IsSuccess)
            {
                ivrRequest.Status = IVRRequestStatus.FAILED;
                _logger.LogWarning("IVR provider rejected request. Source: {Source}, ServiceCode: {ServiceCode}", request.Source, request.ServiceCode);
            }

            var saved = await _requestRepository.AddAsync(ivrRequest);

            await _logRepository.AddAsync(new IVRLog
            {
                EntityId    = saved.Id,
                ActionName  = "SEND_IVR_REQUEST",
                OldData     = "{}",
                NewData     = JsonConvert.SerializeObject(ivrRequest, _jsonSettings),
                CreatedDate = DateTime.UtcNow
            });

            return new IVROutboundResponseDto
            {
                RequestId = saved.Id,
                Status    = saved.Status,
                Message   = providerResponse.IsSuccess ? "Request sent successfully" : "Request failed"
            };
        }

        public async Task ProcessCallbackAsync(IVRRequestEnvelopeDto<IVRCallbackItemDto> callback)
        {
            foreach (var callbackItem in callback.Data)
            {
                var item = await _itemRepository.GetByPhoneAndReferenceAsync(callbackItem.PhoneNumber, callbackItem.ReferenceId);

                if (item == null)
                {
                    _logger.LogWarning("Callback received for unknown item. Phone: {Phone}, ReferenceId: {RefId}", callbackItem.PhoneNumber, callbackItem.ReferenceId);
                    continue;
                }

                var oldData = JsonConvert.SerializeObject(item, _jsonSettings);

                item.CallResult          = callbackItem.Result;
                item.PostponedDays       = callbackItem.PostponedDays;
                item.CallbackReceivedAt  = DateTime.UtcNow;

                await _itemRepository.UpdateAsync(item);

                await _logRepository.AddAsync(new IVRLog
                {
                    EntityId    = item.Id,
                    ActionName  = "RECEIVE_IVR_CALLBACK",
                    OldData     = oldData,
                    NewData     = JsonConvert.SerializeObject(item, _jsonSettings),
                    CreatedDate = DateTime.UtcNow
                });
            }
        }
    }
}
