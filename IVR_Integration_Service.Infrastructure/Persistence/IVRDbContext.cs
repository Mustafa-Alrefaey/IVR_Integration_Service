using IVR_Integration_Service.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IVR_Integration_Service.Infrastructure.Persistence
{
    public class IVRDbContext : DbContext
    {
        public IVRDbContext(DbContextOptions<IVRDbContext> options) : base(options) { }

        public DbSet<IVRRequest> IVRRequests { get; set; }
        public DbSet<IVRItem>    IVRItems    { get; set; }
        public DbSet<IVRLog>     IVRLogs     { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IVRDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
