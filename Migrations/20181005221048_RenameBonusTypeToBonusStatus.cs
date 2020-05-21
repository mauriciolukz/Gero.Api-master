using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gero.API.Migrations
{
    public partial class RenameBonusTypeToBonusStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BonusTypes",
                schema: "DISTRIBUCION");

            migrationBuilder.CreateTable(
                name: "BonusStatuses",
                schema: "DISTRIBUCION",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    PreviousBonusStatusId = table.Column<int>(nullable: true),
                    NextBonusStatusId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BonusStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BonusStatuses_BonusStatuses_PreviousBonusStatusId",
                        column: x => x.PreviousBonusStatusId,
                        principalSchema: "DISTRIBUCION",
                        principalTable: "BonusStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BonusStatuses_BonusStatuses_NextBonusStatusId",
                        column: x => x.NextBonusStatusId,
                        principalSchema: "DISTRIBUCION",
                        principalTable: "BonusStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BonusStatuses_PreviousBonusStatusId",
                schema: "DISTRIBUCION",
                table: "BonusStatuses",
                column: "PreviousBonusStatusId",
                unique: false);

            migrationBuilder.CreateIndex(
                name: "IX_BonusStatuses_NextBonusStatusId",
                schema: "DISTRIBUCION",
                table: "BonusStatuses",
                column: "NextBonusStatusId",
                unique: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BonusStatus",
                schema: "DISTRIBUCION");

            migrationBuilder.CreateTable(
                name: "BonusTypes",
                schema: "DISTRIBUCION",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    NextBonusTypeId = table.Column<int>(nullable: true),
                    PreviousBonusTypeId = table.Column<int>(nullable: true),
                    Status = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BonusTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BonusTypes_BonusTypes_PreviousBonusTypeId",
                        column: x => x.PreviousBonusTypeId,
                        principalSchema: "DISTRIBUCION",
                        principalTable: "BonusTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BonusTypes_BonusTypes_NextBonusTypeId",
                        column: x => x.NextBonusTypeId,
                        principalSchema: "DISTRIBUCION",
                        principalTable: "BonusTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BonusTypes_PreviousBonusTypeId",
                schema: "DISTRIBUCION",
                table: "BonusTypes",
                column: "PreviousBonusTypeId",
                unique: false);

            migrationBuilder.CreateIndex(
                name: "IX_BonusTypes_NextBonusTypeId",
                schema: "DISTRIBUCION",
                table: "BonusTypes",
                column: "NextBonusTypeId",
                unique: false);
        }
    }
}
