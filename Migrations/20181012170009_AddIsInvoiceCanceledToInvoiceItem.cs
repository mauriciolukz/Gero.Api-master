using Microsoft.EntityFrameworkCore.Migrations;

namespace Gero.API.Migrations
{
    public partial class AddIsInvoiceCanceledToInvoiceItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Anulado",
                schema: "DISTRIBUCION",
                table: "DST_DetalleFactura",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Anulado",
                schema: "DISTRIBUCION",
                table: "DST_DetalleFactura");
        }
    }
}
