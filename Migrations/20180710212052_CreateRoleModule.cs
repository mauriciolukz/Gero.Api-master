using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gero.API.Migrations
{
    public partial class CreateRoleModule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoleModules",
                schema: "DISTRIBUCION",
                columns: table => new
                {
                    RoleId = table.Column<int>(nullable: false),
                    ModuleId = table.Column<int>(nullable: false),
                    Status = table.Column<string>(maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(defaultValue: DateTimeOffset.Now, nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleModules", x => new { x.RoleId, x.ModuleId });
                    table.ForeignKey(
                        name: "FK_RoleModules_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "DISTRIBUCION",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleModules_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalSchema: "DISTRIBUCION",
                        principalTable: "Modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoleModules_RoleId",
                schema: "DISTRIBUCION",
                table: "RoleModules",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleModules_ModuleId",
                schema: "DISTRIBUCION",
                table: "RoleModules",
                column: "ModuleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleModules",
                schema: "DISTRIBUCION");
        }
    }
}
