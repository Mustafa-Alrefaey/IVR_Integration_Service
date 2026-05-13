using IVR_Integration_Service.Application.Common.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace IVR_Integration_Service.API.Controllers.Base
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                    .Where(e => e.Value.Errors.Any())
                    .SelectMany(e => e.Value.Errors.Select(err => new Error(400, err.ErrorMessage)))
                    .ToList();

                throw new Application.Common.Exceptions.ValidationException(errors);
            }
        }

        protected IActionResult Ok<TData>(TData data)
            => base.Ok(new ApiResponse<TData>(data));

        protected IActionResult Ok(string message = null)
            => base.Ok(new ApiResponse<string>(message));
    }
}
