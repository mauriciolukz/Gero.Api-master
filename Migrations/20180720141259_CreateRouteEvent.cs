using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gero.API.Migrations
{
    public partial class CreateRouteEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RouteEvents",
                schema: "DISTRIBUCION",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Route = table.Column<string>(maxLength: 10, nullable: false),
                    Status = table.Column<string>(maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: false),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RouteEvents_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "DISTRIBUCION",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RouteEventItems",
                schema: "DISTRIBUCION",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ActionName = table.Column<string>(maxLength: 50, nullable: false),
                    ActionDate = table.Column<DateTimeOffset>(nullable: false),
                    EntityName = table.Column<string>(maxLength: 50, nullable: false),
                    EntityId = table.Column<int>(nullable: false),
                    CustomerCode = table.Column<string>(maxLength: 30, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: false),
                    RouteEventId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteEventItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RouteEventItems_RouteEvents_RouteEventId",
                        column: x => x.RouteEventId,
                        principalSchema: "DISTRIBUCION",
                        principalTable: "RouteEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RouteEventItems_RouteEventId",
                schema: "DISTRIBUCION",
                table: "RouteEventItems",
                column: "RouteEventId");

            migrationBuilder.CreateIndex(
                name: "IX_RouteEvents_UserId",
                schema: "DISTRIBUCION",
                table: "RouteEvents",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RouteEventItems",
                schema: "DISTRIBUCION");

            migrationBuilder.DropTable(
                name: "RouteEvents",
                schema: "DISTRIBUCION");
        }
    }
}
