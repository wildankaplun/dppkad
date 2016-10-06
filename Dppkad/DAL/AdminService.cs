using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using Dppkad.Models;
using Dppkad.Models.Entities;
using Dppkad.BL;
using Dppkad.BL.Repository;

namespace Dppkad.DAL
{
    public interface IAdminService
    {
        bool AddBannerInfo(TBanner banner);
        bool UpdateBannerInfo(TBanner banner);
        IEnumerable<TBanner> GetBannerInfoById(bool flag);
        bool DeleteBannerInfo(TBanner banner);

        bool AddAgendaInfo(TAgenda agenda);

        bool AddBeritaInfo(TBerita berita);

        bool AddGraphInfo(TGrafik graph);

        bool AddRealisasiInfo(TRealisasiSkpd graph);


        void SaveContext();

        IEnumerable<RealisasiSkpdModelInfo> GetRealisasiSkpdList(Expression<Func<RealisasiSkpdModelInfo, bool>> where, int take, int skip,
            Expression<Func<RealisasiSkpdModelInfo, string>> sort, string sortType);
        int TotalRealisasiSkpd();
        int TotalRealisasiSkpd(Expression<Func<RealisasiSkpdModelInfo, bool>> where);

        IEnumerable<BannerSkpdInfo> GetBannerInfo();

        IEnumerable<AgendaSkpdInfo> GetAgendaInfoList(Expression<Func<AgendaSkpdInfo, bool>> where, int take, int skip,
            Expression<Func<AgendaSkpdInfo, string>> sort, string sortType);

        IEnumerable<ChartSkpdInfo> GetGraphInfoList(Expression<Func<ChartSkpdInfo, bool>> where, int take, int skip,
            Expression<Func<ChartSkpdInfo, string>> sort, string sortType);

        IEnumerable<ChartSkpdInfo> GetChartInfo();

        int TotalAgendaInfo();
        int TotalAgendaInfo(Expression<Func<AgendaSkpdInfo, bool>> where);

        int TotalGraphInfo();
        int TotalGraphInfo(Expression<Func<ChartSkpdInfo, bool>> where);

        IEnumerable<BeritaSkpdInfo> GetBeritaInfo();
    }
    public class AdminService : IAdminService
    {
        #region constructor
        private readonly IAdminRepository _realisasiRepository;
        private readonly IGrafikRepository _grafikRepository;
        private readonly IGenericRepository<TAgenda> _agendaRepository;
        private readonly IGenericRepository<TBanner> _bannerRepository;
        private readonly IGenericRepository<TBerita> _beritaRepository;

        private readonly IUnitOfWork _unitOfWork;
        #endregion

        public AdminService(IAdminRepository realisasiRepository
            , IGrafikRepository grafikRepository
            , IGenericRepository<TAgenda> agendaRepository
            , IGenericRepository<TBanner> bannerRepository
            , IGenericRepository<TBerita> beritaRepository
            , IUnitOfWork unitOfWork)
        {
            _realisasiRepository = realisasiRepository;
            _grafikRepository = grafikRepository;
            _agendaRepository = agendaRepository;
            _bannerRepository = bannerRepository;
            _beritaRepository = beritaRepository;
            _unitOfWork = unitOfWork;
        }

