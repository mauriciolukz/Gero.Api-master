using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gero.API.Migrations
{
    public partial class AddCreatedAtToSalesChannel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                schema: "DISTRIBUCION",
                table: "SalesChannels",
                nullable: false,
                defaultValue: DateTimeOffset.Now);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "DISTRIBUCION",
                table: "SalesChannels");
        }
    }
}
