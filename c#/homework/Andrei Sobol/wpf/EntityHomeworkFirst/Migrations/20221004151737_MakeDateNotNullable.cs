using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityHomeworkFirst.Migrations
{
    public partial class MakeDateNotNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Date",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "gfhgfghd",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Date",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
