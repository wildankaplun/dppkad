namespace Dppkad.Models.Entities
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DppkadEntities : DbContext
    {
        public DppkadEntities()
            : base("name=DppkadEntities")
        {
        }

        public virtual DbSet<DAFTPHK3> DAFTPHK3 { get; set; }
        public virtual DbSet<DAFTUNIT> DAFTUNITs { get; set; }
        public virtual DbSet<MATANGR> MATANGRs { get; set; }
        public virtual DbSet<SP2D> SP2D { get; set; }
        public virtual DbSet<SP2DDETR> SP2DDETR { get; set; }
        public virtual DbSet<SPM> SPMs { get; set; }
        public virtual DbSet<TAgenda> TAgendas { get; set; }
        public virtual DbSet<TBanner> TBanners { get; set; }
        public virtual DbSet<TBerita> TBeritas { get; set; }
        public virtual DbSet<TGrafik> TGrafiks { get; set; }
        public virtual DbSet<TRealisasiSkpd> TRealisasiSkpds { get; set; }
        public virtual DbSet<TUser> TUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DAFTUNIT>()
                .Property(e => e.TYPE)
                .IsFixedLength();

            modelBuilder.Entity<MATANGR>()
                .Property(e => e.TYPE)
                .IsFixedLength();

            modelBuilder.Entity<TGrafik>()
                .Property(e => e.Budget)
                .HasPrecision(18, 3);

            modelBuilder.Entity<TGrafik>()
                .Property(e => e.Realisasi)
                .HasPrecision(18, 3);

            modelBuilder.Entity<TRealisasiSkpd>()
                .Property(e => e.UnitSkpd)
                .IsUnicode(false);

            modelBuilder.Entity<TRealisasiSkpd>()
                .Property(e => e.TotalBudget)
                .HasPrecision(18, 3);

            modelBuilder.Entity<TRealisasiSkpd>()
                .Property(e => e.TotalRealisasi)
                .HasPrecision(18, 3);

            modelBuilder.Entity<TRealisasiSkpd>()
                .Property(e => e.SisaBudget)
                .HasPrecision(18, 3);

            modelBuilder.Entity<TRealisasiSkpd>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<TUser>()
                .Property(e => e.UserRole)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<TUser>()
                .Property(e => e.UserStatus)
                .IsFixedLength()
                .IsUnicode(false);
        }
    }
}
