using IVR_Integration_Service.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IVR_Integration_Service.Infrastructure.Configurations
{
    public class IVRItemConfiguration : IEntityTypeConfiguration<IVRItem>
    {
        public void Configure(EntityTypeBuilder<IVRItem> builder)
        {
            builder.ToTable("IVRItems");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(20);
            builder.Property(x => x.ReferenceId).IsRequired().HasMaxLength(100);
            builder.Property(x => x.CallResult).HasMaxLength(20);
        }
    }
}
