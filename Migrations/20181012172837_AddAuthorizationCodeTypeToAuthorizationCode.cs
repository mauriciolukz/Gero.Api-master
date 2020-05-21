using Microsoft.EntityFrameworkCore.Migrations;

namespace Gero.API.Migrations
{
    public partial class AddAuthorizationCodeTypeToAuthorizationCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorizationCodes_Users_UserId",
                schema: "DISTRIBUCION",
                table: "AuthorizationCodes");

            migrationBuilder.DropColumn(
                name: "Type",
                schema: "DISTRIBUCION",
                table: "AuthorizationCodes");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                schema: "DISTRIBUCION",
                table: "AuthorizationCodes",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Entity",
                schema: "DISTRIBUCION",
                table: "AuthorizationCodes",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "CustomerCode",
                schema: "DISTRIBUCION",
                table: "AuthorizationCodes",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 2)");

            migrationBuilder.AddColumn<int>(
                name: "AuthorizationCodeTypeId",
                schema: "DISTRIBUCION",
                table: "AuthorizationCodes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuthorizationCodes_AuthorizationCodeTypeId",
                schema: "DISTRIBUCION",
                table: "AuthorizationCodes",
                column: "AuthorizationCodeTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorizationCodes_AuthorizatonCodeTypes_AuthorizationCodeTypeId",
                schema: "DISTRIBUCION",
                table: "AuthorizationCodes",
                column: "AuthorizationCodeTypeId",
                principalSchema: "DISTRIBUCION",
                principalTable: "AuthorizatonCodeTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorizationCodes_Users_UserId",
                schema: "DISTRIBUCION",
                table: "AuthorizationCodes",
                column: "UserId",
                principalSchema: "DISTRIBUCION",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorizationCodes_AuthorizatonCodeTypes_AuthorizationCodeTypeId",
                schema: "DISTRIBUCION",
                table: "AuthorizationCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorizationCodes_Users_UserId",
                schema: "DISTRIBUCION",
                table: "AuthorizationCodes");

            migrationBuilder.DropIndex(
                name: "IX_AuthorizationCodes_AuthorizationCodeTypeId",
                schema: "DISTRIBUCION",
                table: "AuthorizationCodes");

            migrationBuilder.DropColumn(
                name: "AuthorizationCodeTypeId",
                schema: "DISTRIBUCION",
                table: "AuthorizationCodes");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                schema: "DISTRIBUCION",
                table: "AuthorizationCodes",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Entity",
                schema: "DISTRIBUCION",
                table: "AuthorizationCodes",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "CustomerCode",
                schema: "DISTRIBUCION",
                table: "AuthorizationCodes",
                type: "decimal(18, 2)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                schema: "DISTRIBUCION",
                table: "AuthorizationCodes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorizationCodes_Users_UserId",
                schema: "DISTRIBUCION",
                table: "AuthorizationCodes",
                column: "UserId",
                principalSchema: "DISTRIBUCION",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
