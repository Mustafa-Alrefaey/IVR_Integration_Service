namespace IVR_Integration_Service.Application.Common.Responses
{
    public class ApiResponse<T>
    {
        public bool        Ok     => Errors is null;
        public dynamic     Data   { get; set; }
        public List<Error> Errors { get; set; }

        public ApiResponse() { }

        public ApiResponse(T data, List<Error> errors = null)
        {
            Data   = data;
            Errors = errors;
        }
    }
}
