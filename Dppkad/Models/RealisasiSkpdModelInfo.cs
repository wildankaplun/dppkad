using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dppkad.Models
{
    public class RealisasiSkpdModelView
    {
        public IEnumerable<BannerSkpdInfo> BannerViewInfo { get; set; }
        public IEnumerable<BeritaSkpdInfo> BeritaViewInfo { get; set; }
        public IEnumerable<ChartSkpdInfo> ChartViewInfo { get; set; }
    }

    public class RealisasiSkpdModelInfo
    {
        public string No { get; set; }
        public string UnitSkpd { get; set; }
        public decimal TotalBudget { get; set; }
        public decimal TotalRealisasi { get; set; }
        public int? Persentase { get; set; }
        public decimal? SisaBudget { get; set; }
        public string Status { get; set; }
    }

    public class RealisasiSkpdModelUpdate
    {
        [Required(ErrorMessage = "Unit/SKPD tidak boleh kosong..!")]
        public string UnitSkpd { get; set; }

        [Required(ErrorMessage = "Total Anggaran tidak boleh kosong..!")]
        public decimal TotalBudget { get; set; }

        [Required(ErrorMessage = "Total Realisasi tidak boleh kosong..!")]
        public decimal TotalRealisasi { get; set; }
        
        public int? Persentase { get; set; }
        public decimal? SisaBudget { get; set; }
        public string Status { get; set; }
    }

    public class UserDppkadUpdate
    {
        [Required(ErrorMessage = "Username tidak boleh kosong..!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password tidak boleh kosong..!")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string UserPassword { get; set; }

        [Required(ErrorMessage = "Email tidak boleh kosong..!")]
        [EmailAddress(ErrorMessage = "Email tidak valid..!")]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "Hak Akses User tidak boleh kosong..!")]
        public string UserRole { get; set; }

        [Required(ErrorMessage = "Status User tidak boleh kosong..!")]
        public string UserStatus { get; set; }
    }

    public class BannerSkpdInfo
    {
        public string BannerName { get; set; }
        public string BannerPath { get; set; }
        public bool? ActiveFlag { get; set; }
        public int SortOrder { get; set; }
    }

    public class BannerSkpdUpdate
    {
        public string BannerName { get; set; }
        public string BannerPath { get; set; }
        public bool? ActiveFlag { get; set; }
        public int SortOrder { get; set; }
    }

    public class AgendaSkpdInfo
    {
        public int No { get; set; }
        public string Hari { get; set; }
        public string Tanggal { get; set; }        
        public string Jam { get; set; }
        public string Uraian { get; set; }
        public string Keterangan { get; set; }
    }

    public class AgendaSkpdUpdate
    {
        [Required(ErrorMessage = "Hari tidak boleh kosong..!")]
        public string Hari { get; set; }
        
        [Required(ErrorMessage = "Tanggal tidak boleh kosong..!")]
        public string Tanggal { get; set; }

        [Required(ErrorMessage = "Jam tidak boleh kosong..!")]
        public string Jam { get; set; }

        [Required(ErrorMessage = "Uraian tidak boleh kosong..!")]
        public string Uraian { get; set; }
        
        public string Keterangan { get; set; }
    }

    public class ChartSkpdInfo
    {
        public int TahunBudget { get; set; }
        public decimal? Budget { get; set; }
        public decimal? Realisasi { get; set; }
    }

    public class ChartSkpdUpdate
    {
        [Required(ErrorMessage = "Tahun budget tidak boleh kosong..!")]
        public int TahunBudget { get; set; }
        public decimal? Budget { get; set; }
        public decimal? Realisasi { get; set; }
    }

    public class BeritaSkpdInfo
    {
        public string News { get; set; }
        public bool? ActiveFlag { get; set; }
    }

    public class BeritaSkpdUpdate
    {
        [Required(ErrorMessage = "Berita tidak boleh kosong..!")]
        public string News { get; set; }
        public bool? ActiveFlag { get; set; }
    }
}