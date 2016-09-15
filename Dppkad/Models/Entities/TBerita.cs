namespace Dppkad.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TBerita")]
    public partial class TBerita
    {
        [Key]
        public int IdNews { get; set; }

        [Required]
        [StringLength(255)]
        public string News { get; set; }

        public bool? ActiveFlag { get; set; }
    }
}
