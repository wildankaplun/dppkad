namespace Dppkad.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MATANGR")]
    public partial class MATANGR
    {
        [Key]
        [StringLength(20)]
        public string KDPER { get; set; }

        [Required]
        [StringLength(10)]
        public string MTGKEY { get; set; }

        [Required]
        [StringLength(255)]
        public string NMPER { get; set; }

        public int MTGLEVEL { get; set; }

        public int KDKHUSUS { get; set; }

        [Required]
        [StringLength(2)]
        public string TYPE { get; set; }
    }
}
