namespace VMS.Data.Configurations
{
    // Configurations/DeviceConfiguration.cs
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using VMS.Models;

    public class PurposeOfVisitConfiguration : IEntityTypeConfiguration<PurposeOfVisit>
    {
        public void Configure(EntityTypeBuilder<PurposeOfVisit> entity)
        {
            entity.HasKey(e => e.Id).HasName("pk_purpose_of_visit");

            entity.ToTable("purpose_of_visit");

            entity.HasIndex(e => e.CreatedBy, "fk_purpose_of_visit_created_by");

            entity.HasIndex(e => e.UpdatedBy, "fk_purpose_of_visit_updated_by");

            entity.Property(e => e.Id).HasColumnName("purpose_id");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_date");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("purpose_name");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("updated_date");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.PurposeOfVisitCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("fk_purpose_of_visit_created_by");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.PurposeOfVisitUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("fk_purpose_of_visit_updated_by");

        }
    }

}
