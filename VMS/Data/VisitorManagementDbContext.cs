using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;
using VMS.Models;

namespace VMS.Data;

public partial class VisitorManagementDbContext : DbContext
{
    public VisitorManagementDbContext()
    {
    }

    public VisitorManagementDbContext(DbContextOptions<VisitorManagementDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Device> Devices { get; set; }

    public virtual DbSet<OfficeLocation> OfficeLocations { get; set; }

    public virtual DbSet<Page> Pages { get; set; }

    public virtual DbSet<PageControl> PageControls { get; set; }

    public virtual DbSet<PurposeOfVisit> PurposeOfVisits { get; set; }

    public virtual DbSet<Roles> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserDetail> UserDetails { get; set; }

    public virtual DbSet<UserLocation> UserLocations { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<Visitor> Visitors { get; set; }

    public virtual DbSet<VisitorDevice> VisitorDevices { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        /*modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");*/

        modelBuilder.Entity<Device>(entity =>
        {
            entity.HasKey(e => e.DeviceId).HasName("pk_device");

            entity.ToTable("device");

            entity.HasIndex(e => e.CreatedBy, "fk_device_created_by");

            entity.HasIndex(e => e.UpdatedBy, "fk_device_updated_by");

            entity.Property(e => e.DeviceId).HasColumnName("device_id");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_date");
            entity.Property(e => e.DeviceName)
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
        });

        modelBuilder.Entity<OfficeLocation>(entity =>
        {
            entity.HasKey(e => e.OfficeLocationId).HasName("pk_office_location");

            entity.ToTable("office_location");

            entity.HasIndex(e => e.CreatedBy, "fk_office_location_created_by");

            entity.HasIndex(e => e.UpdatedBy, "fk_office_location_updated_by");

            entity.Property(e => e.OfficeLocationId).HasColumnName("office_location_id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_date");
            entity.Property(e => e.LocationName)
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

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.OfficeLocationCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("fk_office_location_created_by");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.OfficeLocationUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("fk_office_location_updated_by");
        });

        modelBuilder.Entity<Page>(entity =>
        {
            entity.HasKey(e => e.PageId).HasName("pk_page");

            entity.ToTable("page");

            entity.HasIndex(e => e.CreatedBy, "fk_page_created_by");

            entity.HasIndex(e => e.UpdatedBy, "fk_page_updated_by");

            entity.Property(e => e.PageId).HasColumnName("page_id");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_date");
            entity.Property(e => e.PageName)
                .HasMaxLength(255)
                .HasColumnName("page_name");
            entity.Property(e => e.PageUrl)
                .HasMaxLength(255)
                .HasColumnName("page_url");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("updated_date");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.PageCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("fk_page_created_by");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.PageUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("fk_page_updated_by");
        });

        modelBuilder.Entity<PageControl>(entity =>
        {
            entity.HasKey(e => e.PageControlId).HasName("pk_page_control");

            entity.ToTable("page_control");

            entity.HasIndex(e => e.CreatedBy, "fk_page_control_created_by");

            entity.HasIndex(e => e.PageId, "fk_page_control_page_id");

            entity.HasIndex(e => e.RoleId, "fk_page_control_role_id");

            entity.HasIndex(e => e.UpdatedBy, "fk_page_control_updated_by");

            entity.Property(e => e.PageControlId).HasColumnName("page_control_id");
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
        });

        modelBuilder.Entity<PurposeOfVisit>(entity =>
        {
            entity.HasKey(e => e.PurposeId).HasName("pk_purpose_of_visit");

            entity.ToTable("purpose_of_visit");

            entity.HasIndex(e => e.CreatedBy, "fk_purpose_of_visit_created_by");

            entity.HasIndex(e => e.UpdatedBy, "fk_purpose_of_visit_updated_by");

            entity.Property(e => e.PurposeId).HasColumnName("purpose_id");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_date");
            entity.Property(e => e.PurposeName)
                .HasMaxLength(255)
                .HasColumnName("purpose_name");
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
        });

        modelBuilder.Entity<Roles>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("pk_role");

            entity.ToTable("role");

            entity.HasIndex(e => e.CreatedBy, "fk_role_created_by");

            entity.HasIndex(e => e.UpdatedBy, "fk_role_updated_by");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_date");
            entity.Property(e => e.RoleName)
                .HasMaxLength(255)
                .HasColumnName("role_name");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("updated_date");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.RoleCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("fk_role_created_by");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.RoleUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("fk_role_updated_by");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("pk_user");

            entity.ToTable("user");

            entity.HasIndex(e => e.CreatedBy, "fk_user_created_by");

            entity.HasIndex(e => e.UpdatedBy, "fk_user_updated_by");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_date");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.IsLoggedIn).HasColumnName("is_logged_in");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("updated_date");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .HasColumnName("username");
            entity.Property(e => e.ValidFrom).HasColumnName("valid_from");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.InverseCreatedByNavigation)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("fk_user_created_by");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.InverseUpdatedByNavigation)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("fk_user_updated_by");
        });

        modelBuilder.Entity<UserDetail>(entity =>
        {
            entity.HasKey(e => e.UserDetailsId).HasName("pk_user_details");

            entity.ToTable("user_details");

            entity.HasIndex(e => e.CreatedBy, "fk_user_details_created_by");

            entity.HasIndex(e => e.OfficeLocationId, "fk_user_details_office_location_id");

            entity.HasIndex(e => e.UpdatedBy, "fk_user_details_updated_by");

            entity.HasIndex(e => e.UserId, "fk_user_details_user_id");

            entity.Property(e => e.UserDetailsId).HasColumnName("user_details_id");
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
        });

        modelBuilder.Entity<UserLocation>(entity =>
        {
            entity.HasKey(e => e.UserLocationId).HasName("pk_user_location");

            entity.ToTable("user_location");

            entity.HasIndex(e => e.CreatedBy, "fk_user_location_created_by");

            entity.HasIndex(e => e.OfficeLocationId, "fk_user_location_office_location_id");

            entity.HasIndex(e => e.UpdatedBy, "fk_user_location_updated_by");

            entity.HasIndex(e => e.UserId, "fk_user_location_user_id");

            entity.Property(e => e.UserLocationId).HasColumnName("user_location_id");
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
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.UserRoleId).HasName("pk_user_role");

            entity.ToTable("user_role");

            entity.HasIndex(e => e.CreatedBy, "fk_user_role_created_by");

            entity.HasIndex(e => e.RoleId, "fk_user_role_role_id");

            entity.HasIndex(e => e.UpdatedBy, "fk_user_role_updated_by");

            entity.HasIndex(e => e.UserId, "fk_user_role_user_id");

            entity.Property(e => e.UserRoleId).HasColumnName("user_role_id");
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
        });

        modelBuilder.Entity<Visitor>(entity =>
        {
            entity.HasKey(e => e.VisitorId).HasName("pk_visitor");

            entity.ToTable("visitor");

            entity.HasIndex(e => e.CreatedBy, "fk_visitor_created_by");

            entity.HasIndex(e => e.OfficeLocationId, "fk_visitor_location_id");

            entity.HasIndex(e => e.PurposeId, "fk_visitor_purpose_id");

            entity.HasIndex(e => e.UpdatedBy, "fk_visitor_updated_by");

            entity.HasIndex(e => e.UserId, "fk_visitor_user_id");

            entity.Property(e => e.VisitorId).HasColumnName("visitor_id");
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
                .HasMaxLength(255)
                .HasColumnName("photo");
            entity.Property(e => e.PurposeId).HasColumnName("purpose_id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("updated_date");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.VisitDate)
                .HasColumnType("timestamp")
                .HasColumnName("visit_date");
            entity.Property(e => e.VisitorName)
                .HasMaxLength(255)
                .HasColumnName("visitor_name");
            entity.Property(e => e.VisitorPassCode)
                .HasMaxLength(255)
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
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_visitor_user_id");
        });

        modelBuilder.Entity<VisitorDevice>(entity =>
        {
            entity.HasKey(e => e.VisitorDeviceId).HasName("pk_visitor_device");

            entity.ToTable("visitor_device");

            entity.HasIndex(e => e.DeviceId, "fk_device_id");

            entity.HasIndex(e => e.CreatedBy, "fk_visitor_device_created_by");

            entity.HasIndex(e => e.UpdatedBy, "fk_visitor_device_updated_by");

            entity.HasIndex(e => e.VisitorId, "fk_visitor_id");

            entity.Property(e => e.VisitorDeviceId).HasColumnName("visitor_device_id");
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
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
