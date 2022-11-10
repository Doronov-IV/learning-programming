using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainEntityProject.Migrations
{
    public partial class SeedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Authors",
                columns: new string[] { "Name", "WebUrl" },
                values: new string[] { "Mark Twain", null },
                schema: default
            );

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new string[] { "Name", "WebUrl" },
                values: new string[] { "Daniel Defoe", null },
                schema: default
            );

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new string[] { "Name", "WebUrl" },
                values: new string[] { "Jules Verne", null },
                schema: default
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [Authors]; DELETE FROM [Books];");
        }
    }
}
