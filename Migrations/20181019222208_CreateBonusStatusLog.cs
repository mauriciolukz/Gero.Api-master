using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gero.API.Migrations
{
    public partial class CreateBonusStatusLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BonusStatusLogs",
                schema: "DISTRIBUCION",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CurrentStatus = table.Column<int>(nullable: false),
                    NextStatus = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    BonusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BonusStatusLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BonusStatusLogs_Bonus_BonusId",
                        column: x => x.BonusId,
                        principalSchema: "DISTRIBUCION",
                        principalTable: "Bonus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BonusStatusLogs_BonusId",
                schema: "DISTRIBUCION",
                table: "BonusStatusLogs",
                column: "BonusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BonusStatusLogs",
                schema: "DISTRIBUCION");
        }
    }
}
