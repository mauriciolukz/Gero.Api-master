using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gero.API.Migrations
{
    public partial class CreateContainerLoan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContainerLoans",
                schema: "DISTRIBUCION",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OrderNumber = table.Column<string>(maxLength: 100, nullable: false),
                    CustomerCode = table.Column<string>(maxLength: 10, nullable: false),
                    PresaleRoute = table.Column<string>(maxLength: 15, nullable: false),
                    DeliveryRoute = table.Column<string>(maxLength: 15, nullable: false),
                    LoadDate = table.Column<DateTimeOffset>(nullable: false),
                    DueDate = table.Column<DateTimeOffset>(nullable: false),
                    SynchronizationDate = table.Column<DateTimeOffset>(defaultValue: DateTimeOffset.Now, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContainerLoans", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContainerLoans",
                schema: "DISTRIBUCION");
        }
    }
}
