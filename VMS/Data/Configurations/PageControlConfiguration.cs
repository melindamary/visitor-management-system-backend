namespace VMS.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using VMS.Models;

    public class PageControlConfiguration : IEntityTypeConfiguration<PageControl>
    {
        public void Configure(EntityTypeBuilder<PageControl> entity)
        {
            entity.HasKey(e => e.Id).HasName("pk_page_control");

            entity.ToTable("page_control");

            entity.HasIndex(e => e.CreatedBy, "fk_page_control_created_by");

            entity.HasIndex(e => e.PageId, "fk_page_control_page_id");

            entity.HasIndex(e => e.RoleId, "fk_page_control_role_id");

            entity.HasIndex(e => e.UpdatedBy, "fk_page_control_updated_by");

            entity.Property(e => e.Id).HasColumnName("page_control_id");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_date");
            entity.Property(e => e.PageId).HasColumnName("page_id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("updated_date");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.PageControlCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_page_control_created_by");

            entity.HasOne(d => d.Page).WithMany(p => p.PageControls)
                .HasForeignKey(d => d.PageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_page_control_page_id");

            entity.HasOne(d => d.Role).WithMany(p => p.PageControls)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_page_control_role_id");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.PageControlUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_page_control_updated_by");

        }
    }

}
