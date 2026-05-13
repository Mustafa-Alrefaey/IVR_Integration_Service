using IVR_Integration_Service.Domain.Entities;
using IVR_Integration_Service.Domain.Interfaces;
using IVR_Integration_Service.Infrastructure.Persistence;

namespace IVR_Integration_Service.Infrastructure.Repositories
{
    public class IVRRequestRepository : Repository<IVRRequest>, IIVRRequestRepository
    {
        public IVRRequestRepository(IVRDbContext context) : base(context) { }
    }
}
