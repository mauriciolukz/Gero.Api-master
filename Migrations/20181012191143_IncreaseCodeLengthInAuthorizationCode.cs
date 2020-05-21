using Microsoft.EntityFrameworkCore.Migrations;

namespace Gero.API.Migrations
{
    public partial class IncreaseCodeLengthInAuthorizationCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "DISTRIBUCION",
                table: "AuthorizationCodes",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 8);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "DISTRIBUCION",
                table: "AuthorizationCodes",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 10);
        }
    }
}
