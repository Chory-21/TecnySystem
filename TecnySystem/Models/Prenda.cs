using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TecnySystem.Models
{
    [Table("prendas")]
    public class Prenda
    {
        [Key]
        public int id_prenda { get; set; } // Clave primaria

        public string codigo_prenda { get; set; } // Código de la prenda

        public int id_desc_prenda { get; set; } // ¿Referencia a otra tabla?


        [ForeignKey("id_desc_prenda")]
        public Desc_Prendas DescPrenda { get; set; }
    }
}
