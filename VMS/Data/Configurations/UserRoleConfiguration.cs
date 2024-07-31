namespace VMS.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using VMS.Models;

    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> entity)
        {
            entity.HasKey(e => e.Id).HasName("pk_user_role");

            entity.ToTable("user_role");

            entity.HasIndex(e => e.CreatedBy, "fk_user_role_created_by");

            entity.HasIndex(e => e.RoleId, "fk_user_role_role_id");

            entity.HasIndex(e => e.UpdatedBy, "fk_user_role_updated_by");

            entity.HasIndex(e => e.UserId, "fk_user_role_user_id");

            entity.Property(e => e.Id).HasColumnName("user_role_id");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_date");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("updated_date");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.UserRoleCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("fk_user_role_created_by");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("fk_user_role_role_id");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.UserRoleUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("fk_user_role_updated_by");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoleUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_user_role_user_id");

        }
    }

}
