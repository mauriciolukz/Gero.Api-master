using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gero.API.Migrations
{
    public partial class RenameUidFromDevice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Uid",
                schema: "DISTRIBUCION",
                table: "Devices");

            migrationBuilder.AddColumn<int>(
                name: "IMEI",
                schema: "DISTRIBUCION",
                table: "Devices",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IMEI",
                schema: "DISTRIBUCION",
                table: "Devices");

            migrationBuilder.AddColumn<string>(
                name: "Uid",
                schema: "DISTRIBUCION",
                table: "Devices",
                maxLength: 30,
                nullable: false,
                defaultValue: "");
        }
    }
}
