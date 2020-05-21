using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gero.API.Migrations
{
    public partial class CreateCouponUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CouponUsers",
                schema: "DISTRIBUCION",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    ApproverTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CouponUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CouponUsers_ApproverTypes_ApproverTypeId",
                        column: x => x.ApproverTypeId,
                        principalSchema: "DISTRIBUCION",
                        principalTable: "ApproverTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CouponUsers_ApproverTypeId",
                schema: "DISTRIBUCION",
                table: "CouponUsers",
                column: "ApproverTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CouponUsers",
                schema: "DISTRIBUCION");
        }
    }
}