        public bool AddBannerInfo(TBanner banner)
        {
            if (banner == null)
                throw new ArgumentNullException("banner");

            try
            {
                _bannerRepository.Insert(banner);

                return true;
            }
            catch (DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity,
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
        }

        public bool UpdateBannerInfo(TBanner banner)
        {
            if (banner == null)
                throw new ArgumentNullException("banner");

            try
            {
                _bannerRepository.Update(banner);

                return true;
            }
            catch (DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity,
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
        }

        public bool DeleteBannerInfo(TBanner banner)
        {
            if (banner == null)
                throw new ArgumentNullException("banner");

            try
            {
                _bannerRepository.Delete(banner);

                return true;
            }
            catch (DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity,
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
        }

        public bool AddAgendaInfo(TAgenda agenda)
        {
            if (agenda == null)
                throw new ArgumentNullException("agenda");

            try
            {
                _agendaRepository.Insert(agenda);

                return true;
            }
            catch (DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity,
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
        }

        public bool AddBeritaInfo(TBerita berita)
        {
            if (berita == null)
                throw new ArgumentNullException("berita");

            try
            {
                _beritaRepository.Insert(berita);

                return true;
            }
            catch (DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity,
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
        }

        public bool AddGraphInfo(TGrafik grafik)
        {
            if (grafik == null)
                throw new ArgumentNullException("grafik");

            try
            {
                _grafikRepository.Insert(grafik);

                return true;
            }
            catch (DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity,
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
        }

        public bool AddRealisasiInfo(TRealisasiSkpd realisasi)
        {
            if (realisasi == null)
                throw new ArgumentNullException("realisasi");

            try
            {
                _realisasiRepository.Insert(realisasi);

                return true;
            }
            catch (DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity,
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
        }

        /// <summary>
        /// commit to database 
        /// </summary>
        public void SaveContext()
        {
            try
            {
                _unitOfWork.Save();
            }
            catch (DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity,
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
        }

        private IQueryable<RealisasiSkpdModelInfo> QueryLookup
        {
            get
            {
                return (from a in _realisasiRepository.AsQueryable()
                        select new RealisasiSkpdModelInfo()
                        {
                            No = "0",
                            UnitSkpd = a.UnitSkpd,
                            TotalBudget = a.TotalBudget,
                            TotalRealisasi = a.TotalRealisasi,
                            Persentase = a.Persentase,
                            SisaBudget = a.SisaBudget,
                            Status = a.Status            
                        });
            }
        }

        public int TotalRealisasiSkpd()
        {
            return QueryLookup.Count();
        }

        public int TotalRealisasiSkpd(Expression<Func<RealisasiSkpdModelInfo, bool>> where)
        {
            if (@where == null)
                return TotalRealisasiSkpd();
            return QueryLookup.Where(where).Count();
        }

        public IEnumerable<RealisasiSkpdModelInfo> GetRealisasiSkpdList(Expression<Func<RealisasiSkpdModelInfo, bool>> where, int take, int skip,
            Expression<Func<RealisasiSkpdModelInfo, string>> sort, string sortType)
        {
            if (sortType.Equals("asc"))
            {
                if (@where == null)
                {
                    return QueryLookup.OrderBy(sort).Skip(skip).AsEnumerable();
                }
                return QueryLookup.Where(@where).OrderBy(sort).Skip(skip).AsEnumerable();
            }
            if (@where == null)
                return QueryLookup.OrderByDescending(sort).Skip(skip).AsEnumerable();
            return QueryLookup.Where(@where).OrderByDescending(sort).Skip(skip).AsEnumerable();
        }

        private IQueryable<AgendaSkpdInfo> QueryAgendaLookup
        {
            get
            {
                return (from a in _agendaRepository.AsQueryable()
                        select new AgendaSkpdInfo()
                        {
                            No = 0,
                            Hari = a.Hari,
                            Tanggal = a.Tanggal,
                            Jam = a.Jam,
                            Uraian = a.Uraian,
                            Keterangan = a.Keterangan
                        });
            }
        }

        public int TotalAgendaInfo()
        {
            return QueryAgendaLookup.Count();
        }

        public int TotalAgendaInfo(Expression<Func<AgendaSkpdInfo, bool>> where)
        {
            if (@where == null)
                return TotalAgendaInfo();
            return QueryAgendaLookup.Where(where).Count();
        }

        public IEnumerable<AgendaSkpdInfo> GetAgendaInfoList(Expression<Func<AgendaSkpdInfo, bool>> where, int take, int skip,
            Expression<Func<AgendaSkpdInfo, string>> sort, string sortType)
        {
            if (sortType.Equals("asc"))
            {
                if (@where == null)
                {
                    return QueryAgendaLookup.OrderBy(sort).Skip(skip).Take(take).AsEnumerable();
                }
                return QueryAgendaLookup.Where(@where).OrderBy(sort).Skip(skip).Take(take).AsEnumerable();
            }
            if (@where == null)
                return QueryAgendaLookup.OrderByDescending(sort).Skip(skip).Take(take).AsEnumerable();
            return QueryAgendaLookup.Where(@where).OrderByDescending(sort).Skip(skip).Take(take).AsEnumerable();
        }

        public IEnumerable<BannerSkpdInfo> GetBannerInfo()
        {
            var result = (from b in _bannerRepository.AsQueryable()
                          where b.ActiveFlag == true
                          orderby b.SortOrder
                          select new BannerSkpdInfo()
                          {
                              BannerName = b.BannerName,
                              BannerPath = b.BannerPath,
                              ActiveFlag = b.ActiveFlag ?? false,
                              SortOrder = b.SortOrder ?? 0,
                          });
            return result.AsEnumerable();
        }

        public IEnumerable<TBanner> GetBannerInfoById(bool flag)
        {
            var result = _bannerRepository.AsQueryable(c => c.ActiveFlag == flag);
            return result;
        }

        public IEnumerable<BeritaSkpdInfo> GetBeritaInfo()
        {
            var result = (from c in _beritaRepository.AsQueryable()
                          where c.ActiveFlag == true
                          orderby c.CreatedDate ascending
                          select new BeritaSkpdInfo()
                          {
                              News = c.News,
                              ActiveFlag = c.ActiveFlag ?? false,
                          });
            return result.Skip(0).Take(10).AsEnumerable();
        }

        public IEnumerable<ChartSkpdInfo> GetChartInfo()
        {
            var result = (from c in _grafikRepository.AsQueryable()
                          orderby c.TahunBudget
                          select new ChartSkpdInfo()
                          {
                              TahunBudget = c.TahunBudget,
                              Budget = c.Budget,
                              Realisasi = c.Realisasi
                          });
            return result.AsEnumerable();
        }

        private IQueryable<ChartSkpdInfo> QueryGraphLookup
        {
            get
            {
                return (from a in _grafikRepository.AsQueryable()
                        select new ChartSkpdInfo()
                        {
                            TahunBudget = a.TahunBudget,
                            Budget = a.Budget,
                            Realisasi = a.Realisasi
                        });
            }
        }

        public int TotalGraphInfo()
        {
            return QueryGraphLookup.Count();
        }

        public int TotalGraphInfo(Expression<Func<ChartSkpdInfo, bool>> where)
        {
            if (@where == null)
                return TotalGraphInfo();
            return QueryGraphLookup.Where(where).Count();
        }

        public IEnumerable<ChartSkpdInfo> GetGraphInfoList(Expression<Func<ChartSkpdInfo, bool>> where, int take, int skip,
            Expression<Func<ChartSkpdInfo, string>> sort, string sortType)
        {
            if (sortType.Equals("asc"))
            {
                if (@where == null)
                {
                    return QueryGraphLookup.OrderBy(sort).Skip(skip).Take(take).AsEnumerable();
                }
                return QueryGraphLookup.Where(@where).OrderBy(sort).Skip(skip).Take(take).AsEnumerable();
            }
            if (@where == null)
                return QueryGraphLookup.OrderByDescending(sort).Skip(skip).Take(take).AsEnumerable();
            return QueryGraphLookup.Where(@where).OrderByDescending(sort).Skip(skip).Take(take).AsEnumerable();
        }
    }
}