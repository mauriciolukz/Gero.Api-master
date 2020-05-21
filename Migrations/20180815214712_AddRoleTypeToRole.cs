using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gero.API.Migrations
{
    public partial class AddRoleTypeToRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "imageURL",
                schema: "DISTRIBUCION",
                table: "ItemImages",
                newName: "ImageURL");

            migrationBuilder.AddColumn<int>(
                name: "RoleType",
                schema: "DISTRIBUCION",
                table: "Roles",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleType",
                schema: "DISTRIBUCION",
                table: "Roles");

            migrationBuilder.RenameColumn(
                name: "ImageURL",
                schema: "DISTRIBUCION",
                table: "ItemImages",
                newName: "imageURL");
        }
    }
}
