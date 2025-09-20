using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventory_api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Warehouses_Locations_LocationId1",
                table: "Warehouses");

            migrationBuilder.DropIndex(
                name: "IX_Warehouses_LocationId1",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "LocationId1",
                table: "Warehouses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocationId1",
                table: "Warehouses",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_LocationId1",
                table: "Warehouses",
                column: "LocationId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouses_Locations_LocationId1",
                table: "Warehouses",
                column: "LocationId1",
                principalTable: "Locations",
                principalColumn: "LocationId");
        }
    }
}
