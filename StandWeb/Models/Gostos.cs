using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StandWeb.Models {
    public class Gostos {

        /// <summary>
        /// Identificador da lista de gostos
        /// </summary>
        [Key]
        public int IdGostos { get; set; }

        //*****************************
        /// <summary>
        /// Fk para o Utilizador
        /// </summary>
        [ForeignKey(nameof(Utilizador))]
        public int UtilizadoresFK { get; set; }
        public Utilizadores Utilizador { get; set; }


        /// <summary>
        /// FK para o Carro
        /// </summary>
        [ForeignKey(nameof(Carro))]
        public int CarrosFK { get; set; }
        public Carros Carro { get; set; }



    }
}
