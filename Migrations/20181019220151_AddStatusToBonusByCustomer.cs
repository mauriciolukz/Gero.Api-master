using Microsoft.EntityFrameworkCore.Migrations;

namespace Gero.API.Migrations
{
    public partial class AddStatusToBonusByCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "DISTRIBUCION",
                table: "ItemByBonusCustomers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "DISTRIBUCION",
                table: "BonusByCustomers",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                schema: "DISTRIBUCION",
                table: "ItemByBonusCustomers");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "DISTRIBUCION",
                table: "BonusByCustomers");
        }
    }
}
