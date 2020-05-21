using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gero.API.Migrations
{
    public partial class CreateOrderTypeMotive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderTypeMotives",
                schema: "DISTRIBUCION",
                columns: table => new
                {
                    OrderTypeId = table.Column<int>(nullable: false),
                    MotiveId = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(defaultValue: DateTimeOffset.Now, nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderTypeMotives", x => new { x.OrderTypeId, x.MotiveId });
                    table.ForeignKey(
                        name: "FK_OrderTypeMotives_OrderTypes_OrderTypeId",
                        column: x => x.OrderTypeId,
                        principalSchema: "DISTRIBUCION",
                        principalTable: "OrderTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderTypeMotives_Motives_MotiveId",
                        column: x => x.MotiveId,
                        principalSchema: "DISTRIBUCION",
                        principalTable: "Motives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "DISTRIBUCION",
                table: "Motives",
                columns: new[] { "Id", "Code", "CreatedAt", "Name", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "011", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 425, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), "Bonificación por compra", "Active", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 425, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) },
                    { 24, "010", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 425, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), "Degustación de Productos/Evento Casa Pellas", "Active", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 425, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) },
                    { 23, "009", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 425, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), "Bonificaciones a Mayoristas", "Active", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 425, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) },
                    { 22, "008", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 425, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), "Apoyo a Eventos Municipales", "Active", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 425, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) },
                    { 21, "007", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 425, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), "Uso Tour FDC", "Active", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 425, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) },
                    { 20, "006", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 425, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), "Muestras para Distribuidor", "Active", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 425, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) },
                    { 19, "004", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 425, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), "Regalías a Empresas", "Active", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 425, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) },
                    { 18, "003", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 425, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), "Apoyo a Evento Sector Privado", "Active", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 425, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) },
                    { 17, "001", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 425, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), "Celebración Cumpleaños", "Active", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 425, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) },
                    { 16, "024", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 425, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), "Apoyo a instituciones públicas", "Active", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 425, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) },
                    { 14, "005", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 425, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), "Regalías a personas naturales", "Active", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 425, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) },
                    { 13, "023", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 425, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), "Inversión en el comercio", "Active", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 425, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) },
                    { 15, "002", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 425, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), "Donaciones a instituciones", "Active", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 425, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) },
                    { 11, "021", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 425, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), "Apoyo corporativos", "Active", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 425, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) },
                    { 10, "020", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 425, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), "Apoyo/Pago a medios", "Active", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 425, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) },
                    { 9, "019", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 425, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), "Degustaciones/Muestras/Capacitaciones", "Active", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 425, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) },
                    { 8, "018", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 425, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), "Hipicos y fiestas patronales", "Active", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 425, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) },
                    { 7, "017", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 425, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), "Pagos por espacios/exhibición", "Active", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 425, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) },
                    { 6, "016", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 425, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), "Apoyo por evento/aniversario/apertura", "Active", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 425, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) },
                    { 5, "015", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 425, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), "Reconocimiento de envase", "Active", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 425, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) },
                    { 4, "014", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 425, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), "Reconocimiento de margen/inventario", "Active", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 425, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) },
                    { 3, "013", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 425, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), "Planes de incentivos", "Active", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 425, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) },
                    { 2, "012", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 425, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), "Bonificación por plan de desalojo", "Active", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 425, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) },
                    { 12, "022", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 425, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), "Acuerdo comercial", "Active", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 425, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                schema: "DISTRIBUCION",
                table: "OrderTypes",
                columns: new[] { "Id", "CreatedAt", "Name", "ParentId", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { 6, new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 422, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), "Cambios", null, "Active", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 422, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) },
                    { 1, new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 419, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), "Entrega", null, "Active", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 422, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) },
                    { 2, new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 422, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), "Cupones", null, "Active", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 422, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) },
                    { 5, new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 422, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), "Consignación", null, "Active", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 422, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) },
                    { 7, new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 422, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), "Devoluciones", null, "Active", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 422, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) },
                    { 13, new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 422, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), "Préstamo de Envases", null, "Active", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 422, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                schema: "DISTRIBUCION",
                table: "OrderTypes",
                columns: new[] { "Id", "CreatedAt", "Name", "ParentId", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { 3, new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 422, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), "Regalía", 2, "Active", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 422, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) },
                    { 4, new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 422, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), "Bonificación", 2, "Active", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 422, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) },
                    { 8, new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 422, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), "Regalía", 7, "Active", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 422, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) },
                    { 9, new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 422, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), "Bonificación", 7, "Active", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 422, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) },
                    { 10, new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 422, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), "Consignación", 7, "Active", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 422, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) },
                    { 11, new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 422, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), "Factura", 7, "Active", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 422, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) },
                    { 12, new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 422, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), "Otra", 7, "Active", new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 422, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                schema: "DISTRIBUCION",
                table: "OrderTypeMotives",
                columns: new[] { "OrderTypeId", "MotiveId", "CreatedAt", "UpdatedAt" },
                values: new object[,]
                {
                    { 3, 1, new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 426, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 426, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) },
                    { 4, 21, new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 426, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 426, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) },
                    { 4, 20, new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 426, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 426, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) },
                    { 4, 19, new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 426, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 426, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) },
                    { 4, 18, new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 426, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 426, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) },
                    { 4, 17, new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 426, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 426, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) },
                    { 4, 16, new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 426, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 426, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) },
                    { 4, 15, new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 426, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 426, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) },
                    { 4, 14, new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 426, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 426, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) },
                    { 4, 13, new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 426, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 426, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) },
                    { 4, 12, new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 426, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 426, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) },
                    { 4, 11, new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 426, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 426, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) },
                    { 4, 10, new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 426, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 426, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) },
                    { 4, 9, new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 426, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 426, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) },
                    { 4, 8, new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 426, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 426, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) },
                    { 4, 7, new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 426, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 426, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) },
                    { 4, 6, new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 426, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 426, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) },
                    { 3, 23, new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 426, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 426, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) },
                    { 3, 5, new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 426, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 426, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) },
                    { 3, 4, new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 426, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 426, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) },
                    { 3, 3, new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 426, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 426, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) },
                    { 3, 2, new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 426, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 426, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) },
                    { 4, 22, new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 426, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 426, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) },
                    { 4, 24, new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 426, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), new DateTimeOffset(new DateTime(2018, 8, 9, 10, 41, 17, 426, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderTypeMotives_MotiveId",
                schema: "DISTRIBUCION",
                table: "OrderTypeMotives",
                column: "MotiveId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderTypeMotives",
                schema: "DISTRIBUCION");

            migrationBuilder.DeleteData(
                schema: "DISTRIBUCION",
                table: "Motives",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "DISTRIBUCION",
                table: "Motives",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "DISTRIBUCION",
                table: "Motives",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "DISTRIBUCION",
                table: "Motives",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "DISTRIBUCION",
                table: "Motives",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "DISTRIBUCION",
                table: "Motives",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "DISTRIBUCION",
                table: "Motives",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                schema: "DISTRIBUCION",
                table: "Motives",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                schema: "DISTRIBUCION",
                table: "Motives",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                schema: "DISTRIBUCION",
                table: "Motives",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                schema: "DISTRIBUCION",
                table: "Motives",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                schema: "DISTRIBUCION",
                table: "Motives",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                schema: "DISTRIBUCION",
                table: "Motives",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                schema: "DISTRIBUCION",
                table: "Motives",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                schema: "DISTRIBUCION",
                table: "Motives",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                schema: "DISTRIBUCION",
                table: "Motives",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                schema: "DISTRIBUCION",
                table: "Motives",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                schema: "DISTRIBUCION",
                table: "Motives",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                schema: "DISTRIBUCION",
                table: "Motives",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                schema: "DISTRIBUCION",
                table: "Motives",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                schema: "DISTRIBUCION",
                table: "Motives",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                schema: "DISTRIBUCION",
                table: "Motives",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                schema: "DISTRIBUCION",
                table: "Motives",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                schema: "DISTRIBUCION",
                table: "Motives",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                schema: "DISTRIBUCION",
                table: "OrderTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "DISTRIBUCION",
                table: "OrderTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "DISTRIBUCION",
                table: "OrderTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "DISTRIBUCION",
                table: "OrderTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "DISTRIBUCION",
                table: "OrderTypes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "DISTRIBUCION",
                table: "OrderTypes",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                schema: "DISTRIBUCION",
                table: "OrderTypes",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                schema: "DISTRIBUCION",
                table: "OrderTypes",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                schema: "DISTRIBUCION",
                table: "OrderTypes",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                schema: "DISTRIBUCION",
                table: "OrderTypes",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                schema: "DISTRIBUCION",
                table: "OrderTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "DISTRIBUCION",
                table: "OrderTypes",
                keyColumn: "Id",
                keyValue: 7);
        }
    }
}
