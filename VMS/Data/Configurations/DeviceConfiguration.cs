namespace VMS.Data.Configurations
{
    // Configurations/DeviceConfiguration.cs
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using VMS.Models;

    public class DeviceConfiguration : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> entity)
        {
            entity.HasKey(e => e.Id).HasName("pk_device");

            entity.ToTable("device");

            entity.HasIndex(e => e.CreatedBy, "fk_device_created_by");

            entity.HasIndex(e => e.UpdatedBy, "fk_device_updated_by");

            entity.Property(e => e.Id).HasColumnName("device_id");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_date");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("device_name");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("updated_date");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.DeviceCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("fk_device_created_by");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.DeviceUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("fk_device_updated_by");

        }
    }

}
