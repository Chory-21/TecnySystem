using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TecnySystem.Models
{
    [Table("fallas_tela")]
    public class FallaTela
    {
        [Key]
        public int id_falla_tela { get; set; }

        public int id_desc_prenda { get; set; }

        [ForeignKey("id_desc_prenda")]
        public Desc_Prendas DescPrenda { get; set; }

        public string codigo_falla_tela { get; set; } 

        public string descripcion_FTela { get; set; }

        [Column(TypeName = "VARCHAR(20)")]
        public EstadoFallaTela estado { get; set; }

        public enum EstadoFallaTela
        {
            OFERTA,
            PERDIDA
        }
        public ICollection<TallaFallaTela> TallasFallaTela { get; set; }


        public FallaTela()
        {
            TallasFallaTela = new List<TallaFallaTela>();
        }
    }
}
