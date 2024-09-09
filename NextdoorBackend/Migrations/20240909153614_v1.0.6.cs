using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NextDoorBackend.Migrations
{
    /// <inheritdoc />
    public partial class v106 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "IndividualProfiles");

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "BusinessProfiles",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "BusinessProfiles",
                type: "double precision",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "BusinessProfiles");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "BusinessProfiles");

            migrationBuilder.AddColumn<Guid>(
                name: "ProfileId",
                table: "IndividualProfiles",
                type: "uuid",
                nullable: true);
        }
    }
}
