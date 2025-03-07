using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TecnySystem.Models
{
    [Table("fallas_lavanderia")]
    public class FallaLavanderia
    {
        [Key]
        public int id_falla_lavanderia { get; set; }

        public int id_desc_prenda { get; set; }

        [ForeignKey("id_desc_prenda")]
        public Desc_Prendas DescPrenda { get; set; }

        public string codigo_falla_lavanderia { get; set; }

        public string desc_FLavanderia { get; set; }

        public string estado { get; set; }

        public ICollection<TallaFallaLavanderia> TallasFallaLavanderia { get; set; }


        public FallaLavanderia()
        {
            TallasFallaLavanderia = new List<TallaFallaLavanderia>();
        }
    }
}

