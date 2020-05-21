using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gero.API.Migrations
{
    public partial class AddAmountOfCentralizationToOrderItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "MontoCentralizacion",
                schema: "DISTRIBUCION",
                table: "DST_DetallePedido",
                type: "decimal(18, 4)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MontoCentralizacion",
                schema: "DISTRIBUCION",
                table: "DST_DetallePedido");
        }
    }
}
