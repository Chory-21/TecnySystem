using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TecnySystem.Models
{

    [Table("tallas_falla_lavanderia")]
    public class TallaFallaLavanderia
    {
        [Key]
        public int id_tallas_falla_lavanderia { get; set; }
        public int id_falla_lavanderia { get; set; }
        public string talla { get; set; }
        public int cantidad_afectada { get; set; }

        [ForeignKey("id_falla_lavanderia")]
        public FallaLavanderia FLavanderia { get; set; }
    }
}
