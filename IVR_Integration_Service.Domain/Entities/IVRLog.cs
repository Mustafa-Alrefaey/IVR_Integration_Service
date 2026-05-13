namespace IVR_Integration_Service.Domain.Entities
{
    public class IVRLog
    {
        public int      Id          { get; set; }
        public int?     EntityId    { get; set; }
        public string   ActionName  { get; set; }
        public string   OldData     { get; set; }
        public string   NewData     { get; set; }
        public string   ServerInfo  { get; set; }
        public string   CreatedBy   { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
