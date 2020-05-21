using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Gero.API.Migrations
{
    public partial class AddUpdatedAtToSalesRegion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedAt",
                schema: "DISTRIBUCION",
                table: "SalesRegions",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "DISTRIBUCION",
                table: "SalesRegions");
        }
    }
}
