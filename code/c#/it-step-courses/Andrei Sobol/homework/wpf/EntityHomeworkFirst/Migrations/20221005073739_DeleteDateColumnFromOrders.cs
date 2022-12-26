using EntityHomeworkFirst.ViewModel;
using Microsoft.EntityFrameworkCore.Migrations;
using EntityHomeworkFirst.Model;

#nullable disable

namespace EntityHomeworkFirst.Migrations
{
    public partial class DeleteDateColumnFromOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(nameof(Order.Date), nameof(ApplicationContext.Orders));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: nameof(Order.Date),
                table: nameof(ApplicationContext.Orders),
                type: "nvarchar(24)",
                nullable: true);
        }
    }
}
