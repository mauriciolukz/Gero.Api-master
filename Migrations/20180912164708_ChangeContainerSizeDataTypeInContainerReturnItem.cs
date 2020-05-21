using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gero.API.Migrations
{
    public partial class ChangeContainerSizeDataTypeInContainerReturnItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Tamanio",
                schema: "DISTRIBUCION",
                table: "DST_DetalleRetorno",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Tamanio",
                schema: "DISTRIBUCION",
                table: "DST_DetalleRetorno",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 150);
        }
    }
}
