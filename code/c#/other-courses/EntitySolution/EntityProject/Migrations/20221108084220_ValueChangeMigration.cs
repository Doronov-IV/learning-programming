using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainEntityProject.Migrations
{
    public partial class ValueChangeMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Name",
                keyValue: "Daniel Defoe",
                value: "Definitely not Daniel Defoe",
                column: "Name",
                schema: null
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Name",
                keyValue: "Definitely not Daniel Defoe",
                value: "Daniel Defoe",
                column: "Name",
                schema: null
            );
        }
    }
}
