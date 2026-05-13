namespace IVR_Integration_Service.Application.DTOs.Requests
{
    public class IVRCallbackItemDto : IVRDataItemDto
    {
        public string Result        { get; set; }
        public int?   PostponedDays { get; set; }
    }
}
