using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TecnySystem.Models
{
    [Table("desc_prendas")]
    public class Desc_Prendas
    {
        [Key]
        public int id_desc_prenda { get; set; }

        public string codigo_lote { get; set; }

        public string categoria { get; set; }

        public string modelo { get; set; }

        public string color { get; set; }

        public DateTime fecha_registro { get; set; }

        // Relaciones con otras tablas
        public ICollection<Talla> Tallas { get; set; }
        public ICollection<Prenda> Prendas { get; set; }
        public ICollection<FallaTela> FallasTela { get; set; }
        public ICollection<FallaLavanderia> FallasLavanderia { get; set; }

        public Desc_Prendas()
        {
            Tallas = new List<Talla>();
            Prendas = new List<Prenda>();
            FallasTela = new List<FallaTela>();
            FallasLavanderia = new List<FallaLavanderia>();
        }
    }
}
