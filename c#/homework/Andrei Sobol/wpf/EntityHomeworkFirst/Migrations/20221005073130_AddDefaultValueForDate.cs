using EntityHomeworkFirst.ViewModel;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityHomeworkFirst.Migrations
{
    public partial class AddDefaultValueForDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                context.Orders.ToList().ForEach(order => order.Date = DateTime.Now.ToString());
                context.SaveChanges();
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                context.Orders.ToList().ForEach(order => order.Date = "NULL");
                context.SaveChanges();
            }
        }
    }
}
