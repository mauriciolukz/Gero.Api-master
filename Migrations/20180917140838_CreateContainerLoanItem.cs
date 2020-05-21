using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gero.API.Migrations
{
    public partial class CreateContainerLoanItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContainerLoanItems",
                schema: "DISTRIBUCION",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ItemCode = table.Column<string>(maxLength: 30, nullable: false),
                    ItemName = table.Column<string>(maxLength: 100, nullable: false),
                    BoxesQuantity = table.Column<int>(nullable: false),
                    UnitsPerBox = table.Column<int>(nullable: false),
                    UnitsQuantity = table.Column<int>(nullable: false),
                    TotalOfUnits = table.Column<int>(nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18, 4)", nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(18, 4)", nullable: false),
                    VAT = table.Column<decimal>(type: "decimal(18, 4)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18, 4)", nullable: false),
                    LoanItemStatus = table.Column<int>(nullable: false),
                    ContainerLoanId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContainerLoanItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContainerLoanItems_ContainerLoans_ContainerLoanId",
                        column: x => x.ContainerLoanId,
                        principalSchema: "DISTRIBUCION",
                        principalTable: "ContainerLoans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContainerLoanItems_ContainerLoanId",
                schema: "DISTRIBUCION",
                table: "ContainerLoanItems",
                column: "ContainerLoanId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContainerLoanItems",
                schema: "DISTRIBUCION");
        }
    }
}
