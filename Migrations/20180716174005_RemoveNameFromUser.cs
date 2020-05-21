using Microsoft.EntityFrameworkCore.Migrations;

namespace Gero.API.Migrations
{
    public partial class RemoveNameFromUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                schema: "DISTRIBUCION",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastName",
                schema: "DISTRIBUCION",
                table: "Users");            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "DISTRIBUCION",
                table: "Users",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                schema: "DISTRIBUCION",
                table: "Users",
                maxLength: 50,
                nullable: true);
        }
    }
}
