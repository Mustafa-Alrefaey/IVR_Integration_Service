namespace IVR_Integration_Service.Application.Common.Responses
{
    public class Error
    {
        public int    StatusCode    { get; set; }
        public string ErrorMessage  { get; set; }

        public Error() { }

        public Error(int statusCode, string errorMessage)
        {
            StatusCode   = statusCode;
            ErrorMessage = errorMessage;
        }
    }
}
