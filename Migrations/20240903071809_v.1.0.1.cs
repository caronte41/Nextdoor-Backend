using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NextDoorBackend.Migrations
{
    /// <inheritdoc />
    public partial class v101 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Add a new Guid column with default values
            migrationBuilder.AddColumn<Guid>(
                name: "NewId",
                table: "Employees",
                type: "uuid",
                nullable: false,
                defaultValueSql: "gen_random_uuid()");

            // 2. Copy existing data into the new column (optional if you want to keep the old data)
            migrationBuilder.Sql("UPDATE \"Employees\" SET \"NewId\" = gen_random_uuid();");

            // 3. Drop the old Id column
            migrationBuilder.DropColumn(name: "Id", table: "Employees");

            // 4. Rename the new column to Id
            migrationBuilder.RenameColumn(name: "NewId", table: "Employees", newName: "Id");

            // 5. Set the new Id column as the primary key
            migrationBuilder.AddPrimaryKey("PK_Employees", "Employees", "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Reverse the operation in the Down method

            // 1. Add the old int Id column back
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Employees",
                type: "integer",
                nullable: false);

            // 2. Drop the Guid Id column
            migrationBuilder.DropColumn(name: "Id", table: "Employees");

            // 3. Rename Id to the old column
            migrationBuilder.RenameColumn(name: "OldId", table: "Employees", newName: "Id");

            // 4. Set the old Id column as the primary key
            migrationBuilder.AddPrimaryKey("PK_Employees", "Employees", "Id");
        }
    }
}
