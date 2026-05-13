using IVR_Integration_Service.Application.Common.Responses;

namespace IVR_Integration_Service.Application.Common.Exceptions
{
    public class ValidationException : AppException
    {
        public ValidationException(string message)
            : base(422, message) { }

        public ValidationException(List<Error> errors)
            : base(422, errors) { }
    }
}
