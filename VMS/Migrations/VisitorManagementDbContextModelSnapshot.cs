﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VMS.Data;

#nullable disable

namespace VMS.Migrations
{
    [DbContext(typeof(VisitorManagementDbContext))]
    partial class VisitorManagementDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("utf8mb4_0900_ai_ci")
                .HasAnnotation("MySql:CharSet", "utf8mb4")
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("VMS.Models.Device", b =>
                {
                    b.Property<int>("DeviceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("device_id");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("created_date");

                    b.Property<string>("DeviceName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("device_name");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int")
                        .HasColumnName("updated_by");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("updated_date");

                    b.HasKey("DeviceId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "CreatedBy" }, "fk_device_created_by");

                    b.HasIndex(new[] { "UpdatedBy" }, "fk_device_updated_by");

                    b.ToTable("device", (string)null);
                });

            modelBuilder.Entity("VMS.Models.OfficeLocation", b =>
                {
                    b.Property<int>("LocationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("location_id");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("address");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("created_date");

                    b.Property<string>("LocationName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("location_name");

                    b.Property<string>("Phone")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("phone");

                    b.Property<int>("UpdatedBy")
                        .HasColumnType("int")
                        .HasColumnName("updated_by");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("updated_date");

                    b.HasKey("LocationId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "CreatedBy" }, "fk_office_location_created_by");

                    b.HasIndex(new[] { "UpdatedBy" }, "fk_office_location_updated_by");

                    b.ToTable("office_location", (string)null);
                });

            modelBuilder.Entity("VMS.Models.Page", b =>
                {
                    b.Property<int>("PageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("page_id");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("created_date");

                    b.Property<string>("PageName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("page_name");

                    b.Property<string>("PageUrl")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("page_url");

                    b.Property<int>("UpdatedBy")
                        .HasColumnType("int")
                        .HasColumnName("updated_by");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("updated_date");

                    b.HasKey("PageId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "CreatedBy" }, "fk_page_created_by");

                    b.HasIndex(new[] { "UpdatedBy" }, "fk_page_updated_by");

                    b.ToTable("page", (string)null);
                });

            modelBuilder.Entity("VMS.Models.PageControl", b =>
                {
                    b.Property<int>("PageControlId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("page_control_id");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("created_date");

                    b.Property<int>("PageId")
                        .HasColumnType("int")
                        .HasColumnName("page_id");

                    b.Property<int>("RoleId")
                        .HasColumnType("int")
                        .HasColumnName("role_id");

                    b.Property<int>("UpdatedBy")
                        .HasColumnType("int")
                        .HasColumnName("updated_by");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("updated_date");

                    b.HasKey("PageControlId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "CreatedBy" }, "fk_page_control_created_by");

                    b.HasIndex(new[] { "PageId" }, "fk_page_control_page_id");

                    b.HasIndex(new[] { "RoleId" }, "fk_page_control_role_id");

                    b.HasIndex(new[] { "UpdatedBy" }, "fk_page_control_updated_by");

                    b.ToTable("page_control", (string)null);
                });

            modelBuilder.Entity("VMS.Models.PurposeOfVisit", b =>
                {
                    b.Property<int>("PurposeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("purpose_id");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("created_date");

                    b.Property<string>("PurposeName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("purpose_name");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int")
                        .HasColumnName("updated_by");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("updated_date");

                    b.HasKey("PurposeId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "CreatedBy" }, "fk_purpose_of_visit_created_by");

                    b.HasIndex(new[] { "UpdatedBy" }, "fk_purpose_of_visit_updated_by");

                    b.ToTable("purpose_of_visit", (string)null);
                });

            modelBuilder.Entity("VMS.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("role_id");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("created_date");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("role_name");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int")
                        .HasColumnName("updated_by");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("updated_date");

                    b.HasKey("RoleId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "CreatedBy" }, "fk_role_created_by");

                    b.HasIndex(new[] { "UpdatedBy" }, "fk_role_updated_by");

                    b.ToTable("role", (string)null);
                });

            modelBuilder.Entity("VMS.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("created_date");

                    b.Property<int>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("is_active")
                        .HasDefaultValueSql("'1'");

                    b.Property<int>("IsLoggedIn")
                        .HasColumnType("int")
                        .HasColumnName("is_logged_in");

                    b.Property<int>("LocationId")
                        .HasColumnType("int")
                        .HasColumnName("location_id");

                    b.Property<string>("Password")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("password");

                    b.Property<int>("RoleId")
                        .HasColumnType("int")
                        .HasColumnName("role_id");

                    b.Property<int>("UpdatedBy")
                        .HasColumnType("int")
                        .HasColumnName("updated_by");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("updated_date");

                    b.Property<int>("UserDetailsId")
                        .HasColumnType("int")
                        .HasColumnName("user_details_id");

                    b.Property<string>("Username")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("username");

                    b.Property<DateOnly>("ValidFrom")
                        .HasColumnType("date")
                        .HasColumnName("valid_from");

                    b.HasKey("UserId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "CreatedBy" }, "fk_user_created_by");

                    b.HasIndex(new[] { "LocationId" }, "fk_user_location_id");

                    b.HasIndex(new[] { "RoleId" }, "fk_user_role_id");

                    b.HasIndex(new[] { "UpdatedBy" }, "fk_user_updated_by");

                    b.HasIndex(new[] { "UserDetailsId" }, "fk_user_user_details_id");

                    b.ToTable("user", (string)null);
                });

            modelBuilder.Entity("VMS.Models.UserDetail", b =>
                {
                    b.Property<int>("UserDetailsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("user_details_id");

                    b.Property<string>("Address")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("address");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("created_date");

                    b.Property<string>("Email")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("last_name");

                    b.Property<string>("Phone")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("phone");

                    b.Property<int>("UpdatedBy")
                        .HasColumnType("int")
                        .HasColumnName("updated_by");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("updated_date");

                    b.HasKey("UserDetailsId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "CreatedBy" }, "fk_user_details_created_by");

                    b.HasIndex(new[] { "UpdatedBy" }, "fk_user_details_updated_by");

                    b.ToTable("user_details", (string)null);
                });

            modelBuilder.Entity("VMS.Models.Visitor", b =>
                {
                    b.Property<int>("VisitorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("visitor_id");

                    b.Property<DateTime?>("CheckInTime")
                        .HasColumnType("datetime")
                        .HasColumnName("check_in_time");

                    b.Property<DateTime?>("CheckOutTime")
                        .HasColumnType("datetime")
                        .HasColumnName("check_out_time");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("created_date");

                    b.Property<string>("HostName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("host_name");

                    b.Property<int>("LocationId")
                        .HasColumnType("int")
                        .HasColumnName("location_id");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("phone");

                    b.Property<string>("Photo")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("photo");

                    b.Property<int>("PurposeId")
                        .HasColumnType("int")
                        .HasColumnName("purpose_id");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("status");

                    b.Property<int>("UpdatedBy")
                        .HasColumnType("int")
                        .HasColumnName("updated_by");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("updated_date");

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.Property<DateOnly>("VisitDate")
                        .HasColumnType("date")
                        .HasColumnName("visit_date");

                    b.Property<string>("VisitorName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("visitor_name");

                    b.Property<string>("VisitorPassCode")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("visitor_pass_code");

                    b.HasKey("VisitorId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "CreatedBy" }, "fk_visitor_created_by");

                    b.HasIndex(new[] { "LocationId" }, "fk_visitor_location_id");

                    b.HasIndex(new[] { "PurposeId" }, "fk_visitor_purpose_id");

                    b.HasIndex(new[] { "UpdatedBy" }, "fk_visitor_updated_by");

                    b.HasIndex(new[] { "UserId" }, "fk_visitor_user_id");

                    b.ToTable("visitor", (string)null);
                });

            modelBuilder.Entity("VMS.Models.VisitorDevice", b =>
                {
                    b.Property<int>("VisitorDeviceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("visitor_device_id");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("created_date");

                    b.Property<int>("DeviceId")
                        .HasColumnType("int")
                        .HasColumnName("device_id");

                    b.Property<string>("SerialNumber")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("serial_number");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int")
                        .HasColumnName("updated_by");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("updated_date");

                    b.Property<int>("VisitorId")
                        .HasColumnType("int")
                        .HasColumnName("visitor_id");

                    b.HasKey("VisitorDeviceId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "DeviceId" }, "fk_device_id");

                    b.HasIndex(new[] { "CreatedBy" }, "fk_visitor_device_created_by");

                    b.HasIndex(new[] { "UpdatedBy" }, "fk_visitor_device_updated_by");

                    b.HasIndex(new[] { "VisitorId" }, "fk_visitor_id");

                    b.ToTable("visitor_device", (string)null);
                });

            modelBuilder.Entity("VMS.Models.Device", b =>
                {
                    b.HasOne("VMS.Models.User", "CreatedByNavigation")
                        .WithMany("DeviceCreatedByNavigations")
                        .HasForeignKey("CreatedBy")
                        .HasConstraintName("fk_device_created_by");

                    b.HasOne("VMS.Models.User", "UpdatedByNavigation")
                        .WithMany("DeviceUpdatedByNavigations")
                        .HasForeignKey("UpdatedBy")
                        .HasConstraintName("fk_device_updated_by");

                    b.Navigation("CreatedByNavigation");

                    b.Navigation("UpdatedByNavigation");
                });

            modelBuilder.Entity("VMS.Models.OfficeLocation", b =>
                {
                    b.HasOne("VMS.Models.User", "CreatedByNavigation")
                        .WithMany("OfficeLocationCreatedByNavigations")
                        .HasForeignKey("CreatedBy")
                        .IsRequired()
                        .HasConstraintName("fk_office_location_created_by");

                    b.HasOne("VMS.Models.User", "UpdatedByNavigation")
                        .WithMany("OfficeLocationUpdatedByNavigations")
                        .HasForeignKey("UpdatedBy")
                        .IsRequired()
                        .HasConstraintName("fk_office_location_updated_by");

                    b.Navigation("CreatedByNavigation");

                    b.Navigation("UpdatedByNavigation");
                });

            modelBuilder.Entity("VMS.Models.Page", b =>
                {
                    b.HasOne("VMS.Models.User", "CreatedByNavigation")
                        .WithMany("PageCreatedByNavigations")
                        .HasForeignKey("CreatedBy")
                        .IsRequired()
                        .HasConstraintName("fk_page_created_by");

                    b.HasOne("VMS.Models.User", "UpdatedByNavigation")
                        .WithMany("PageUpdatedByNavigations")
                        .HasForeignKey("UpdatedBy")
                        .IsRequired()
                        .HasConstraintName("fk_page_updated_by");

                    b.Navigation("CreatedByNavigation");

                    b.Navigation("UpdatedByNavigation");
                });

            modelBuilder.Entity("VMS.Models.PageControl", b =>
                {
                    b.HasOne("VMS.Models.User", "CreatedByNavigation")
                        .WithMany("PageControlCreatedByNavigations")
                        .HasForeignKey("CreatedBy")
                        .IsRequired()
                        .HasConstraintName("fk_page_control_created_by");

                    b.HasOne("VMS.Models.Page", "Page")
                        .WithMany("PageControls")
                        .HasForeignKey("PageId")
                        .IsRequired()
                        .HasConstraintName("fk_page_control_page_id");

                    b.HasOne("VMS.Models.Role", "Role")
                        .WithMany("PageControls")
                        .HasForeignKey("RoleId")
                        .IsRequired()
                        .HasConstraintName("fk_page_control_role_id");

                    b.HasOne("VMS.Models.User", "UpdatedByNavigation")
                        .WithMany("PageControlUpdatedByNavigations")
                        .HasForeignKey("UpdatedBy")
                        .IsRequired()
                        .HasConstraintName("fk_page_control_updated_by");

                    b.Navigation("CreatedByNavigation");

                    b.Navigation("Page");

                    b.Navigation("Role");

                    b.Navigation("UpdatedByNavigation");
                });

            modelBuilder.Entity("VMS.Models.PurposeOfVisit", b =>
                {
                    b.HasOne("VMS.Models.User", "CreatedByNavigation")
                        .WithMany("PurposeOfVisitCreatedByNavigations")
                        .HasForeignKey("CreatedBy")
                        .HasConstraintName("fk_purpose_of_visit_created_by");

                    b.HasOne("VMS.Models.User", "UpdatedByNavigation")
                        .WithMany("PurposeOfVisitUpdatedByNavigations")
                        .HasForeignKey("UpdatedBy")
                        .HasConstraintName("fk_purpose_of_visit_updated_by");

                    b.Navigation("CreatedByNavigation");

                    b.Navigation("UpdatedByNavigation");
                });

            modelBuilder.Entity("VMS.Models.Role", b =>
                {
                    b.HasOne("VMS.Models.User", "CreatedByNavigation")
                        .WithMany("RoleCreatedByNavigations")
                        .HasForeignKey("CreatedBy")
                        .IsRequired()
                        .HasConstraintName("fk_role_created_by");

                    b.HasOne("VMS.Models.User", "UpdatedByNavigation")
                        .WithMany("RoleUpdatedByNavigations")
                        .HasForeignKey("UpdatedBy")
                        .HasConstraintName("fk_role_updated_by");

                    b.Navigation("CreatedByNavigation");

                    b.Navigation("UpdatedByNavigation");
                });

            modelBuilder.Entity("VMS.Models.User", b =>
                {
                    b.HasOne("VMS.Models.User", "CreatedByNavigation")
                        .WithMany("InverseCreatedByNavigation")
                        .HasForeignKey("CreatedBy")
                        .IsRequired()
                        .HasConstraintName("fk_user_created_by");

                    b.HasOne("VMS.Models.OfficeLocation", "Location")
                        .WithMany("Users")
                        .HasForeignKey("LocationId")
                        .IsRequired()
                        .HasConstraintName("fk_user_location_id");

                    b.HasOne("VMS.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .IsRequired()
                        .HasConstraintName("fk_user_role_id");

                    b.HasOne("VMS.Models.User", "UpdatedByNavigation")
                        .WithMany("InverseUpdatedByNavigation")
                        .HasForeignKey("UpdatedBy")
                        .IsRequired()
                        .HasConstraintName("fk_user_updated_by");

                    b.HasOne("VMS.Models.UserDetail", "UserDetails")
                        .WithMany("Users")
                        .HasForeignKey("UserDetailsId")
                        .IsRequired()
                        .HasConstraintName("fk_user_user_details_id");

                    b.Navigation("CreatedByNavigation");

                    b.Navigation("Location");

                    b.Navigation("Role");

                    b.Navigation("UpdatedByNavigation");

                    b.Navigation("UserDetails");
                });

            modelBuilder.Entity("VMS.Models.UserDetail", b =>
                {
                    b.HasOne("VMS.Models.User", "CreatedByNavigation")
                        .WithMany("UserDetailCreatedByNavigations")
                        .HasForeignKey("CreatedBy")
                        .IsRequired()
                        .HasConstraintName("fk_user_details_created_by");

                    b.HasOne("VMS.Models.User", "UpdatedByNavigation")
                        .WithMany("UserDetailUpdatedByNavigations")
                        .HasForeignKey("UpdatedBy")
                        .IsRequired()
                        .HasConstraintName("fk_user_details_updated_by");

                    b.Navigation("CreatedByNavigation");

                    b.Navigation("UpdatedByNavigation");
                });

            modelBuilder.Entity("VMS.Models.Visitor", b =>
                {
                    b.HasOne("VMS.Models.User", "CreatedByNavigation")
                        .WithMany("VisitorCreatedByNavigations")
                        .HasForeignKey("CreatedBy")
                        .IsRequired()
                        .HasConstraintName("fk_visitor_created_by");

                    b.HasOne("VMS.Models.OfficeLocation", "Location")
                        .WithMany("Visitors")
                        .HasForeignKey("LocationId")
                        .IsRequired()
                        .HasConstraintName("fk_visitor_location_id");

                    b.HasOne("VMS.Models.PurposeOfVisit", "Purpose")
                        .WithMany("Visitors")
                        .HasForeignKey("PurposeId")
                        .IsRequired()
                        .HasConstraintName("fk_visitor_purpose_id");

                    b.HasOne("VMS.Models.User", "UpdatedByNavigation")
                        .WithMany("VisitorUpdatedByNavigations")
                        .HasForeignKey("UpdatedBy")
                        .IsRequired()
                        .HasConstraintName("fk_visitor_updated_by");

                    b.HasOne("VMS.Models.User", "User")
                        .WithMany("VisitorUsers")
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_visitor_user_id");

                    b.Navigation("CreatedByNavigation");

                    b.Navigation("Location");

                    b.Navigation("Purpose");

                    b.Navigation("UpdatedByNavigation");

                    b.Navigation("User");
                });

            modelBuilder.Entity("VMS.Models.VisitorDevice", b =>
                {
                    b.HasOne("VMS.Models.User", "CreatedByNavigation")
                        .WithMany("VisitorDeviceCreatedByNavigations")
                        .HasForeignKey("CreatedBy")
                        .HasConstraintName("fk_visitor_device_created_by");

                    b.HasOne("VMS.Models.Device", "Device")
                        .WithMany("VisitorDevices")
                        .HasForeignKey("DeviceId")
                        .IsRequired()
                        .HasConstraintName("fk_device_id");

                    b.HasOne("VMS.Models.User", "UpdatedByNavigation")
                        .WithMany("VisitorDeviceUpdatedByNavigations")
                        .HasForeignKey("UpdatedBy")
                        .HasConstraintName("fk_visitor_device_updated_by");

                    b.HasOne("VMS.Models.Visitor", "Visitor")
                        .WithMany("VisitorDevices")
                        .HasForeignKey("VisitorId")
                        .IsRequired()
                        .HasConstraintName("fk_visitor_id");

                    b.Navigation("CreatedByNavigation");

                    b.Navigation("Device");

                    b.Navigation("UpdatedByNavigation");

                    b.Navigation("Visitor");
                });

            modelBuilder.Entity("VMS.Models.Device", b =>
                {
                    b.Navigation("VisitorDevices");
                });

            modelBuilder.Entity("VMS.Models.OfficeLocation", b =>
                {
                    b.Navigation("Users");

                    b.Navigation("Visitors");
                });

            modelBuilder.Entity("VMS.Models.Page", b =>
                {
                    b.Navigation("PageControls");
                });

            modelBuilder.Entity("VMS.Models.PurposeOfVisit", b =>
                {
                    b.Navigation("Visitors");
                });

            modelBuilder.Entity("VMS.Models.Role", b =>
                {
                    b.Navigation("PageControls");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("VMS.Models.User", b =>
                {
                    b.Navigation("DeviceCreatedByNavigations");

                    b.Navigation("DeviceUpdatedByNavigations");

                    b.Navigation("InverseCreatedByNavigation");

                    b.Navigation("InverseUpdatedByNavigation");

                    b.Navigation("OfficeLocationCreatedByNavigations");

                    b.Navigation("OfficeLocationUpdatedByNavigations");

                    b.Navigation("PageControlCreatedByNavigations");

                    b.Navigation("PageControlUpdatedByNavigations");

                    b.Navigation("PageCreatedByNavigations");

                    b.Navigation("PageUpdatedByNavigations");

                    b.Navigation("PurposeOfVisitCreatedByNavigations");

                    b.Navigation("PurposeOfVisitUpdatedByNavigations");

                    b.Navigation("RoleCreatedByNavigations");

                    b.Navigation("RoleUpdatedByNavigations");

                    b.Navigation("UserDetailCreatedByNavigations");

                    b.Navigation("UserDetailUpdatedByNavigations");

                    b.Navigation("VisitorCreatedByNavigations");

                    b.Navigation("VisitorDeviceCreatedByNavigations");

                    b.Navigation("VisitorDeviceUpdatedByNavigations");

                    b.Navigation("VisitorUpdatedByNavigations");

                    b.Navigation("VisitorUsers");
                });

            modelBuilder.Entity("VMS.Models.UserDetail", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("VMS.Models.Visitor", b =>
                {
                    b.Navigation("VisitorDevices");
                });
#pragma warning restore 612, 618
        }
    }
}
