using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gero.API.Migrations
{
    public partial class AddConsolidatedOrderIdToOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Consolidado",
                schema: "DISTRIBUCION",
                table: "DST_Pedido",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Consolidado",
                schema: "DISTRIBUCION",
                table: "DST_Pedido");
        }
    }
}
