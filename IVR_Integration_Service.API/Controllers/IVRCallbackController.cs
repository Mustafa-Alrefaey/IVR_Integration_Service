using IVR_Integration_Service.Application.DTOs.Requests;
using IVR_Integration_Service.Application.Interfaces;
using IVR_Integration_Service.API.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IVR_Integration_Service.API.Controllers
{
    [AllowAnonymous]
    public class IVRCallbackController : BaseController
    {
        private readonly IIVRService _ivrService;

        public IVRCallbackController(IIVRService ivrService)
        {
            _ivrService = ivrService;
        }

        [HttpPost("Receive")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Receive([FromBody] IVRRequestEnvelopeDto<IVRCallbackItemDto> callback)
        {
            await _ivrService.ProcessCallbackAsync(callback);
            return Ok("Callback processed successfully");
        }
    }
}
