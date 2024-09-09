using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NextDoorBackend.Migrations
{
    /// <inheritdoc />
    public partial class v105 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Update the Bbox column to be of type geometry(Polygon, 4326)
            migrationBuilder.AlterColumn<Polygon>(
                name: "Bbox",
                table: "Neighborhoods",
                type: "geometry(Polygon, 4326)",
                nullable: false,
                oldClrType: typeof(Polygon),
                oldType: "geometry");

            // Update the Geometry column to be of type geometry(Polygon, 4326)
            migrationBuilder.AlterColumn<Polygon>(
                name: "Geometry",
                table: "Neighborhoods",
                type: "geometry(Polygon, 4326)",
                nullable: false,
                oldClrType: typeof(Polygon),
                oldType: "geometry");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Revert the Bbox column to its previous type
            migrationBuilder.AlterColumn<Polygon>(
                name: "Bbox",
                table: "Neighborhoods",
                type: "geometry",
                nullable: false,
                oldClrType: typeof(Polygon),
                oldType: "geometry(Polygon, 4326)");

            // Revert the Geometry column to its previous type
            migrationBuilder.AlterColumn<Polygon>(
                name: "Geometry",
                table: "Neighborhoods",
                type: "geometry",
                nullable: false,
                oldClrType: typeof(Polygon),
                oldType: "geometry(Polygon, 4326)");
        }
    }
}
