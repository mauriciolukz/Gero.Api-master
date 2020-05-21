using Microsoft.EntityFrameworkCore.Migrations;

namespace Gero.API.Migrations
{
    public partial class CreateSynchronizationStepByRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SynchronizationStepByRoles",
                schema: "DISTRIBUCION",
                columns: table => new
                {
                    RoleId = table.Column<int>(nullable: false),
                    SynchronizationStepId = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SynchronizationStepByRoles", x => new { x.RoleId, x.SynchronizationStepId });
                    table.ForeignKey(
                        name: "FK_SynchronizationStepByRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "DISTRIBUCION",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SynchronizationStepByRoles_SynchronizationSteps_SynchronizationStepId",
                        column: x => x.SynchronizationStepId,
                        principalSchema: "DISTRIBUCION",
                        principalTable: "SynchronizationSteps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SynchronizationStepByRoles_RoleId",
                schema: "DISTRIBUCION",
                table: "SynchronizationStepByRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_SynchronizationStepByRoles_SynchronizationStepId",
                schema: "DISTRIBUCION",
                table: "SynchronizationStepByRoles",
                column: "SynchronizationStepId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SynchronizationStepByRoles",
                schema: "DISTRIBUCION");
        }
    }
}
