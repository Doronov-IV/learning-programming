using EntityHomeworkFirst.Model;
using EntityHomeworkFirst.ViewModel;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityHomeworkFirst.Migrations
{
    public partial class AddDateToOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: nameof(Order.Date),
                table: nameof(ApplicationContext.Orders),
                type: "nvarchar(24)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(nameof(Order.Date), nameof(ApplicationContext.Orders));
        }
    }
}
