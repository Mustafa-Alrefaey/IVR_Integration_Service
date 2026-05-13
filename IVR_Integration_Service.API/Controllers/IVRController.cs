using IVR_Integration_Service.Application.DTOs.Requests;
using IVR_Integration_Service.Application.DTOs.Responses;
using IVR_Integration_Service.Application.Interfaces;
using IVR_Integration_Service.API.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IVR_Integration_Service.API.Controllers
{
    [AllowAnonymous]
    public class IVRController : BaseController
    {
        private readonly IIVRService _ivrService;

        public IVRController(IIVRService ivrService)
        {
            _ivrService = ivrService;
        }

        [HttpPost("Send")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IVROutboundResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Send([FromBody] IVRRequestEnvelopeDto<IVRDataItemDto> request)
        {
            var result = await _ivrService.SendAsync(request);
            return Ok(result);
        }
    }
}
