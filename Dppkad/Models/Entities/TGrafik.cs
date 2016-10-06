namespace Dppkad.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TGrafik")]
    public partial class TGrafik
    {
        [Key]
        public int IdGrafik { get; set; }

        public int TahunBudget { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Budget { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Realisasi { get; set; }
    }
}
