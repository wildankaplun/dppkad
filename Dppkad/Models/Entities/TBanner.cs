namespace Dppkad.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TBanner")]
    public partial class TBanner
    {
        [Key]
        public int IdBanner { get; set; }

        [Required]
        [StringLength(30)]
        public string BannerName { get; set; }

        public string BannerPath { get; set; }

        public bool? ActiveFlag { get; set; }

        public int? SortOrder { get; set; }
    }
}
