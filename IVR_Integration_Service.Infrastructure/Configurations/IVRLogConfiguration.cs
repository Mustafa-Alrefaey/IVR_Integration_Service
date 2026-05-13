using IVR_Integration_Service.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IVR_Integration_Service.Infrastructure.Configurations
{
    public class IVRLogConfiguration : IEntityTypeConfiguration<IVRLog>
    {
        public void Configure(EntityTypeBuilder<IVRLog> builder)
        {
            builder.ToTable("IVRLogs");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ActionName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.OldData).HasColumnType("nvarchar(max)");
            builder.Property(x => x.NewData).HasColumnType("nvarchar(max)");
            builder.Property(x => x.ServerInfo).HasMaxLength(50);
            builder.Property(x => x.CreatedBy).HasMaxLength(100);
        }
    }
}
