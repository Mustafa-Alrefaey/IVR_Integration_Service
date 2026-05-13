namespace IVR_Integration_Service.Application.DTOs.Requests
{
    public class IVRRequestEnvelopeDto<TData> where TData : IVRDataItemDto
    {
        public string      Source      { get; set; }
        public string      ServiceCode { get; set; }
        public List<TData> Data        { get; set; }
    }
}
