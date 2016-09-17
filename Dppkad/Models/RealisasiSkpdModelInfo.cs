using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dppkad.Models
{
    public class RealisasiSkpdModelView
    {
        public IEnumerable<BannerSkpdInfo> BannerViewInfo { get; set; }
        public IEnumerable<BeritaSkpdInfo> BeritaViewInfo { get; set; }
    }

    public class RealisasiSkpdModelInfo
    {
        public int No { get; set; }
        public string UnitSkpd { get; set; }
        public decimal TotalBudget { get; set; }
        public decimal TotalRealisasi { get; set; }
        public int? Persentase { get; set; }
        public decimal? SisaBudget { get; set; }
        public string Status { get; set; }
    }

    public class BannerSkpdInfo
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

    public class BeritaSkpdInfo
    {
        public string News { get; set; }
        public bool? ActiveFlag { get; set; }
    }
}