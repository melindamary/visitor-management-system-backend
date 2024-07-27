namespace VMS.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using VMS.Models;

    public class UserDetailConfiguration : IEntityTypeConfiguration<UserDetail>
    {
        public void Configure(EntityTypeBuilder<UserDetail> entity)
        {

            entity.HasKey(e => e.Id).HasName("pk_user_details");

            entity.ToTable("user_details");

            entity.HasIndex(e => e.CreatedBy, "fk_user_details_created_by");

            entity.HasIndex(e => e.OfficeLocationId, "fk_user_details_office_location_id");

            entity.HasIndex(e => e.UpdatedBy, "fk_user_details_updated_by");

            entity.HasIndex(e => e.UserId, "fk_user_details_user_id");

            entity.Property(e => e.Id).HasColumnName("user_details_id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_date");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .HasColumnName("last_name");
            entity.Property(e => e.OfficeLocationId).HasColumnName("office_location_id");
            entity.Property(e => e.Phone)
                .HasMaxLength(255)
                .HasColumnName("phone");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("updated_date");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.UserDetailCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("fk_user_details_created_by");

            entity.HasOne(d => d.OfficeLocation).WithMany(p => p.UserDetails)
                .HasForeignKey(d => d.OfficeLocationId)
                .HasConstraintName("fk_user_details_office_location_id");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.UserDetailUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("fk_user_details_updated_by");

            entity.HasOne(d => d.User).WithMany(p => p.UserDetailUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_user_details_user_id");

        }
    }

}
