using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StandWeb.Models
{
    public class CarrosAPIViewModel
    {
        /// <summary>
        /// Identificador do carro
        /// </summary>
        public int IdCarros { get; set; }

        /// <summary>
        /// Modelo do carro
        /// </summary> 
        public string Modelo { get; set; }

        /// <summary>
        /// Foto do carro
        /// </summary>
        public string Foto { get; set; }
       
        /// <summary>
        /// Ano do carro
        /// </summary>
        public int Ano { get; set; }

        /// <summary>
        /// Cilindrada
        /// </summary>
        public int Cilindrada { get; set; }

        /// <summary>
        /// Potencia
        /// </summary>
        public int Potencia { get; set; }

        /// <summary>
        /// Combustivel
        /// </summary>
        public string Combustivel { get; set; }

        /// <summary>
        /// Preco
        /// </summary>
        public string Preco { get; set; }

    }
}
