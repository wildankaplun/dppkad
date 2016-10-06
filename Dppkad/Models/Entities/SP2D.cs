namespace Dppkad.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SP2D
    {
        [Key]
        [StringLength(20)]
        public string NO_SP2D { get; set; }

        [Required]
        [StringLength(10)]
        public string UNITKEY { get; set; }

        public int KDSTATUS { get; set; }

        [StringLength(40)]
        public string NOSPM { get; set; }

        [Required]
        [StringLength(10)]
        public string KEYBEND { get; set; }

        [Required]
        [StringLength(10)]
        public string IDXSKO { get; set; }

        [Required]
        [StringLength(10)]
        public string IDXTTD { get; set; }

        [StringLength(10)]
        public string KDP3 { get; set; }

        public int IDXKODE { get; set; }

        [Required]
        [StringLength(10)]
        public string NOREG { get; set; }

        [StringLength(20)]
        public string KETOTOR { get; set; }

        [StringLength(40)]
        public string NOKONTRAK { get; set; }

        public string KEPERLUAN { get; set; }

        public int? PENOLAKAN { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? TGLVALID { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? TGLSP2D { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? TGLSPM { get; set; }

        [StringLength(10)]
        public string NOBANTU { get; set; }
    }
}
