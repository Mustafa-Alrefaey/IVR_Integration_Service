namespace IVR_Integration_Service.Domain.Entities
{
    public class IVRRequest
    {
        public int    Id               { get; set; }
        public string Source           { get; set; }
        public string ServiceCode      { get; set; }
        public string RequestBody      { get; set; }
        public string ProviderResponse { get; set; }
        public string Status           { get; set; }
        public DateTime CreatedDate    { get; set; }

        public ICollection<IVRItem> Items { get; set; } = new List<IVRItem>();
    }
}
