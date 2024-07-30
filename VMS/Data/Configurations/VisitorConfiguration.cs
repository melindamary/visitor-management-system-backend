namespace VMS.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using VMS.Models;

    public class VisitorConfiguration : IEntityTypeConfiguration<Visitor>
    {
        public void Configure(EntityTypeBuilder<Visitor> entity)
        {
            entity.HasKey(e => e.Id).HasName("pk_visitor");

            entity.ToTable("visitor");

            entity.HasIndex(e => e.CreatedBy, "fk_visitor_created_by");

            entity.HasIndex(e => e.OfficeLocationId, "fk_visitor_location_id");

            entity.HasIndex(e => e.PurposeId, "fk_visitor_purpose_id");

            entity.HasIndex(e => e.UpdatedBy, "fk_visitor_updated_by");

            entity.HasIndex(e => e.StaffId, "fk_visitor_user_id");

            entity.Property(e => e.Id).HasColumnName("visitor_id");
            entity.Property(e => e.CheckInTime)
                .HasColumnType("timestamp")
                .HasColumnName("check_in_time");
            entity.Property(e => e.CheckOutTime)
                .HasColumnType("timestamp")
                .HasColumnName("check_out_time");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_date");
            entity.Property(e => e.HostName)
                .HasMaxLength(255)
                .HasColumnName("host_name");
            entity.Property(e => e.OfficeLocationId).HasColumnName("office_location_id");
            entity.Property(e => e.Phone)
                .HasMaxLength(255)
                .HasColumnName("phone");
            entity.Property(e => e.Photo)
                .HasColumnType("bytea")
                .HasColumnName("photo");
            entity.Property(e => e.PurposeId).HasColumnName("purpose_id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("updated_date");
            entity.Property(e => e.StaffId).HasColumnName("user_id");
            entity.Property(e => e.VisitDate)
                .HasColumnType("timestamp")
                .HasColumnName("visit_date");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("visitor_name");
            entity.Property(e => e.VisitorPassCode)
                .HasColumnName("visitor_pass_code");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.VisitorCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("fk_visitor_created_by");

            entity.HasOne(d => d.OfficeLocation).WithMany(p => p.Visitors)
                .HasForeignKey(d => d.OfficeLocationId)
                .HasConstraintName("fk_visitor_location_id");

            entity.HasOne(d => d.Purpose).WithMany(p => p.Visitors)
                .HasForeignKey(d => d.PurposeId)
                .HasConstraintName("fk_visitor_purpose_id");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.VisitorUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("fk_visitor_updated_by");

            entity.HasOne(d => d.User).WithMany(p => p.VisitorUsers)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("fk_visitor_user_id");

        }
    }

}
