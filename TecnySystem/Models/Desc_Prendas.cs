using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TecnySystem.Models
{
    [Table("desc_prendas")]
    public class Desc_Prendas
    {
        [Key]
        [Column("id_desc_prenda")]
        public int IdDescPrenda { get; set; }

        [Required]
        [StringLength(50)]
        [Column("codigo_lote")]
        public string CodigoLote { get; set; }

        [StringLength(50)]
        [Column("categoria")]
        public string Categoria { get; set; }

        [Required]
        [StringLength(100)]
        [Column("modelo")]
        public string Modelo { get; set; }

        [Required]
        [StringLength(50)]
        [Column("color")]
        public string Color { get; set; }

        [Required]
        [Column("fecha_registro")]
        public DateTime FechaRegistro { get; set; }

        // Relaciones
        public ICollection<Talla> Tallas { get; set; }
        public ICollection<Prendas> Prendas { get; set; }

        public Desc_Prendas()
        {
            Tallas = new List<Talla>();
            Prendas = new List<Prendas>();
        }
    }
}

