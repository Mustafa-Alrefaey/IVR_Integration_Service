using IVR_Integration_Service.Application.Common.Responses;

namespace IVR_Integration_Service.Application.Common.Exceptions
{
    public class AppException : Exception
    {
        public int        StatusCode { get; }
        public List<Error> Errors    { get; }

        public AppException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
            Errors     = new List<Error> { new Error(statusCode, message) };
        }

        public AppException(int statusCode, List<Error> errors)
        {
            StatusCode = statusCode;
            Errors     = errors;
        }
    }
}
