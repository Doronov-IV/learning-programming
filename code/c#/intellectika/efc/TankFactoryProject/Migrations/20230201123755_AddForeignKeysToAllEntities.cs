using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainEntityProject.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeysToAllEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MainBattleTank_EngineId",
                table: "MainBattleTank");

            migrationBuilder.DropIndex(
                name: "IX_MainBattleTank_GunId",
                table: "MainBattleTank");

            migrationBuilder.DropIndex(
                name: "IX_MainBattleTank_ManufacturerId",
                table: "MainBattleTank");

            migrationBuilder.DropIndex(
                name: "IX_MainBattleTank_PriceId",
                table: "MainBattleTank");

            migrationBuilder.DropIndex(
                name: "IX_Guns_ManufacturerId",
                table: "Guns");

            migrationBuilder.DropIndex(
                name: "IX_Guns_PriceId",
                table: "Guns");

            migrationBuilder.CreateIndex(
                name: "IX_MainBattleTank_EngineId",
                table: "MainBattleTank",
                column: "EngineId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MainBattleTank_GunId",
                table: "MainBattleTank",
                column: "GunId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MainBattleTank_ManufacturerId",
                table: "MainBattleTank",
                column: "ManufacturerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MainBattleTank_PriceId",
                table: "MainBattleTank",
                column: "PriceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Guns_ManufacturerId",
                table: "Guns",
                column: "ManufacturerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Guns_PriceId",
                table: "Guns",
                column: "PriceId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MainBattleTank_EngineId",
                table: "MainBattleTank");

            migrationBuilder.DropIndex(
                name: "IX_MainBattleTank_GunId",
                table: "MainBattleTank");

            migrationBuilder.DropIndex(
                name: "IX_MainBattleTank_ManufacturerId",
                table: "MainBattleTank");

            migrationBuilder.DropIndex(
                name: "IX_MainBattleTank_PriceId",
                table: "MainBattleTank");

            migrationBuilder.DropIndex(
                name: "IX_Guns_ManufacturerId",
                table: "Guns");

            migrationBuilder.DropIndex(
                name: "IX_Guns_PriceId",
                table: "Guns");

            migrationBuilder.CreateIndex(
                name: "IX_MainBattleTank_EngineId",
                table: "MainBattleTank",
                column: "EngineId");

            migrationBuilder.CreateIndex(
                name: "IX_MainBattleTank_GunId",
                table: "MainBattleTank",
                column: "GunId");

            migrationBuilder.CreateIndex(
                name: "IX_MainBattleTank_ManufacturerId",
                table: "MainBattleTank",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_MainBattleTank_PriceId",
                table: "MainBattleTank",
                column: "PriceId");

            migrationBuilder.CreateIndex(
                name: "IX_Guns_ManufacturerId",
                table: "Guns",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_Guns_PriceId",
                table: "Guns",
                column: "PriceId");
        }
    }
}
