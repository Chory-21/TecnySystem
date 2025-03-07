using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TecnySystem.Models
{

    [Table("tallas_falla_tela")]
    public class TallaFallaTela
    {
        [Key]
        public int id_tallas_falla_tela { get; set; }
        public int id_falla_tela { get; set; }
        public string talla { get; set; }
        public int cantidad_afectada { get; set; }
        [ForeignKey("id_falla_tela")]
        public FallaTela FTela { get; set; }
    }
}
