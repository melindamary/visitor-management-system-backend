using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace VMS.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusColumnToRoleAndDeviceTablesAndChangeVisitorPassCodeType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    password = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    valid_from = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_active = table.Column<int>(type: "integer", nullable: true),
                    is_logged_in = table.Column<int>(type: "integer", nullable: true),
                    created_by = table.Column<int>(type: "integer", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_by = table.Column<int>(type: "integer", nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user", x => x.user_id);
                    table.ForeignKey(
                        name: "fk_user_created_by",
                        column: x => x.created_by,
                        principalTable: "user",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "fk_user_updated_by",
                        column: x => x.updated_by,
                        principalTable: "user",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "device",
                columns: table => new
                {
                    device_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    device_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    status = table.Column<int>(type: "integer", nullable: true),
                    created_by = table.Column<int>(type: "integer", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_by = table.Column<int>(type: "integer", nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_device", x => x.device_id);
                    table.ForeignKey(
                        name: "fk_device_created_by",
                        column: x => x.created_by,
                        principalTable: "user",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "fk_device_updated_by",
                        column: x => x.updated_by,
                        principalTable: "user",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "office_location",
                columns: table => new
                {
                    office_location_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    location_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    address = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    phone = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    created_by = table.Column<int>(type: "integer", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_by = table.Column<int>(type: "integer", nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_office_location", x => x.office_location_id);
                    table.ForeignKey(
                        name: "fk_office_location_created_by",
                        column: x => x.created_by,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_office_location_updated_by",
                        column: x => x.updated_by,
                        principalTable: "user",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "page",
                columns: table => new
                {
                    page_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    page_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    page_url = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    created_by = table.Column<int>(type: "integer", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_by = table.Column<int>(type: "integer", nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_page", x => x.page_id);
                    table.ForeignKey(
                        name: "fk_page_created_by",
                        column: x => x.created_by,
                        principalTable: "user",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "fk_page_updated_by",
                        column: x => x.updated_by,
                        principalTable: "user",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "purpose_of_visit",
                columns: table => new
                {
                    purpose_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    purpose_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    created_by = table.Column<int>(type: "integer", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_by = table.Column<int>(type: "integer", nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_purpose_of_visit", x => x.purpose_id);
                    table.ForeignKey(
                        name: "fk_purpose_of_visit_created_by",
                        column: x => x.created_by,
                        principalTable: "user",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "fk_purpose_of_visit_updated_by",
                        column: x => x.updated_by,
                        principalTable: "user",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    role_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    role_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    created_by = table.Column<int>(type: "integer", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_by = table.Column<int>(type: "integer", nullable: true),
                    status = table.Column<int>(type: "integer", nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_role", x => x.role_id);
                    table.ForeignKey(
                        name: "fk_role_created_by",
                        column: x => x.created_by,
                        principalTable: "user",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "fk_role_updated_by",
                        column: x => x.updated_by,
                        principalTable: "user",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "user_details",
                columns: table => new
                {
                    user_details_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    office_location_id = table.Column<int>(type: "integer", nullable: false),
                    first_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    last_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    phone = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    address = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    created_by = table.Column<int>(type: "integer", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_by = table.Column<int>(type: "integer", nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_details", x => x.user_details_id);
                    table.ForeignKey(
                        name: "fk_user_details_created_by",
                        column: x => x.created_by,
                        principalTable: "user",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "fk_user_details_office_location_id",
                        column: x => x.office_location_id,
                        principalTable: "office_location",
                        principalColumn: "office_location_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_details_updated_by",
                        column: x => x.updated_by,
                        principalTable: "user",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "fk_user_details_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_location",
                columns: table => new
                {
                    user_location_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    office_location_id = table.Column<int>(type: "integer", nullable: false),
                    created_by = table.Column<int>(type: "integer", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_by = table.Column<int>(type: "integer", nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_location", x => x.user_location_id);
                    table.ForeignKey(
                        name: "fk_user_location_created_by",
                        column: x => x.created_by,
                        principalTable: "user",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "fk_user_location_office_location_id",
                        column: x => x.office_location_id,
                        principalTable: "office_location",
                        principalColumn: "office_location_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_location_updated_by",
                        column: x => x.updated_by,
                        principalTable: "user",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "fk_user_location_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "visitor",
                columns: table => new
                {
                    visitor_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    visitor_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    phone = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    purpose_id = table.Column<int>(type: "integer", nullable: false),
                    host_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    photo = table.Column<byte[]>(type: "bytea", nullable: false),
                    visit_date = table.Column<DateTime>(type: "timestamp", nullable: false),
                    visitor_pass_code = table.Column<int>(type: "integer", nullable: true),
                    check_in_time = table.Column<DateTime>(type: "timestamp", nullable: true),
                    check_out_time = table.Column<DateTime>(type: "timestamp", nullable: true),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    office_location_id = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: true),
                    created_by = table.Column<int>(type: "integer", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_by = table.Column<int>(type: "integer", nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_visitor", x => x.visitor_id);
                    table.ForeignKey(
                        name: "fk_visitor_created_by",
                        column: x => x.created_by,
                        principalTable: "user",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "fk_visitor_location_id",
                        column: x => x.office_location_id,
                        principalTable: "office_location",
                        principalColumn: "office_location_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_visitor_purpose_id",
                        column: x => x.purpose_id,
                        principalTable: "purpose_of_visit",
                        principalColumn: "purpose_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_visitor_updated_by",
                        column: x => x.updated_by,
                        principalTable: "user",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "fk_visitor_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "page_control",
                columns: table => new
                {
                    page_control_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    role_id = table.Column<int>(type: "integer", nullable: false),
                    page_id = table.Column<int>(type: "integer", nullable: false),
                    created_by = table.Column<int>(type: "integer", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_by = table.Column<int>(type: "integer", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_page_control", x => x.page_control_id);
                    table.ForeignKey(
                        name: "fk_page_control_created_by",
                        column: x => x.created_by,
                        principalTable: "user",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "fk_page_control_page_id",
                        column: x => x.page_id,
                        principalTable: "page",
                        principalColumn: "page_id");
                    table.ForeignKey(
                        name: "fk_page_control_role_id",
                        column: x => x.role_id,
                        principalTable: "role",
                        principalColumn: "role_id");
                    table.ForeignKey(
                        name: "fk_page_control_updated_by",
                        column: x => x.updated_by,
                        principalTable: "user",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "user_role",
                columns: table => new
                {
                    user_role_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    role_id = table.Column<int>(type: "integer", nullable: false),
                    created_by = table.Column<int>(type: "integer", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_by = table.Column<int>(type: "integer", nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_role", x => x.user_role_id);
                    table.ForeignKey(
                        name: "fk_user_role_created_by",
                        column: x => x.created_by,
                        principalTable: "user",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "fk_user_role_role_id",
                        column: x => x.role_id,
                        principalTable: "role",
                        principalColumn: "role_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_role_updated_by",
                        column: x => x.updated_by,
                        principalTable: "user",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "fk_user_role_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "visitor_device",
                columns: table => new
                {
                    visitor_device_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    visitor_id = table.Column<int>(type: "integer", nullable: false),
                    device_id = table.Column<int>(type: "integer", nullable: false),
                    serial_number = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    created_by = table.Column<int>(type: "integer", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_by = table.Column<int>(type: "integer", nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_visitor_device", x => x.visitor_device_id);
                    table.ForeignKey(
                        name: "fk_device_id",
                        column: x => x.device_id,
                        principalTable: "device",
                        principalColumn: "device_id");
                    table.ForeignKey(
                        name: "fk_visitor_device_created_by",
                        column: x => x.created_by,
                        principalTable: "user",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "fk_visitor_device_updated_by",
                        column: x => x.updated_by,
                        principalTable: "user",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "fk_visitor_id",
                        column: x => x.visitor_id,
                        principalTable: "visitor",
                        principalColumn: "visitor_id");
                });

            migrationBuilder.CreateIndex(
                name: "fk_device_created_by",
                table: "device",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "fk_device_updated_by",
                table: "device",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "fk_office_location_created_by",
                table: "office_location",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "fk_office_location_updated_by",
                table: "office_location",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "fk_page_created_by",
                table: "page",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "fk_page_updated_by",
                table: "page",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "fk_page_control_created_by",
                table: "page_control",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "fk_page_control_page_id",
                table: "page_control",
                column: "page_id");

            migrationBuilder.CreateIndex(
                name: "fk_page_control_role_id",
                table: "page_control",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "fk_page_control_updated_by",
                table: "page_control",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "fk_purpose_of_visit_created_by",
                table: "purpose_of_visit",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "fk_purpose_of_visit_updated_by",
                table: "purpose_of_visit",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "fk_role_created_by",
                table: "role",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "fk_role_updated_by",
                table: "role",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "fk_user_created_by",
                table: "user",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "fk_user_updated_by",
                table: "user",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "fk_user_details_created_by",
                table: "user_details",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "fk_user_details_office_location_id",
                table: "user_details",
                column: "office_location_id");

            migrationBuilder.CreateIndex(
                name: "fk_user_details_updated_by",
                table: "user_details",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "fk_user_details_user_id",
                table: "user_details",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "fk_user_location_created_by",
                table: "user_location",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "fk_user_location_office_location_id",
                table: "user_location",
                column: "office_location_id");

            migrationBuilder.CreateIndex(
                name: "fk_user_location_updated_by",
                table: "user_location",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "fk_user_location_user_id",
                table: "user_location",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "fk_user_role_created_by",
                table: "user_role",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "fk_user_role_role_id",
                table: "user_role",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "fk_user_role_updated_by",
                table: "user_role",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "fk_user_role_user_id",
                table: "user_role",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "fk_visitor_created_by",
                table: "visitor",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "fk_visitor_location_id",
                table: "visitor",
                column: "office_location_id");

            migrationBuilder.CreateIndex(
                name: "fk_visitor_purpose_id",
                table: "visitor",
                column: "purpose_id");

            migrationBuilder.CreateIndex(
                name: "fk_visitor_updated_by",
                table: "visitor",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "fk_visitor_user_id",
                table: "visitor",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "fk_device_id",
                table: "visitor_device",
                column: "device_id");

            migrationBuilder.CreateIndex(
                name: "fk_visitor_device_created_by",
                table: "visitor_device",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "fk_visitor_device_updated_by",
                table: "visitor_device",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "fk_visitor_id",
                table: "visitor_device",
                column: "visitor_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "page_control");

            migrationBuilder.DropTable(
                name: "user_details");

            migrationBuilder.DropTable(
                name: "user_location");

            migrationBuilder.DropTable(
                name: "user_role");

            migrationBuilder.DropTable(
                name: "visitor_device");

            migrationBuilder.DropTable(
                name: "page");

            migrationBuilder.DropTable(
                name: "role");

            migrationBuilder.DropTable(
                name: "device");

            migrationBuilder.DropTable(
                name: "visitor");

            migrationBuilder.DropTable(
                name: "office_location");

            migrationBuilder.DropTable(
                name: "purpose_of_visit");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
