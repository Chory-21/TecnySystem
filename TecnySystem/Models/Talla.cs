using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TecnySystem.Models
{
    [Table("tallas")]
    public class Talla
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_talla")]
        public int IdTalla { get; set; }

        [Required]
        [Column("id_desc_prenda")]
        public int IdDescPrenda { get; set; }

        [Required]
        [StringLength(10)]
        [Column("talla")]
        public string NombreTalla { get; set; }

        [Required]
        [Column("cantidad")]
        public int Cantidad { get; set; }

        [ForeignKey("IdDescPrenda")]
        public Desc_Prendas DescPrenda { get; set; }
    }
}
