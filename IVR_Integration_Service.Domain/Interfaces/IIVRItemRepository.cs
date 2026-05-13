using IVR_Integration_Service.Domain.Entities;

namespace IVR_Integration_Service.Domain.Interfaces
{
    public interface IIVRItemRepository : IRepository<IVRItem>
    {
        Task<IVRItem> GetByPhoneAndReferenceAsync(string phoneNumber, string referenceId);
    }
}
