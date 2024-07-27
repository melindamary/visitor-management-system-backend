namespace VMS.Data.Configurations
{
    // Configurations/DeviceConfiguration.cs
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using VMS.Models;

    public class VisitorDeviceConfiguration : IEntityTypeConfiguration<VisitorDevice>
    {
        public void Configure(EntityTypeBuilder<VisitorDevice> entity)
        {

            entity.HasKey(e => e.Id).HasName("pk_visitor_device");

            entity.ToTable("visitor_device");

            entity.HasIndex(e => e.DeviceId, "fk_device_id");

            entity.HasIndex(e => e.CreatedBy, "fk_visitor_device_created_by");

            entity.HasIndex(e => e.UpdatedBy, "fk_visitor_device_updated_by");

            entity.HasIndex(e => e.VisitorId, "fk_visitor_id");

            entity.Property(e => e.Id).HasColumnName("visitor_device_id");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_date");
            entity.Property(e => e.DeviceId).HasColumnName("device_id");
            entity.Property(e => e.SerialNumber)
                .HasMaxLength(255)
                .HasColumnName("serial_number");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("updated_date");
            entity.Property(e => e.VisitorId).HasColumnName("visitor_id");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.VisitorDeviceCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("fk_visitor_device_created_by");

            entity.HasOne(d => d.Device).WithMany(p => p.VisitorDevices)
                .HasForeignKey(d => d.DeviceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_device_id");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.VisitorDeviceUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("fk_visitor_device_updated_by");

            entity.HasOne(d => d.Visitor).WithMany(p => p.VisitorDevices)
                .HasForeignKey(d => d.VisitorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_visitor_id");

        }
    }

}
