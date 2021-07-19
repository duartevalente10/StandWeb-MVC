using Microsoft.EntityFrameworkCore.Migrations;

namespace StandWeb.Data.Migrations
{
    public partial class marcas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c",
                column: "ConcurrencyStamp",
                value: "e52ec432-be40-4bc8-94ca-32454f155fbb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "g",
                column: "ConcurrencyStamp",
                value: "06af6534-938b-4d1e-81e7-4990f1bee172");

            migrationBuilder.InsertData(
                table: "ListaDeMarcas",
                columns: new[] { "IdMarcas", "Nome" },
                values: new object[,]
                {
                    { 1, "Buggati" },
                    { 2, "Pagani" },
                    { 3, "McLaren" },
                    { 4, "Lamborghini" },
                    { 5, "Koenigsegg" },
                    { 6, "Ferrari" },
                    { 7, "Porsche" },
                    { 8, "Rimac" },
                    { 9, "Outra" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ListaDeMarcas",
                keyColumn: "IdMarcas",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ListaDeMarcas",
                keyColumn: "IdMarcas",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ListaDeMarcas",
                keyColumn: "IdMarcas",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ListaDeMarcas",
                keyColumn: "IdMarcas",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ListaDeMarcas",
                keyColumn: "IdMarcas",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ListaDeMarcas",
                keyColumn: "IdMarcas",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ListaDeMarcas",
                keyColumn: "IdMarcas",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ListaDeMarcas",
                keyColumn: "IdMarcas",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ListaDeMarcas",
                keyColumn: "IdMarcas",
                keyValue: 9);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c",
                column: "ConcurrencyStamp",
                value: "5936d463-d47c-4f21-9088-79bfa79e11b0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "g",
                column: "ConcurrencyStamp",
                value: "cbf1219f-d34d-4508-b0d0-3d1268240cfa");
        }
    }
}
