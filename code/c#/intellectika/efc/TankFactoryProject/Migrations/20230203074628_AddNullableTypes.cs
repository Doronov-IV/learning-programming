using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainEntityProject.Migrations
{
    /// <inheritdoc />
    public partial class AddNullableTypes : Migration
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

            migrationBuilder.DropIndex(
                name: "IX_Tanks_ManufacturerId",
                table: "Tanks");

            migrationBuilder.DropIndex(
                name: "IX_Tanks_PriceId",
                table: "Tanks");

            migrationBuilder.DropIndex(
                name: "IX_Manufacturers_BudgetId",
                table: "Manufacturers");

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

            migrationBuilder.AlterColumn<int>(
                name: "PriceId",
                table: "Tanks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ManufacturerId",
                table: "Tanks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "GunId",
                table: "Tanks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "EngineId",
                table: "Tanks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CrewCount",
                table: "Tanks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "BudgetId",
                table: "Manufacturers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PriceId",
                table: "Guns",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ManufacturerId",
                table: "Guns",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "LengthInCalibers",
                table: "Guns",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CaliberMillimetres",
                table: "Guns",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PriceId",
                table: "Engines",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ManufacturerId",
                table: "Engines",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "HorsePowers",
                table: "Engines",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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
                name: "IX_Manufacturers_BudgetId",
                table: "Manufacturers",
                column: "BudgetId",
                unique: true,
                filter: "[BudgetId] IS NOT NULL");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tanks_EngineId",
                table: "Tanks");

            migrationBuilder.DropIndex(
                name: "IX_Tanks_GunId",
                table: "Tanks");

            migrationBuilder.DropIndex(
                name: "IX_Tanks_ManufacturerId",
                table: "Tanks");

            migrationBuilder.DropIndex(
                name: "IX_Tanks_PriceId",
                table: "Tanks");

            migrationBuilder.DropIndex(
                name: "IX_Manufacturers_BudgetId",
                table: "Manufacturers");

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

            migrationBuilder.AlterColumn<int>(
                name: "PriceId",
                table: "Tanks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ManufacturerId",
                table: "Tanks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GunId",
                table: "Tanks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EngineId",
                table: "Tanks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CrewCount",
                table: "Tanks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BudgetId",
                table: "Manufacturers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PriceId",
                table: "Guns",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ManufacturerId",
                table: "Guns",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LengthInCalibers",
                table: "Guns",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CaliberMillimetres",
                table: "Guns",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PriceId",
                table: "Engines",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ManufacturerId",
                table: "Engines",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HorsePowers",
                table: "Engines",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tanks_EngineId",
                table: "Tanks",
                column: "EngineId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tanks_GunId",
                table: "Tanks",
                column: "GunId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tanks_ManufacturerId",
                table: "Tanks",
                column: "ManufacturerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tanks_PriceId",
                table: "Tanks",
                column: "PriceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Manufacturers_BudgetId",
                table: "Manufacturers",
                column: "BudgetId",
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
    }
}
