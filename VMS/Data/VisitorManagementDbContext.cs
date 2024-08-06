using Microsoft.EntityFrameworkCore;
using System.Reflection;
using VMS.Models;

namespace VMS.Data;

public partial class VisitorManagementDbContext : DbContext
{
    public VisitorManagementDbContext() {}

    public VisitorManagementDbContext(DbContextOptions<VisitorManagementDbContext> options)
        : base(options){}
    public virtual DbSet<Device> Devices { get; set; }
    public virtual DbSet<OfficeLocation> OfficeLocations { get; set; }
    public virtual DbSet<Page> Pages { get; set; }
    public virtual DbSet<PageControl> PageControls { get; set; }
    public virtual DbSet<PurposeOfVisit> PurposeOfVisits { get; set; }
    public virtual DbSet<Role> Roles { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<UserDetail> UserDetails { get; set; }
    public virtual DbSet<UserLocation> UserLocations { get; set; }
    public virtual DbSet<UserRole> UserRoles { get; set; }
    public virtual DbSet<Visitor> Visitors { get; set; }
    public virtual DbSet<VisitorDevice> VisitorDevices { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // Seed data for Users
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Username = "system", Password = "system" }
        );
        // Seed data for Roles
        modelBuilder.Entity<Role>().HasData(
            new Role { Id = 1, Name = "SuperAdmin" }
        );

        // Seed data for Locations
        modelBuilder.Entity<OfficeLocation>().HasData(
            new OfficeLocation { Id = 1, Name = "Thejaswini", Address = "Technopark Phase 1, Trivandrum", Status = 1},
            new OfficeLocation { Id = 2, Name = "Gayathri", Address = "Technopark Phase 1, Trivandrum", Status = 1 },
            new OfficeLocation { Id = 3, Name = "Athulya", Address = "Infopark, Cochin", Status = 1 }
        );


    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
