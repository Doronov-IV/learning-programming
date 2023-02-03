using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainEntityProject.Migrations
{
    /// <inheritdoc />
    public partial class AddTanksReferencesToUnits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tanks_EngineId",
                table: "Tanks");

            migrationBuilder.DropIndex(
                name: "IX_Tanks_GunId",
                table: "Tanks");

            migrationBuilder.CreateIndex(
                name: "IX_Tanks_EngineId",
                table: "Tanks",
                column: "EngineId");

            migrationBuilder.CreateIndex(
                name: "IX_Tanks_GunId",
                table: "Tanks",
                column: "GunId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tanks_EngineId",
                table: "Tanks");

            migrationBuilder.DropIndex(
                name: "IX_Tanks_GunId",
                table: "Tanks");

            migrationBuilder.CreateIndex(
                name: "IX_Tanks_EngineId",
                table: "Tanks",
                column: "EngineId",
                unique: true,
                filter: "[EngineId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Tanks_GunId",
                table: "Tanks",
                column: "GunId",
                unique: true,
                filter: "[GunId] IS NOT NULL");
        }
    }
}
