namespace Dppkad.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TUser
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [StringLength(255)]
        public string UserPassword { get; set; }

        [Required]
        [StringLength(255)]
        public string UserEmail { get; set; }

        [Required]
        [StringLength(3)]
        public string UserRole { get; set; }

        [Required]
        [StringLength(1)]
        public string UserStatus { get; set; }
    }
}
