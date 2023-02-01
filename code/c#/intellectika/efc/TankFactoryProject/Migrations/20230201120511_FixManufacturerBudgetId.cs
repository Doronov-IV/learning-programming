using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TankFactoryProject.Migrations
{
    /// <inheritdoc />
    public partial class FixManufacturerBudgetId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Manufacturers_Budgets_BudgetId1",
                table: "Manufacturers");

            migrationBuilder.DropIndex(
                name: "IX_Manufacturers_BudgetId1",
                table: "Manufacturers");

            migrationBuilder.DropColumn(
                name: "BudgetId1",
                table: "Manufacturers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BudgetId1",
                table: "Manufacturers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Manufacturers_BudgetId1",
                table: "Manufacturers",
                column: "BudgetId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Manufacturers_Budgets_BudgetId1",
                table: "Manufacturers",
                column: "BudgetId1",
                principalTable: "Budgets",
                principalColumn: "Id");
        }
    }
}
