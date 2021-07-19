using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StandWeb.Models
{
    /// <summary>
    /// Diferentes tipos de categorias de Carros
    /// </summary>
    public class Marcas
    {
        /// <summary>
        /// Construtor da classe
        /// </summary>
        public Marcas()
        {
            ListaDeCarros = new HashSet<Carros>();
        }
        /// <summary>
        /// Identificador de marca
        /// </summary>
        [Key]
        public int IdMarcas { get; set; }

        /// <summary>
        /// Nome da marca
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Lista de marcas
        /// </summary>
        public ICollection<Carros> ListaDeCarros { get; set; }
    }
}
