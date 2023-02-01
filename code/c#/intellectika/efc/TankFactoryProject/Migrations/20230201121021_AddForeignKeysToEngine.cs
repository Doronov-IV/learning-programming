using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainEntityProject.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeysToEngine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Engines_ManufacturerId",
                table: "Engines");

            migrationBuilder.DropIndex(
                name: "IX_Engines_PriceId",
                table: "Engines");

            migrationBuilder.CreateIndex(
                name: "IX_Engines_ManufacturerId",
                table: "Engines",
                column: "ManufacturerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Engines_PriceId",
                table: "Engines",
                column: "PriceId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Engines_ManufacturerId",
                table: "Engines");

            migrationBuilder.DropIndex(
                name: "IX_Engines_PriceId",
                table: "Engines");

            migrationBuilder.CreateIndex(
                name: "IX_Engines_ManufacturerId",
                table: "Engines",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_Engines_PriceId",
                table: "Engines",
                column: "PriceId");
        }
    }
}
