using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gero.API.Migrations
{
    public partial class CreateInventoryLiquidation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InventoryLiquidations",
                schema: "DISTRIBUCION",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ItemCode = table.Column<string>(maxLength: 30, nullable: false),
                    Route = table.Column<string>(maxLength: 10, nullable: false),
                    SynchronizationDate = table.Column<DateTimeOffset>(defaultValue: DateTimeOffset.Now, nullable: false),
                    BoxesQuantity = table.Column<int>(nullable: false),
                    UnitsQuantity = table.Column<int>(nullable: false),
                    TotalOfUnits = table.Column<int>(nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18, 4)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18, 4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryLiquidations", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InventoryLiquidations",
                schema: "DISTRIBUCION");
        }
    }
}
