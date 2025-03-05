using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace TecnySystem.Models
{
    [Table("prendas")]
    public class Prendas
    {
        [Key]
        [Column("id_prenda")]
        public int IdPrenda { get; set; }

        [Required]
        [StringLength(50)]
        [Column("codigo_prenda")]
        public string CodigoPrenda { get; set; }

        [Required]
        [Column("id_desc_prenda")]
        public int IdDescPrenda { get; set; }

        [ForeignKey("IdDescPrenda")]
        public Desc_Prendas DescPrenda { get; set; }

        public ICollection<Prendas_Fallas> PrendasFallas { get; set; }

        public Prendas()
        {
            PrendasFallas = new List<Prendas_Fallas>();
        }
    }
}
