using IVR_Integration_Service.Application.Common.Exceptions;
using IVR_Integration_Service.Application.Common.Responses;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace IVR_Integration_Service.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        private static readonly JsonSerializerSettings _jsonSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next   = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (AppException ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode  = ex.StatusCode;

                var response = new ApiResponse<object>(null, ex.Errors);
                await context.Response.WriteAsync(JsonConvert.SerializeObject(response, _jsonSettings));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");

                context.Response.ContentType = "application/json";
                context.Response.StatusCode  = 500;

                var response = new ApiResponse<object>(null, new List<Error>
                {
                    new Error(500, ex.InnerException?.Message ?? ex.Message)
                });

                await context.Response.WriteAsync(JsonConvert.SerializeObject(response, _jsonSettings));
            }
        }
    }
}
