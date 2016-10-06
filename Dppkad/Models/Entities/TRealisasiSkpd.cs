namespace Dppkad.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TRealisasiSkpd")]
    public partial class TRealisasiSkpd
    {
        [Key]
        public int IdRealisasi { get; set; }

        [Required]
        [StringLength(50)]
        public string UnitSkpd { get; set; }

        [Column(TypeName = "numeric")]
        public decimal TotalBudget { get; set; }

        [Column(TypeName = "numeric")]
        public decimal TotalRealisasi { get; set; }

        public int? Persentase { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SisaBudget { get; set; }

        [StringLength(30)]
        public string Status { get; set; }
    }
}
