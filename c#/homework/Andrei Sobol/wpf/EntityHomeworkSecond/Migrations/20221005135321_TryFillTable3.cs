using EntityHomeworkSecond.Model.Entities;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityHomeworkSecond.Migrations
{
    public partial class TryFillTable3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            Card card1;
            Card card2;

            Student student1 = new() { FirstName = "John", LastName = "von Neumann", Birthay = "28/12/1903", PhoneNumber = "88005553535" };
            Student student2 = new() { FirstName = "Ada", LastName = "Lovelace", Birthay = "10/12/1815", PhoneNumber = "88005553534" };

            card1 = new() { Student = student1 };
            card2 = new() { Student = student2 };

            student1.Card = card1;
            student2.Card = card2;

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
