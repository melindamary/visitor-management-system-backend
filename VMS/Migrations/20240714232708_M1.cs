using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace VMS.Migrations
{
    /// <inheritdoc />
    public partial class M1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "device",
                columns: table => new
                {
                    device_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    device_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    updated_date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.device_id);
                })
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "office_location",
                columns: table => new
                {
                    location_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    location_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    address = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    phone = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: false),
                    updated_date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.location_id);
                })
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "page",
                columns: table => new
                {
                    page_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    page_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    page_url = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: false),
                    updated_date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.page_id);
                })
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "page_control",
                columns: table => new
                {
                    page_control_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    role_id = table.Column<int>(type: "int", nullable: false),
                    page_id = table.Column<int>(type: "int", nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: false),
                    updated_date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.page_control_id);
                    table.ForeignKey(
                        name: "fk_page_control_page_id",
                        column: x => x.page_id,
                        principalTable: "page",
                        principalColumn: "page_id");
                })
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "purpose_of_visit",
                columns: table => new
                {
                    purpose_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    purpose_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    updated_date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.purpose_id);
                })
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    role_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    role_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    updated_date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.role_id);
                })
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    role_id = table.Column<int>(type: "int", nullable: false),
                    user_details_id = table.Column<int>(type: "int", nullable: false),
                    location_id = table.Column<int>(type: "int", nullable: false),
                    username = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    password = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    valid_from = table.Column<DateOnly>(type: "date", nullable: false),
                    is_active = table.Column<int>(type: "int", nullable: false, defaultValueSql: "'1'"),
                    is_logged_in = table.Column<int>(type: "int", nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: false),
                    updated_date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.user_id);
                    table.ForeignKey(
                        name: "fk_user_created_by",
                        column: x => x.created_by,
                        principalTable: "user",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "fk_user_location_id",
                        column: x => x.location_id,
                        principalTable: "office_location",
                        principalColumn: "location_id");
                    table.ForeignKey(
                        name: "fk_user_role_id",
                        column: x => x.role_id,
                        principalTable: "role",
                        principalColumn: "role_id");
                    table.ForeignKey(
                        name: "fk_user_updated_by",
                        column: x => x.updated_by,
                        principalTable: "user",
                        principalColumn: "user_id");
                })
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "user_details",
                columns: table => new
                {
                    user_details_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    first_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    last_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    phone = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    address = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: false),
                    updated_date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.user_details_id);
                    table.ForeignKey(
                        name: "fk_user_details_created_by",
                        column: x => x.created_by,
                        principalTable: "user",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "fk_user_details_updated_by",
                        column: x => x.updated_by,
                        principalTable: "user",
                        principalColumn: "user_id");
                })
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "visitor",
                columns: table => new
                {
                    visitor_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    visitor_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    phone = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    purpose_id = table.Column<int>(type: "int", nullable: false),
                    host_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    photo = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    visit_date = table.Column<DateOnly>(type: "date", nullable: false),
                    visitor_pass_code = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    check_in_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    check_out_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    location_id = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: false),
                    updated_date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.visitor_id);
                    table.ForeignKey(
                        name: "fk_visitor_created_by",
                        column: x => x.created_by,
                        principalTable: "user",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "fk_visitor_location_id",
                        column: x => x.location_id,
                        principalTable: "office_location",
                        principalColumn: "location_id");
                    table.ForeignKey(
                        name: "fk_visitor_purpose_id",
                        column: x => x.purpose_id,
                        principalTable: "purpose_of_visit",
                        principalColumn: "purpose_id");
                    table.ForeignKey(
                        name: "fk_visitor_updated_by",
                        column: x => x.updated_by,
                        principalTable: "user",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "fk_visitor_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "user_id");
                })
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "visitor_device",
                columns: table => new
                {
                    visitor_device_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    visitor_id = table.Column<int>(type: "int", nullable: false),
                    device_id = table.Column<int>(type: "int", nullable: false),
                    serial_number = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    updated_date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.visitor_device_id);
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
                })
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

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
                name: "fk_user_location_id",
                table: "user",
                column: "location_id");

            migrationBuilder.CreateIndex(
                name: "fk_user_role_id",
                table: "user",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "fk_user_updated_by",
                table: "user",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "fk_user_user_details_id",
                table: "user",
                column: "user_details_id");

            migrationBuilder.CreateIndex(
                name: "fk_user_details_created_by",
                table: "user_details",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "fk_user_details_updated_by",
                table: "user_details",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "fk_visitor_created_by",
                table: "visitor",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "fk_visitor_location_id",
                table: "visitor",
                column: "location_id");

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

            migrationBuilder.AddForeignKey(
                name: "fk_device_created_by",
                table: "device",
                column: "created_by",
                principalTable: "user",
                principalColumn: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_device_updated_by",
                table: "device",
                column: "updated_by",
                principalTable: "user",
                principalColumn: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_office_location_created_by",
                table: "office_location",
                column: "created_by",
                principalTable: "user",
                principalColumn: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_office_location_updated_by",
                table: "office_location",
                column: "updated_by",
                principalTable: "user",
                principalColumn: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_page_created_by",
                table: "page",
                column: "created_by",
                principalTable: "user",
                principalColumn: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_page_updated_by",
                table: "page",
                column: "updated_by",
                principalTable: "user",
                principalColumn: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_page_control_created_by",
                table: "page_control",
                column: "created_by",
                principalTable: "user",
                principalColumn: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_page_control_updated_by",
                table: "page_control",
                column: "updated_by",
                principalTable: "user",
                principalColumn: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_page_control_role_id",
                table: "page_control",
                column: "role_id",
                principalTable: "role",
                principalColumn: "role_id");

            migrationBuilder.AddForeignKey(
                name: "fk_purpose_of_visit_created_by",
                table: "purpose_of_visit",
                column: "created_by",
                principalTable: "user",
                principalColumn: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_purpose_of_visit_updated_by",
                table: "purpose_of_visit",
                column: "updated_by",
                principalTable: "user",
                principalColumn: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_role_created_by",
                table: "role",
                column: "created_by",
                principalTable: "user",
                principalColumn: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_role_updated_by",
                table: "role",
                column: "updated_by",
                principalTable: "user",
                principalColumn: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_user_user_details_id",
                table: "user",
                column: "user_details_id",
                principalTable: "user_details",
                principalColumn: "user_details_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_office_location_created_by",
                table: "office_location");

            migrationBuilder.DropForeignKey(
                name: "fk_office_location_updated_by",
                table: "office_location");

            migrationBuilder.DropForeignKey(
                name: "fk_role_created_by",
                table: "role");

            migrationBuilder.DropForeignKey(
                name: "fk_role_updated_by",
                table: "role");

            migrationBuilder.DropForeignKey(
                name: "fk_user_details_created_by",
                table: "user_details");

            migrationBuilder.DropForeignKey(
                name: "fk_user_details_updated_by",
                table: "user_details");

            migrationBuilder.DropTable(
                name: "page_control");

            migrationBuilder.DropTable(
                name: "visitor_device");

            migrationBuilder.DropTable(
                name: "page");

            migrationBuilder.DropTable(
                name: "device");

            migrationBuilder.DropTable(
                name: "visitor");

            migrationBuilder.DropTable(
                name: "purpose_of_visit");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "office_location");

            migrationBuilder.DropTable(
                name: "role");

            migrationBuilder.DropTable(
                name: "user_details");
        }
    }
}
