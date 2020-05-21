using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gero.API.Migrations
{
    public partial class CreateBonusByCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BonusByCustomers",
                schema: "DISTRIBUCION",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerCode = table.Column<string>(maxLength: 30, nullable: false),
                    CustomerName = table.Column<string>(maxLength: 250, nullable: true),
                    BusinessName = table.Column<string>(maxLength: 250, nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 20, nullable: true),
                    Address = table.Column<string>(nullable: true),
                    BonusId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BonusByCustomers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BonusByCustomers_Bonus_BonusId",
                        column: x => x.BonusId,
                        principalSchema: "DISTRIBUCION",
                        principalTable: "Bonus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BonusByCustomers_BonusId",
                schema: "DISTRIBUCION",
                table: "BonusByCustomers",
                column: "BonusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BonusByCustomers",
                schema: "DISTRIBUCION");
        }
    }
}
