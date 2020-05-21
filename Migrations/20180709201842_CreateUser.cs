using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gero.API.Migrations
{
    public partial class CreateUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                schema: "DISTRIBUCION",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(maxLength: 30, nullable: false),
                    Password = table.Column<byte[]>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    LastName = table.Column<string>(maxLength: 50, nullable: true),
                    InvitationToken = table.Column<string>(maxLength: 100, nullable: true),
                    InvitationSentAt = table.Column<DateTimeOffset>(nullable: true),
                    InvitationAcceptedAt = table.Column<DateTimeOffset>(nullable: true),
                    ResetPasswordToken = table.Column<string>(maxLength: 100, nullable: true),
                    ResetPasswordSentAt = table.Column<DateTimeOffset>(nullable: true),
                    ResetPasswordExpiredAt = table.Column<DateTimeOffset>(nullable: true),
                    Status = table.Column<string>(maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users",
                schema: "DISTRIBUCION");
        }
    }
}
