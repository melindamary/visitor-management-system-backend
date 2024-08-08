using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VMS.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMaxLengthOfFormSubmissionModeColumnInVisitorTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "form_submission_mode",
                table: "visitor",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "form_submission_mode",
                table: "visitor",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);
        }
    }
}
