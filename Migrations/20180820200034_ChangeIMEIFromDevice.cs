using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gero.API.Migrations
{
    public partial class ChangeIMEIFromDevice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "IMEI",
                schema: "DISTRIBUCION",
                table: "Devices",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IMEI",
                schema: "DISTRIBUCION",
                table: "Devices",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
