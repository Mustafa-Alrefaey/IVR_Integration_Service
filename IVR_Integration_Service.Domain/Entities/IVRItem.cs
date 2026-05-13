namespace IVR_Integration_Service.Domain.Entities
{
    public class IVRItem
    {
        public int      Id                  { get; set; }
        public int      IVRRequestId        { get; set; }
        public string   PhoneNumber         { get; set; }
        public string   ReferenceId         { get; set; }
        public string   CallResult          { get; set; }
        public int?     PostponedDays       { get; set; }
        public DateTime? CallbackReceivedAt { get; set; }

        public IVRRequest IVRRequest { get; set; }
    }
}
