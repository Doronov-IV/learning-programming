using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReversedService.Migrations
{
    /// <inheritdoc />
    public partial class FixTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_authorizationPairs",
                table: "authorizationPairs");

            migrationBuilder.RenameTable(
                name: "authorizationPairs",
                newName: "AuthorizationPairs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuthorizationPairs",
                table: "AuthorizationPairs",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AuthorizationPairs",
                table: "AuthorizationPairs");

            migrationBuilder.RenameTable(
                name: "AuthorizationPairs",
                newName: "authorizationPairs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_authorizationPairs",
                table: "authorizationPairs",
                column: "Id");
        }
    }
}
