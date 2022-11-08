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
                values: new string[] { "Mark Twain", "https://en.wikipedia.org/wiki/Mark_Twain" },
                schema: default
            );

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new string[] { "Name", "WebUrl" },
                values: new string[] { "Daniel Defoe", "https://en.wikipedia.org/wiki/Daniel_Defoe" },
                schema: default
            );

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new string[] { "Name", "WebUrl" },
                values: new string[] { "Jules Verne", "https://en.wikipedia.org/wiki/Jules_Verne" },
                schema: default
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [Authors]; DELETE FROM [Books];");
        }
    }
}
