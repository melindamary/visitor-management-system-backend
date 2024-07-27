using System;
using System.Collections.Generic;
using System.Reflection;
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
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
