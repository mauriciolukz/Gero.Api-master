using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gero.API.Migrations
{
    public partial class CreateSalesRegion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SalesRegions",
                schema: "DISTRIBUCION",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesRegions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SalesChannels",
                schema: "DISTRIBUCION",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Route = table.Column<string>(nullable: false),
                    SalesRegionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesChannels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesChannels_SalesRegions_SalesRegionId",
                        column: x => x.SalesRegionId,
                        principalSchema: "DISTRIBUCION",
                        principalTable: "SalesRegions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalesChannels_SalesRegionId",
                schema: "DISTRIBUCION",
                table: "SalesChannels",
                column: "SalesRegionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                schema: "DISTRIBUCION",
                name: "SalesChannels"
            );

            migrationBuilder.DropTable(
                schema: "DISTRIBUCION",
                name: "SalesRegions"
            );
        }
    }
}
