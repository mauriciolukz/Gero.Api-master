using Microsoft.EntityFrameworkCore.Migrations;

namespace Gero.API.Migrations
{
    public partial class AddMaximumNumberOfBoxesToCostCenterByItemFamily : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaximumNumberOfBoxes",
                schema: "DISTRIBUCION",
                table: "CostCenterByFamilies",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaximumNumberOfBoxes",
                schema: "DISTRIBUCION",
                table: "CostCenterByFamilies");
        }
    }
}
