using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainEntityProject.Migrations
{
    /// <inheritdoc />
    public partial class FixUniqueValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Prices_Value",
                table: "Prices");

            migrationBuilder.DropIndex(
                name: "IX_Budgets_Value",
                table: "Budgets");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Prices_Value",
                table: "Prices",
                column: "Value",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_Value",
                table: "Budgets",
                column: "Value",
                unique: true);
        }
    }
}
