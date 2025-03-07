//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace TecnySystem.Models
//{
//    [Table("prendas_fallas")]
//    public class Prendas_Fallas
//    {
//        [Key]
//        [Column("id_prendas_fallas")]
//        public int IdPrendasFallas { get; set; }

//        [Required]
//        [StringLength(50)]
//        [Column("codigo_prenda_fallas")]
//        public string CodigoPrendaFallas { get; set; }

//        [Required]
//        [Column("id_prenda")]
//        public int IdPrenda { get; set; }

//        [ForeignKey("IdPrenda")]
//        public Prendas Prenda { get; set; }

//        [Column("cat_FTela")]
//        public int? CatFTela { get; set; }

//        [StringLength(200)]
//        [Column("desc_FTela")]
//        public string DescFTela { get; set; }

//        [Column("estado_FTela")]
//        public EstadoFalla? EstadoFTela { get; set; }

//        [Column("cat_FLavanderia")]
//        public int? CatFLavanderia { get; set; }

//        [StringLength(200)]
//        [Column("desc_FLavanderia")]
//        public string DescFLavanderia { get; set; }

//        [Column("estado_FLavanderia")]
//        public EstadoLavanderia? EstadoFLavanderia { get; set; }
//    }

//    // Enum para estado de fallas en tela
//    public enum EstadoFalla
//    {
//        OFERTA,
//        PERDIDA
//    }

//    // Enum para estado de fallas en lavandería
//    public enum EstadoLavanderia
//    {
//        PROCESO_RECUPERACION,
//        PERDIDA
//    }
//}
