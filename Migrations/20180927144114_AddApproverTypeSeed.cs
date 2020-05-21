using Microsoft.EntityFrameworkCore.Migrations;

namespace Gero.API.Migrations
{
    public partial class AddApproverTypeSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "DISTRIBUCION",
                table: "ApproverTypes",
                columns: new[] { "Id", "Code", "Name", "NextCode" },
                values: new object[,]
                {
                    { 1, "mobile_applicant", "Solicitante Móvil", "checker" },
                    { 2, "web_applicant", "Solicitante Web", "checker" },
                    { 3, "checker", "Verificador", "approver" },
                    { 4, "approver", "Aprobador", "master_approver" },
                    { 5, "master_approver", "Aprobador Superior", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "DISTRIBUCION",
                table: "ApproverTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "DISTRIBUCION",
                table: "ApproverTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "DISTRIBUCION",
                table: "ApproverTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "DISTRIBUCION",
                table: "ApproverTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "DISTRIBUCION",
                table: "ApproverTypes",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
