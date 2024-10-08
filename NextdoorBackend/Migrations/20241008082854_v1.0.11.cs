using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NextDoorBackend.Migrations
{
    /// <inheritdoc />
    public partial class v1011 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Modify the CoverPhoto column to type text (string)
            migrationBuilder.AlterColumn<string>(
                name: "CoverPhoto",
                table: "Events",
                type: "text",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "bytea");

            // No need to add EventEndDay and EventEndHour again, since they already exist in the DB
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Revert the CoverPhoto column back to byte[] (bytea)
            migrationBuilder.AlterColumn<byte[]>(
                name: "CoverPhoto",
                table: "Events",
                type: "bytea",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            // No need to remove EventEndDay and EventEndHour since we didn't modify them
        }
    }


}
