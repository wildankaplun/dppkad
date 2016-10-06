namespace Dppkad.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DAFTUNIT")]
    public partial class DAFTUNIT
    {
        [Key]
        [StringLength(20)]
        public string KDUNIT { get; set; }

        [Required]
        [StringLength(10)]
        public string UNITKEY { get; set; }

        public int KDLEVEL { get; set; }

        [Required]
        [StringLength(50)]
        public string NMUNIT { get; set; }

        [StringLength(40)]
        public string AKROUNIT { get; set; }

        public string ALAMAT { get; set; }

        [StringLength(20)]
        public string TELEPON { get; set; }

        [StringLength(2)]
        public string TYPE { get; set; }
    }
}
