using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StandWeb.Models
{
    /// <summary>
    /// Descrição de um carro
    /// </summary>
    public class Carros
    {

        /// <summary>
        /// Construtores
        /// </summary>
        public Carros()
        {
            ListaDeReviews = new HashSet<Reviews>();
            ListaDeMarcas = new HashSet<Marcas>();
            ListaDeGostos = new HashSet<Gostos>();
        }

        /// <summary>
        /// Identificador do carro
        /// </summary>
        [Key]
        public int IdCarros { get; set; }

        /// <summary>
        /// Modelo do carro
        /// </summary>
        [Required]
        public string Modelo { get; set; }

        /// <summary>
        /// Foto do carro
        /// </summary>
        public string Foto { get; set; }

        /// <summary>
        /// Cilindrada
        /// </summary>
        [Required]
        public int Cilindrada { get; set; }

        /// <summary>
        /// Potencia
        /// </summary>
        [Required]
        public int Potencia { get; set; }

        /// <summary>
        /// Breve descrição sobre o carro
        /// </summary>
        [Required]
        public string Combustivel { get; set; }

        /// <summary>
        /// Preço do carro
        /// </summary>
        [Required]
        public string Preco { get; set; }

        /// <summary>
        /// Ano do carro
        /// </summary>
        [Required]
        public int Ano { get; set; }


        /// <summary>
        /// Lista das reviews dos carros
        /// </summary>
        public ICollection<Reviews> ListaDeReviews { get; set; }

        /// <summary>
        /// Lista da marca do carro
        /// </summary>
        public ICollection<Marcas> ListaDeMarcas { get; set; }

        /// <summary>
        /// lista de gostos
        /// </summary>
        public ICollection<Gostos> ListaDeGostos { get; set; }
    }
}
