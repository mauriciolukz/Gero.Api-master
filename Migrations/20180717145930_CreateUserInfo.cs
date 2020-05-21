using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gero.API.Migrations
{
    public partial class CreateUserInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserInfos",
                schema: "DISTRIBUCION",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    IDNumber = table.Column<string>(maxLength: 30, nullable: false),
                    LicenseNumber = table.Column<string>(maxLength: 20, nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    Birthdate = table.Column<DateTimeOffset>(nullable: false),
                    NationalityCode = table.Column<string>(maxLength: 10, nullable: false),
                    NationalityName = table.Column<string>(maxLength: 30, nullable: false),
                    DepartmentCode = table.Column<string>(maxLength: 10, nullable: false),
                    DepartmentName = table.Column<string>(maxLength: 30, nullable: false),
                    MunicipalityCode = table.Column<string>(maxLength: 10, nullable: false),
                    MunicipalityName = table.Column<string>(maxLength: 30, nullable: false),
                    Address = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserInfos_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "DISTRIBUCION",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTelephones",
                schema: "DISTRIBUCION",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TelephoneType = table.Column<string>(maxLength: 20, nullable: false),
                    TelephoneNumber = table.Column<string>(maxLength: 20, nullable: false),
                    UserInfoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTelephones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTelephones_UserInfos_UserInfoId",
                        column: x => x.UserInfoId,
                        principalSchema: "DISTRIBUCION",
                        principalTable: "UserInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_UserId",
                schema: "DISTRIBUCION",
                table: "UserInfos",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTelephones_UserInfoId",
                schema: "DISTRIBUCION",
                table: "UserTelephones",
                column: "UserInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserTelephones",
                schema: "DISTRIBUCION");

            migrationBuilder.DropTable(
                name: "UserInfos",
                schema: "DISTRIBUCION");
        }
    }
}
