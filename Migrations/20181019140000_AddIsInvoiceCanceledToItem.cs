using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gero.API.Migrations
{
    public partial class AddIsInvoiceCanceledToItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Anulado",
                schema: "DISTRIBUCION",
                table: "DST_DetalleFactura");

            migrationBuilder.AddColumn<bool>(
                name: "Anulado",
                schema: "DISTRIBUCION",
                table: "DST_Factura",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Anulado",
                schema: "DISTRIBUCION",
                table: "DST_Factura");

            migrationBuilder.AddColumn<bool>(
                name: "Anulado",
                schema: "DISTRIBUCION",
                table: "DST_DetalleFactura",
                nullable: false,
                defaultValue: false);
        }
    }
}
