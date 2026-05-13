using IVR_Integration_Service.Domain.Entities;
using IVR_Integration_Service.Domain.Interfaces;
using IVR_Integration_Service.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IVR_Integration_Service.Infrastructure.Repositories
{
    public class IVRItemRepository : Repository<IVRItem>, IIVRItemRepository
    {
        public IVRItemRepository(IVRDbContext context) : base(context) { }

        public async Task<IVRItem> GetByPhoneAndReferenceAsync(string phoneNumber, string referenceId)
            => await _context.IVRItems
                .FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber && x.ReferenceId == referenceId);
    }
}
