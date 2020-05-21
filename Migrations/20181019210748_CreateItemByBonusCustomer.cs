using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gero.API.Migrations
{
    public partial class CreateItemByBonusCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemByBonusCustomers",
                schema: "DISTRIBUCION",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ItemCode = table.Column<string>(maxLength: 30, nullable: false),
                    ItemName = table.Column<string>(maxLength: 100, nullable: true),
                    CostCenterCode = table.Column<string>(maxLength: 10, nullable: true),
                    BoxesQuantity = table.Column<int>(nullable: false),
                    UnitsQuantity = table.Column<int>(nullable: false),
                    IsForExport = table.Column<bool>(nullable: false),
                    BonusByCustomerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemByBonusCustomers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemByBonusCustomers_BonusByCustomers_BonusByCustomerId",
                        column: x => x.BonusByCustomerId,
                        principalSchema: "DISTRIBUCION",
                        principalTable: "BonusByCustomers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemByBonusCustomers_BonusByCustomerId",
                schema: "DISTRIBUCION",
                table: "ItemByBonusCustomers",
                column: "BonusByCustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemByBonusCustomers",
                schema: "DISTRIBUCION");
        }
    }
}
