using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NextDoorBackend.Migrations
{
    /// <inheritdoc />
    public partial class v1010 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVerified",
                table: "Accounts",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VerificationToken",
                table: "Accounts",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVerified",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "VerificationToken",
                table: "Accounts");
        }
    }
}
