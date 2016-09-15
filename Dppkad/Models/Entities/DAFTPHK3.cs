namespace Dppkad.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DAFTPHK3
    {
        [Key]
        [StringLength(10)]
        public string KDP3 { get; set; }

        [StringLength(50)]
        public string NMP3 { get; set; }

        [StringLength(50)]
        public string NMINST { get; set; }

        [StringLength(20)]
        public string NORCP3 { get; set; }

        [StringLength(50)]
        public string NMBANK { get; set; }

        [StringLength(50)]
        public string JNSUSAHA { get; set; }

        public string ALAMAT { get; set; }

        [StringLength(20)]
        public string TELEPON { get; set; }

        [StringLength(20)]
        public string NPWP { get; set; }

        [StringLength(10)]
        public string UNITKEY { get; set; }
    }
}
