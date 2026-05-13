namespace IVR_Integration_Service.Application.Common.Exceptions
{
    public class NotFoundException : AppException
    {
        public NotFoundException(string message)
            : base(404, message) { }
    }
}
