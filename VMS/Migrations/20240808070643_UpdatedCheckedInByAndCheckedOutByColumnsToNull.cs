using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VMS.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedCheckedInByAndCheckedOutByColumnsToNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_visitor_checked_in_id",
                table: "visitor");

            migrationBuilder.AlterColumn<int>(
                name: "checked_out_by",
                table: "visitor",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "checked_in_by",
                table: "visitor",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "fk_visitor_checked_in_id",
                table: "visitor",
                column: "checked_in_by",
                principalTable: "user",
                principalColumn: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_visitor_checked_in_id",
                table: "visitor");

            migrationBuilder.AlterColumn<int>(
                name: "checked_out_by",
                table: "visitor",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "checked_in_by",
                table: "visitor",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_visitor_checked_in_id",
                table: "visitor",
                column: "checked_in_by",
                principalTable: "user",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
