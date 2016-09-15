using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dppkad.Models
{
    public class RealisasiBelanjaModelView
    {
        public IEnumerable<BannerInfo> BannerViewInfo { get; set; }
        public IEnumerable<BeritaInfo> BeritaViewInfo { get; set; }
        public UserDppkadInfo UserViewInfo { get; set; }
    }

    public class RealisasiBelanjaModelInfo
    {
        public int No { get; set; }

        public string NoSPM { get; set; }

        public string NoSP2D { get; set; }

        public DateTime? TglSP2D { get; set; }

        public string Unit { get; set; }

        public string Kegiatan { get; set; }

        public string Pihak3 { get; set; }

        public string Kota { get; set; }

        public decimal? NilaiKontrak { get; set; }

        public string Status { get; set; }
    }

    public class BannerInfo
    {
        public string BannerName { get; set; }
        public string BannerPath { get; set; }
        public bool? ActiveFlag { get; set; }
        public int SortOrder { get; set; }
    }

    public class AgendaInfo
    {
        public int No { get; set; }
        public string Hari { get; set; }
        public string Tanggal { get; set; }
        public string Jam { get; set; }
        public string Uraian { get; set; }
        public string Keterangan { get; set; }
    }

    public class BeritaInfo
    {
        public string News { get; set; }
        public bool? ActiveFlag { get; set; }
    }

    public class UserDppkadInfo
    {
        public int UserId { get; set; }

        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string UserPassword { get; set; }
        public string UserEmail { get; set; }
        public string UserRole { get; set; }
        public string UserStatus { get; set; }
    }
}