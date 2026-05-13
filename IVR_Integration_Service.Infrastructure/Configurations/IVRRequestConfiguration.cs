using IVR_Integration_Service.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IVR_Integration_Service.Infrastructure.Configurations
{
    public class IVRRequestConfiguration : IEntityTypeConfiguration<IVRRequest>
    {
        public void Configure(EntityTypeBuilder<IVRRequest> builder)
        {
            builder.ToTable("IVRRequests");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Source).IsRequired().HasMaxLength(50);
            builder.Property(x => x.ServiceCode).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Status).IsRequired().HasMaxLength(20);
            builder.Property(x => x.RequestBody).HasColumnType("nvarchar(max)");
            builder.Property(x => x.ProviderResponse).HasColumnType("nvarchar(max)");
            builder.HasMany(x => x.Items).WithOne(x => x.IVRRequest).HasForeignKey(x => x.IVRRequestId);
        }
    }
}
