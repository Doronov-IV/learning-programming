using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainEntityProject.Migrations
{
    /// <inheritdoc />
    public partial class TryRemoveListReferences : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tanks_ManufacturerId",
                table: "Tanks");

            migrationBuilder.DropIndex(
                name: "IX_Tanks_PriceId",
                table: "Tanks");

            migrationBuilder.DropIndex(
                name: "IX_Guns_ManufacturerId",
                table: "Guns");

            migrationBuilder.DropIndex(
                name: "IX_Guns_PriceId",
                table: "Guns");

            migrationBuilder.DropIndex(
                name: "IX_Engines_ManufacturerId",
                table: "Engines");

            migrationBuilder.DropIndex(
                name: "IX_Engines_PriceId",
                table: "Engines");

            migrationBuilder.CreateIndex(
                name: "IX_Tanks_ManufacturerId",
                table: "Tanks",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_Tanks_PriceId",
                table: "Tanks",
                column: "PriceId");

            migrationBuilder.CreateIndex(
                name: "IX_Guns_ManufacturerId",
                table: "Guns",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_Guns_PriceId",
                table: "Guns",
                column: "PriceId");

            migrationBuilder.CreateIndex(
                name: "IX_Engines_ManufacturerId",
                table: "Engines",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_Engines_PriceId",
                table: "Engines",
                column: "PriceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tanks_ManufacturerId",
                table: "Tanks");

            migrationBuilder.DropIndex(
                name: "IX_Tanks_PriceId",
                table: "Tanks");

            migrationBuilder.DropIndex(
                name: "IX_Guns_ManufacturerId",
                table: "Guns");

            migrationBuilder.DropIndex(
                name: "IX_Guns_PriceId",
                table: "Guns");

            migrationBuilder.DropIndex(
                name: "IX_Engines_ManufacturerId",
                table: "Engines");

            migrationBuilder.DropIndex(
                name: "IX_Engines_PriceId",
                table: "Engines");

            migrationBuilder.CreateIndex(
                name: "IX_Tanks_ManufacturerId",
                table: "Tanks",
                column: "ManufacturerId",
                unique: true,
                filter: "[ManufacturerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Tanks_PriceId",
                table: "Tanks",
                column: "PriceId",
                unique: true,
                filter: "[PriceId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Guns_ManufacturerId",
                table: "Guns",
                column: "ManufacturerId",
                unique: true,
                filter: "[ManufacturerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Guns_PriceId",
                table: "Guns",
                column: "PriceId",
                unique: true,
                filter: "[PriceId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Engines_ManufacturerId",
                table: "Engines",
                column: "ManufacturerId",
                unique: true,
                filter: "[ManufacturerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Engines_PriceId",
                table: "Engines",
                column: "PriceId",
                unique: true,
                filter: "[PriceId] IS NOT NULL");
        }
    }
}
