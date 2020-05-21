using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gero.API.Migrations
{
    public partial class CreateTargetSale : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TargetSales",
                schema: "DISTRIBUCION",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Route = table.Column<string>(maxLength: 15, nullable: false),
                    Year = table.Column<int>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    ItemFamilyCode = table.Column<string>(maxLength: 15, nullable: false),
                    NineLitresTarget = table.Column<int>(nullable: false),
                    TargetAmount = table.Column<decimal>(type: "decimal(18, 4)", nullable: false),
                    WorkingDays = table.Column<int>(nullable: false),
                    NineLitresTargetPerDay = table.Column<int>(nullable: false),
                    TargetAmountPerDay = table.Column<decimal>(type: "decimal(18, 4)", nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    UpdatedBy = table.Column<int>(nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(defaultValue: DateTimeOffset.Now, nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TargetSales", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TargetSales",
                schema: "DISTRIBUCION");
        }
    }
}
