using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gero.API.Migrations
{
    public partial class CreateConsolidatedOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConsolidatedOrders",
                schema: "DISTRIBUCION",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RouteCode = table.Column<string>(maxLength: 10, nullable: false),
                    PickingId = table.Column<int>(nullable: false),
                    OrderStatus = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(defaultValue: DateTimeOffset.Now, nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(defaultValue: DateTimeOffset.Now, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsolidatedOrders", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsolidatedOrders",
                schema: "DISTRIBUCION");
        }
    }
}
