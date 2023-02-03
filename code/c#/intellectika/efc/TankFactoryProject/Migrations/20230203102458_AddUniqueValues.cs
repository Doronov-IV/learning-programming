using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainEntityProject.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ModelName",
                table: "Tanks",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModelName",
                table: "Guns",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModelName",
                table: "Engines",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tanks_ModelName",
                table: "Tanks",
                column: "ModelName",
                unique: true,
                filter: "[ModelName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_Value",
                table: "Prices",
                column: "Value",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Guns_ModelName",
                table: "Guns",
                column: "ModelName",
                unique: true,
                filter: "[ModelName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Engines_ModelName",
                table: "Engines",
                column: "ModelName",
                unique: true,
                filter: "[ModelName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_Value",
                table: "Budgets",
                column: "Value",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tanks_ModelName",
                table: "Tanks");

            migrationBuilder.DropIndex(
                name: "IX_Prices_Value",
                table: "Prices");

            migrationBuilder.DropIndex(
                name: "IX_Guns_ModelName",
                table: "Guns");

            migrationBuilder.DropIndex(
                name: "IX_Engines_ModelName",
                table: "Engines");

            migrationBuilder.DropIndex(
                name: "IX_Budgets_Value",
                table: "Budgets");

            migrationBuilder.AlterColumn<string>(
                name: "ModelName",
                table: "Tanks",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModelName",
                table: "Guns",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModelName",
                table: "Engines",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
