namespace Dppkad.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SP2DDETR
    {
        public int SP2DDETRID { get; set; }

        [Required]
        [StringLength(10)]
        public string KDKEGUNIT { get; set; }

        [Required]
        [StringLength(10)]
        public string MTGKEY { get; set; }

        [Required]
        [StringLength(10)]
        public string UNITKEY { get; set; }

        [Required]
        [StringLength(20)]
        public string NOSP2D { get; set; }

        public int NOJETRA { get; set; }

        [StringLength(10)]
        public string KDDANA { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? NILAI { get; set; }
    }
}
