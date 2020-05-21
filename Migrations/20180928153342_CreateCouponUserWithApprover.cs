using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gero.API.Migrations
{
    public partial class CreateCouponUserWithApprover : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CouponUserWithApprovers",
                schema: "DISTRIBUCION",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicantUserId = table.Column<int>(nullable: false),
                    ApproverUserId = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CouponUserWithApprovers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CouponUserWithApprovers_CouponUsers_ApplicantUserId",
                        column: x => x.ApplicantUserId,
                        principalSchema: "DISTRIBUCION",
                        principalTable: "CouponUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CouponUserWithApprovers_CouponUsers_ApproverUserId",
                        column: x => x.ApproverUserId,
                        principalSchema: "DISTRIBUCION",
                        principalTable: "CouponUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CouponUserWithApprovers_ApplicantUserId",
                schema: "DISTRIBUCION",
                table: "CouponUserWithApprovers",
                column: "ApplicantUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CouponUserWithApprovers_ApproverUserId",
                schema: "DISTRIBUCION",
                table: "CouponUserWithApprovers",
                column: "ApproverUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CouponUserWithApprovers",
                schema: "DISTRIBUCION");
        }
    }
}
