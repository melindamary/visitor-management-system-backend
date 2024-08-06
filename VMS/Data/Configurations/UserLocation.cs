namespace VMS.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using VMS.Models;

    public class UserLocationConfiguration : IEntityTypeConfiguration<UserLocation>
    {
        public void Configure(EntityTypeBuilder<UserLocation> entity)
        {
            entity.HasKey(e => e.Id).HasName("pk_user_location");

            entity.ToTable("user_location");

            entity.HasIndex(e => e.CreatedBy, "fk_user_location_created_by");

            entity.HasIndex(e => e.OfficeLocationId, "fk_user_location_office_location_id");

            entity.HasIndex(e => e.UpdatedBy, "fk_user_location_updated_by");

            entity.HasIndex(e => e.UserId, "fk_user_location_user_id");

            entity.Property(e => e.Id)
                .HasColumnName("user_location_id")
                .ValueGeneratedOnAdd();
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_date");
            entity.Property(e => e.OfficeLocationId).HasColumnName("office_location_id");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("updated_date");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.UserLocationCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("fk_user_location_created_by");

            entity.HasOne(d => d.OfficeLocation).WithMany(p => p.UserLocations)
                .HasForeignKey(d => d.OfficeLocationId)
                .HasConstraintName("fk_user_location_office_location_id");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.UserLocationUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("fk_user_location_updated_by");

            entity.HasOne(d => d.User).WithMany(p => p.UserLocationUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_user_location_user_id");

        }
    }

}
