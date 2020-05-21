using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Gero.API.Migrations
{
    public partial class AddUpdatedAtToSalesChannel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedAt",
                schema: "DISTRIBUCION",
                table: "SalesChannels",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "DISTRIBUCION",
                table: "SalesChannels");
        }
    }
}
