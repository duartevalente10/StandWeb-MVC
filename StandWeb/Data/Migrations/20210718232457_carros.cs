using Microsoft.EntityFrameworkCore.Migrations;

namespace StandWeb.Data.Migrations
{
    public partial class carros : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "Carros",
                columns: new[] { "IdCarros", "Ano", "Cilindrada", "Combustivel", "Foto", "Modelo", "Potencia", "Preco" },
                values: new object[,]
                {
                    { 1, 2010, 8000, "Gasolina", "veyron.jpg", "Bugatti Veyron", 1400, "2300000" },
                    { 2, 2018, 9000, "Gasolina", "chiron.jpg", "Bugatti Chiron", 1600, "2800000" },
                    { 3, 2021, 10000, "Gasolina", "divo.jpg", "Bugatti Divo", 1700, "5000000" },
                    { 4, 2019, 8000, "Hibrido", "P1.jpg", "McLaren P1", 1600, "1300000" },
                    { 5, 2020, 7000, "Hibrido", "senna.jpg", "McLaren Senna", 1200, "1000000" },
                    { 6, 2021, 9000, "Hibrido", "sian.jpg", "Lamborgini Sian", 1500, "3700000" },
                    { 7, 2021, 8000, "Hibrido", "gemera.jpg", "Koenigsegg Gemera", 1500, "1900000" },
                    { 8, 2019, 9000, "Gasolina", "jesko.jpg", "Koenigsegg Jesko", 1700, "2500000" },
                    { 9, 2018, 5000, "Hibrido", "LaFerrari.jpg", "Ferrari Laferrari ", 850, "2000000" },
                    { 10, 2013, 6000, "Gasolina", "CarreraGT.jpg", "Porche Carrera gt", 800, "1900000" },
                    { 11, 2021, 10000, "Eletrico", "nevera.jpg", "Rimac Nevera", 2000, "100000" },
                    { 12, 2017, 8000, "Gasolina", "Huayra.jpg", "Paggani Huayra", 1300, "300000" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Carros",
                keyColumn: "IdCarros",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Carros",
                keyColumn: "IdCarros",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Carros",
                keyColumn: "IdCarros",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Carros",
                keyColumn: "IdCarros",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Carros",
                keyColumn: "IdCarros",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Carros",
                keyColumn: "IdCarros",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Carros",
                keyColumn: "IdCarros",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Carros",
                keyColumn: "IdCarros",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Carros",
                keyColumn: "IdCarros",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Carros",
                keyColumn: "IdCarros",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Carros",
                keyColumn: "IdCarros",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Carros",
                keyColumn: "IdCarros",
                keyValue: 12);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c",
                column: "ConcurrencyStamp",
                value: "ed72cba2-04d1-46b2-b104-4db54fafb107");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "g",
                column: "ConcurrencyStamp",
                value: "3b96561c-e802-4855-8709-a0a3e6367df0");
        }
    }
}
