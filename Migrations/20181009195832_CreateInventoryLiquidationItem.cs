using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gero.API.Migrations
{
    public partial class CreateInventoryLiquidationItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BoxesQuantity",
                schema: "DISTRIBUCION",
                table: "InventoryLiquidations");

            migrationBuilder.DropColumn(
                name: "ItemCode",
                schema: "DISTRIBUCION",
                table: "InventoryLiquidations");

            migrationBuilder.DropColumn(
                name: "Total",
                schema: "DISTRIBUCION",
                table: "InventoryLiquidations");

            migrationBuilder.DropColumn(
                name: "TotalOfUnits",
                schema: "DISTRIBUCION",
                table: "InventoryLiquidations");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                schema: "DISTRIBUCION",
                table: "InventoryLiquidations");

            migrationBuilder.DropColumn(
                name: "UnitsQuantity",
                schema: "DISTRIBUCION",
                table: "InventoryLiquidations");

            migrationBuilder.CreateTable(
                name: "InventoryLiquidationItems",
                schema: "DISTRIBUCION",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ItemCode = table.Column<string>(maxLength: 30, nullable: false),
                    BoxesQuantity = table.Column<int>(nullable: false),
                    UnitsQuantity = table.Column<int>(nullable: false),
                    TotalOfUnits = table.Column<int>(nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18, 4)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18, 4)", nullable: false),
                    InventoryLiquidationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryLiquidationItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryLiquidationItems_InventoryLiquidations_InventoryLiquidationId",
                        column: x => x.InventoryLiquidationId,
                        principalSchema: "DISTRIBUCION",
                        principalTable: "InventoryLiquidations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryLiquidationItems_InventoryLiquidationId",
                schema: "DISTRIBUCION",
                table: "InventoryLiquidationItems",
                column: "InventoryLiquidationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InventoryLiquidationItems",
                schema: "DISTRIBUCION");

            migrationBuilder.AddColumn<int>(
                name: "BoxesQuantity",
                schema: "DISTRIBUCION",
                table: "InventoryLiquidations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ItemCode",
                schema: "DISTRIBUCION",
                table: "InventoryLiquidations",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                schema: "DISTRIBUCION",
                table: "InventoryLiquidations",
                type: "decimal(18, 4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "TotalOfUnits",
                schema: "DISTRIBUCION",
                table: "InventoryLiquidations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice",
                schema: "DISTRIBUCION",
                table: "InventoryLiquidations",
                type: "decimal(18, 4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "UnitsQuantity",
                schema: "DISTRIBUCION",
                table: "InventoryLiquidations",
                nullable: false,
                defaultValue: 0);
        }
    }
}
