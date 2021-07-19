using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StandWeb.Data.Migrations
{
    public partial class inicio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carros",
                columns: table => new
                {
                    IdCarros = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Modelo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Foto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cilindrada = table.Column<int>(type: "int", nullable: false),
                    Potencia = table.Column<int>(type: "int", nullable: false),
                    Combustivel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Preco = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ano = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carros", x => x.IdCarros);
                });

            migrationBuilder.CreateTable(
                name: "ListaDeMarcas",
                columns: table => new
                {
                    IdMarcas = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListaDeMarcas", x => x.IdMarcas);
                });

            migrationBuilder.CreateTable(
                name: "Utilizadores",
                columns: table => new
                {
                    IdUtilizador = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserNameId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ControlarReview = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilizadores", x => x.IdUtilizador);
                });

            migrationBuilder.CreateTable(
                name: "CarrosMarcas",
                columns: table => new
                {
                    ListaDeCarrosIdCarros = table.Column<int>(type: "int", nullable: false),
                    ListaDeMarcasIdMarcas = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrosMarcas", x => new { x.ListaDeCarrosIdCarros, x.ListaDeMarcasIdMarcas });
                    table.ForeignKey(
                        name: "FK_CarrosMarcas_Carros_ListaDeCarrosIdCarros",
                        column: x => x.ListaDeCarrosIdCarros,
                        principalTable: "Carros",
                        principalColumn: "IdCarros",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarrosMarcas_ListaDeMarcas_ListaDeMarcasIdMarcas",
                        column: x => x.ListaDeMarcasIdMarcas,
                        principalTable: "ListaDeMarcas",
                        principalColumn: "IdMarcas",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Gostos",
                columns: table => new
                {
                    IdGostos = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UtilizadoresFK = table.Column<int>(type: "int", nullable: false),
                    CarrosFK = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gostos", x => x.IdGostos);
                    table.ForeignKey(
                        name: "FK_Gostos_Carros_CarrosFK",
                        column: x => x.CarrosFK,
                        principalTable: "Carros",
                        principalColumn: "IdCarros",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Gostos_Utilizadores_UtilizadoresFK",
                        column: x => x.UtilizadoresFK,
                        principalTable: "Utilizadores",
                        principalColumn: "IdUtilizador",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    IdReview = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comentario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pontuacao = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Visibilidade = table.Column<bool>(type: "bit", nullable: false),
                    UtilizadoresFK = table.Column<int>(type: "int", nullable: false),
                    CarrosFK = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.IdReview);
                    table.ForeignKey(
                        name: "FK_Reviews_Carros_CarrosFK",
                        column: x => x.CarrosFK,
                        principalTable: "Carros",
                        principalColumn: "IdCarros",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Utilizadores_UtilizadoresFK",
                        column: x => x.UtilizadoresFK,
                        principalTable: "Utilizadores",
                        principalColumn: "IdUtilizador",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c", "ed72cba2-04d1-46b2-b104-4db54fafb107", "Cliente", "CLIENTE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "g", "3b96561c-e802-4855-8709-a0a3e6367df0", "Gestor", "GESTOR" });

            migrationBuilder.CreateIndex(
                name: "IX_CarrosMarcas_ListaDeMarcasIdMarcas",
                table: "CarrosMarcas",
                column: "ListaDeMarcasIdMarcas");

            migrationBuilder.CreateIndex(
                name: "IX_Gostos_CarrosFK",
                table: "Gostos",
                column: "CarrosFK");

            migrationBuilder.CreateIndex(
                name: "IX_Gostos_UtilizadoresFK",
                table: "Gostos",
                column: "UtilizadoresFK");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CarrosFK",
                table: "Reviews",
                column: "CarrosFK");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UtilizadoresFK",
                table: "Reviews",
                column: "UtilizadoresFK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarrosMarcas");

            migrationBuilder.DropTable(
                name: "Gostos");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "ListaDeMarcas");

            migrationBuilder.DropTable(
                name: "Carros");

            migrationBuilder.DropTable(
                name: "Utilizadores");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "g");
        }
    }
}
