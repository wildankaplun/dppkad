namespace Dppkad.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TAgenda")]
    public partial class TAgenda
    {
        [Key]
        public int IdAgenda { get; set; }

        [StringLength(25)]
        public string Hari { get; set; }

        [StringLength(10)]
        public string Tanggal { get; set; }

        [StringLength(10)]
        public string Jam { get; set; }

        [StringLength(100)]
        public string Uraian { get; set; }

        [StringLength(50)]
        public string Keterangan { get; set; }
    }
}
