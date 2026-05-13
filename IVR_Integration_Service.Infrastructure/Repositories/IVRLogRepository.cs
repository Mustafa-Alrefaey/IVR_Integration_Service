using IVR_Integration_Service.Domain.Entities;
using IVR_Integration_Service.Domain.Interfaces;
using IVR_Integration_Service.Infrastructure.Persistence;

namespace IVR_Integration_Service.Infrastructure.Repositories
{
    public class IVRLogRepository : Repository<IVRLog>, IIVRLogRepository
    {
        public IVRLogRepository(IVRDbContext context) : base(context) { }
    }
}
