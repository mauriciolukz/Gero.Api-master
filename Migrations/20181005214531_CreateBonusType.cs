using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gero.API.Migrations
{
    public partial class CreateBonusType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BonusTypes",
                schema: "DISTRIBUCION",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    PreviousBonusTypeId = table.Column<int>(nullable: true),
                    NextBonusTypeId = table.Column<int>(nullable: true),
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BonusTypes",
                schema: "DISTRIBUCION");
        }
    }
}
