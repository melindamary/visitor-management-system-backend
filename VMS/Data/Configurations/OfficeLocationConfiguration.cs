namespace VMS.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using VMS.Models;

    public class OfficeLocationConfiguration : IEntityTypeConfiguration<OfficeLocation>
    {
        public void Configure(EntityTypeBuilder<OfficeLocation> entity)
        {
            entity.HasKey(e => e.Id).HasName("pk_office_location");

            entity.ToTable("office_location");

            entity.HasIndex(e => e.CreatedBy, "fk_office_location_created_by");

            entity.HasIndex(e => e.UpdatedBy, "fk_office_location_updated_by");

            entity.Property(e => e.Id).HasColumnName("office_location_id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_date");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("location_name");
            entity.Property(e => e.Phone)
                .HasMaxLength(255)
                .HasColumnName("phone");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("updated_date");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.OfficeLocationCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("fk_office_location_created_by");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.OfficeLocationUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("fk_office_location_updated_by");

        }
    }

}
