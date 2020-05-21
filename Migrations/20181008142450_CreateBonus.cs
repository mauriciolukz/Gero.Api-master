using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gero.API.Migrations
{
    public partial class CreateBonus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bonus",
                schema: "DISTRIBUCION",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TypeOfRequest = table.Column<int>(nullable: false),
                    ApplicantUserId = table.Column<int>(nullable: false),
                    OrderTypeId = table.Column<int>(nullable: false),
                    MotiveId = table.Column<int>(nullable: false),
                    WarehouseCode = table.Column<string>(maxLength: 6, nullable: false),
                    CheckerUserId = table.Column<int>(nullable: true),
                    ApproverUserId = table.Column<int>(nullable: false),
                    MasterApproverUserId = table.Column<int>(nullable: true),
                    CosterCenterCode = table.Column<string>(maxLength: 10, nullable: true),
                    DispatchDate = table.Column<DateTimeOffset>(nullable: true),
                    BonusStatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bonus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bonus_BonusStatuses_BonusStatusId",
                        column: x => x.BonusStatusId,
                        principalSchema: "DISTRIBUCION",
                        principalTable: "BonusStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bonus_Motives_MotiveId",
                        column: x => x.MotiveId,
                        principalSchema: "DISTRIBUCION",
                        principalTable: "Motives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bonus_OrderTypes_OrderTypeId",
                        column: x => x.OrderTypeId,
                        principalSchema: "DISTRIBUCION",
                        principalTable: "OrderTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bonus_BonusStatusId",
                schema: "DISTRIBUCION",
                table: "Bonus",
                column: "BonusStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Bonus_MotiveId",
                schema: "DISTRIBUCION",
                table: "Bonus",
                column: "MotiveId");

            migrationBuilder.CreateIndex(
                name: "IX_Bonus_OrderTypeId",
                schema: "DISTRIBUCION",
                table: "Bonus",
                column: "OrderTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bonus",
                schema: "DISTRIBUCION");
        }
    }
}
