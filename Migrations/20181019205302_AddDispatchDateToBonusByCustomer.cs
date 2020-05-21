using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gero.API.Migrations
{
    public partial class AddDispatchDateToBonusByCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CosterCenterCode",
                schema: "DISTRIBUCION",
                table: "Bonus");

            migrationBuilder.DropColumn(
                name: "DispatchDate",
                schema: "DISTRIBUCION",
                table: "Bonus");

            migrationBuilder.DropColumn(
                name: "WarehouseCode",
                schema: "DISTRIBUCION",
                table: "Bonus");

            migrationBuilder.AddColumn<string>(
                name: "CustomerTypeName",
                schema: "DISTRIBUCION",
                table: "BonusByCustomers",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DispatchDate",
                schema: "DISTRIBUCION",
                table: "BonusByCustomers",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "MotiveDescription",
                schema: "DISTRIBUCION",
                table: "BonusByCustomers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerTypeName",
                schema: "DISTRIBUCION",
                table: "BonusByCustomers");

            migrationBuilder.DropColumn(
                name: "DispatchDate",
                schema: "DISTRIBUCION",
                table: "BonusByCustomers");

            migrationBuilder.DropColumn(
                name: "MotiveDescription",
                schema: "DISTRIBUCION",
                table: "BonusByCustomers");

            migrationBuilder.AddColumn<string>(
                name: "CosterCenterCode",
                schema: "DISTRIBUCION",
                table: "Bonus",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DispatchDate",
                schema: "DISTRIBUCION",
                table: "Bonus",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "WarehouseCode",
                schema: "DISTRIBUCION",
                table: "Bonus",
                maxLength: 6,
                nullable: false,
                defaultValue: "");
        }
    }
}
