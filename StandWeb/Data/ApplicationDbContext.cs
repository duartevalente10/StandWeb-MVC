using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StandWeb.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StandWeb.Data
{

    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole { Id = "c", Name = "Cliente", NormalizedName = "CLIENTE" },
            new IdentityRole { Id = "g", Name = "Gestor", NormalizedName = "GESTOR" }
         );

            modelBuilder.Entity<Carros>().HasData(
                 new Carros { IdCarros = 1, Modelo = "Bugatti Veyron", Ano = 2010, Preco = "2300000", Cilindrada = 8000, Potencia = 1400, Combustivel = "Gasolina", Foto = "veyron.jpg" },
                 new Carros { IdCarros = 2, Modelo = "Bugatti Chiron", Ano = 2018, Preco = "2800000", Cilindrada = 9000, Potencia = 1600, Combustivel = "Gasolina", Foto = "chiron.jpg" },
                 new Carros { IdCarros = 3, Modelo = "Bugatti Divo", Ano = 2021, Preco = "5000000", Cilindrada = 10000, Potencia = 1700, Combustivel = "Gasolina", Foto = "divo.jpg" },
                 new Carros { IdCarros = 4, Modelo = "McLaren P1", Ano = 2019, Preco = "1300000", Cilindrada = 8000, Potencia = 1600, Combustivel = "Hibrido", Foto = "P1.jpg" },
                 new Carros { IdCarros = 5, Modelo = "McLaren Senna", Ano = 2020, Preco = "1000000", Cilindrada = 7000, Potencia = 1200, Combustivel = "Hibrido", Foto = "senna.jpg" },
                 new Carros { IdCarros = 6, Modelo = "Lamborgini Sian", Ano = 2021, Preco = "3700000", Cilindrada = 9000, Potencia = 1500, Combustivel = "Hibrido", Foto = "sian.jpg" },
                 new Carros { IdCarros = 7, Modelo = "Koenigsegg Gemera", Ano = 2021, Preco = "1900000", Cilindrada = 8000, Potencia = 1500, Combustivel = "Hibrido", Foto = "gemera.jpg" },
                 new Carros { IdCarros = 8, Modelo = "Koenigsegg Jesko", Ano = 2019, Preco = "2500000", Cilindrada = 9000, Potencia = 1700, Combustivel = "Gasolina", Foto = "jesko.jpg" },
                 new Carros { IdCarros = 9, Modelo = "Ferrari Laferrari ", Ano = 2018, Preco = "2000000", Cilindrada = 5000, Potencia = 850, Combustivel = "Hibrido", Foto = "LaFerrari.jpg" },
                 new Carros { IdCarros = 10, Modelo = "Porche Carrera gt", Ano = 2013, Preco = "1900000", Cilindrada = 6000, Potencia = 800, Combustivel = "Gasolina", Foto = "CarreraGT.jpg" },
                 new Carros { IdCarros = 11, Modelo = "Rimac Nevera", Ano = 2021, Preco = "100000", Cilindrada = 10000, Potencia = 2000, Combustivel = "Eletrico", Foto = "nevera.jpg" },
                 new Carros { IdCarros = 12, Modelo = "Paggani Huayra", Ano = 2017, Preco = "300000", Cilindrada = 8000, Potencia = 1300, Combustivel = "Gasolina", Foto = "Huayra.jpg" }
              );

            modelBuilder.Entity<Marcas>().HasData(
               new Marcas { IdMarcas = 1, Nome = "Buggati" },
               new Marcas { IdMarcas = 2, Nome = "Pagani" },
               new Marcas { IdMarcas = 3, Nome = "McLaren" },
               new Marcas { IdMarcas = 4, Nome = "Lamborghini" },
               new Marcas { IdMarcas = 5, Nome = "Koenigsegg" },
               new Marcas { IdMarcas = 6, Nome = "Ferrari" },
               new Marcas { IdMarcas = 7, Nome = "Porsche" },
               new Marcas { IdMarcas = 8, Nome = "Rimac" },
               new Marcas { IdMarcas = 9, Nome = "Outra" }
            );

        }

        /// <summary>
        /// Representar a Tabela Carros da BD
        /// </summary>
        public DbSet<Carros> Carros { get; set; }
        public DbSet<Marcas> ListaDeMarcas { get; set; }
        public DbSet<Utilizadores> Utilizadores { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<Gostos> Gostos { get; set; }
    }
}
