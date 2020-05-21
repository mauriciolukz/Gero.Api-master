using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Gero.API.Migrations
{
    public partial class AddCreatedAtToSalesRegion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                schema: "DISTRIBUCION",
                table: "SalesRegions",
                nullable: false,
                defaultValue: DateTimeOffset.Now);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "DISTRIBUCION",
                table: "SalesRegions");
        }
    }
}
