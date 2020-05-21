using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gero.API.Migrations
{
    public partial class SetNullableToDateInAuthorizationCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdatedAt",
                schema: "DISTRIBUCION",
                table: "AuthorizationCodes",
                nullable: true,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<int>(
                name: "EntityId",
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
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Date",
                schema: "DISTRIBUCION",
                table: "AuthorizationCodes",
                nullable: true,
                oldClrType: typeof(DateTimeOffset));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdatedAt",
                schema: "DISTRIBUCION",
                table: "AuthorizationCodes",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EntityId",
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
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Date",
                schema: "DISTRIBUCION",
                table: "AuthorizationCodes",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldNullable: true);
        }
    }
}
