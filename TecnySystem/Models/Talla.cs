using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TecnySystem.Models
{
    [Table("tallas")]
    public class Talla
    {
        [Key]
        public int id_talla { get; set; }

        public int id_desc_prenda { get; set; }

        public string talla { get; set; }

        public int cantidad { get; set; }

        [ForeignKey("id_desc_prenda")]
        public Desc_Prendas DescPrenda { get; set; }
    }
}
