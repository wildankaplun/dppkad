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
    public interface IRealisasiBelanjaService
    {
        void SaveContext();

        IEnumerable<RealisasiBelanjaModelInfo> GetRealisasiBelanjaList(Expression<Func<RealisasiBelanjaModelInfo, bool>> where, int take, int skip, 
            Expression<Func<RealisasiBelanjaModelInfo, string>> sort, string sortType);
        int TotalRealisasiBelanja();
        int TotalRealisasiBelanja(Expression<Func<RealisasiBelanjaModelInfo, bool>> where);

        IEnumerable<BannerInfo> GetBannerInfo();

        IEnumerable<AgendaInfo> GetAgendaInfoList(Expression<Func<AgendaInfo, bool>> where, int take, int skip,
            Expression<Func<AgendaInfo, string>> sort, string sortType);
        int TotalAgendaInfo();
        int TotalAgendaInfo(Expression<Func<AgendaInfo, bool>> where);

        IEnumerable<BeritaInfo> GetBeritaInfo();
    }

    public class RealisasiBelanjaService : IRealisasiBelanjaService
    {
        #region constructor
        private readonly IRealisasiBelanjaRepository _repository;
        private readonly IGenericRepository<SPM> _spmRepository;
        private readonly IGenericRepository<SP2D> _sp2dRepository;
        private readonly IGenericRepository<DAFTPHK3> _daftPhk3Repository;
        private readonly IGenericRepository<DAFTUNIT> _daftUnitRepository;
        private readonly IGenericRepository<MATANGR> _matangRepository;
        private readonly IGenericRepository<TAgenda> _agendaRepository;
        private readonly IGenericRepository<TBanner> _bannerRepository;
        private readonly IGenericRepository<TBerita> _beritaRepository;

        private readonly IUnitOfWork _unitOfWork;
        #endregion

        public RealisasiBelanjaService(IRealisasiBelanjaRepository repository
            , IGenericRepository<SPM> spmRepository
            , IGenericRepository<SP2D> sp2dRepository
            , IGenericRepository<DAFTPHK3> daftPhk3Repository
            , IGenericRepository<DAFTUNIT> daftUnitRepository
            , IGenericRepository<MATANGR> matangRepository
            , IGenericRepository<TAgenda> agendaRepository
            , IGenericRepository<TBanner> bannerRepository
            , IGenericRepository<TBerita> beritaRepository
            , IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _spmRepository = spmRepository;
            _sp2dRepository = sp2dRepository;
            _daftPhk3Repository = daftPhk3Repository;
            _daftUnitRepository = daftUnitRepository;
            _matangRepository = matangRepository;
            _agendaRepository = agendaRepository;
            _bannerRepository = bannerRepository;
            _beritaRepository = beritaRepository;
            _unitOfWork = unitOfWork;
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

        private IQueryable<RealisasiBelanjaModelInfo> QueryLookup
        {
            get
            {
                var SpmGrp = (from a in _sp2dRepository.AsQueryable()
                              join b in _spmRepository.AsQueryable() on a.UNITKEY.Trim() equals b.UNITKEY.Trim() into leftQuery
                              from leftJoin in leftQuery.DefaultIfEmpty()
                               join c in 
                                   (
                                        from c1 in _repository.AsQueryable()
                                        group c1 by new { c1.UNITKEY, c1.MTGKEY, c1.NOSP2D } into grp
                                        select new
                                        {
                                            UNITKEY = grp.Key.UNITKEY,
                                            MTGKEY = grp.Key.MTGKEY,
                                            NOSP2D = grp.Key.NOSP2D,
                                            NILAI = grp.Sum(c => c.NILAI ?? 0)
                                        }).AsQueryable() on a.UNITKEY.Trim() equals c.UNITKEY.Trim()
                               select new
                               {
                                   a.UNITKEY,
                                   c.MTGKEY,
                                   a.NO_SP2D,
                                   a.NOSPM,
                                   a.TGLSP2D,
                                   a.KEPERLUAN,
                                   c.NILAI,
                                   STATUS = string.IsNullOrEmpty(a.NOSPM) ? "Proses" : "Terbit"
                               });

                return (from a in SpmGrp.AsQueryable()
                        join b in _daftPhk3Repository.AsQueryable() on a.UNITKEY.Trim() equals b.UNITKEY.Trim() into leftQueryB
                        from leftJoinB in leftQueryB.DefaultIfEmpty()
                        join c in _daftUnitRepository.AsQueryable() on a.UNITKEY.Trim() equals c.UNITKEY.Trim() into leftQueryC
                        from leftJoinC in leftQueryC.DefaultIfEmpty()
                        join d in _matangRepository.AsQueryable() on a.MTGKEY.Trim() equals d.MTGKEY.Trim() into leftQueryD
                        from leftJoinD in leftQueryD.DefaultIfEmpty()
                        select new RealisasiBelanjaModelInfo()
                        {
                            No = 0,
                            NoSPM = a.NOSPM,
                            NoSP2D = a.NO_SP2D,
                            TglSP2D = a.TGLSP2D,
                            Unit = leftJoinC.NMUNIT,
                            Kegiatan = a.KEPERLUAN,
                            Pihak3 = leftJoinB.NMINST,
                            Kota = leftJoinC.ALAMAT,
                            NilaiKontrak = a.NILAI,
                            Status = a.STATUS
                        });
            }
        }

        public int TotalRealisasiBelanja()
        {
            return QueryLookup.Count();
        }

        public int TotalRealisasiBelanja(Expression<Func<RealisasiBelanjaModelInfo, bool>> where)
        {
            if (@where == null)
                return TotalRealisasiBelanja();
            return QueryLookup.Where(where).Count();
        }

        public IEnumerable<RealisasiBelanjaModelInfo> GetRealisasiBelanjaList(Expression<Func<RealisasiBelanjaModelInfo, bool>> where, int take, int skip,
            Expression<Func<RealisasiBelanjaModelInfo, string>> sort, string sortType)
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

        private IQueryable<AgendaInfo> QueryAgendaLookup
        {
            get
            {
                return (from a in _agendaRepository.AsQueryable()
                        select new AgendaInfo()
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

        public int TotalAgendaInfo(Expression<Func<AgendaInfo, bool>> where)
        {
            if (@where == null)
                return TotalAgendaInfo();
            return QueryAgendaLookup.Where(where).Count();
        }

        public IEnumerable<AgendaInfo> GetAgendaInfoList(Expression<Func<AgendaInfo, bool>> where, int take, int skip,
            Expression<Func<AgendaInfo, string>> sort, string sortType)
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

        public IEnumerable<BannerInfo> GetBannerInfo()
        {
            var result = (from b in _bannerRepository.AsQueryable()
                          where b.ActiveFlag == true
                          orderby b.SortOrder
                          select new BannerInfo()
                          {
                              BannerName = b.BannerName,
                              BannerPath = b.BannerPath,
                              ActiveFlag = b.ActiveFlag ?? false,
                              SortOrder = b.SortOrder ?? 0,
                          });
            return result.AsEnumerable();
        }

        public IEnumerable<BeritaInfo> GetBeritaInfo()
        {
            var result = (from c in _beritaRepository.AsQueryable()
                          where c.ActiveFlag == true
                          select new BeritaInfo()
                          {
                              News = c.News,
                              ActiveFlag = c.ActiveFlag ?? false,
                          });
            return result.AsEnumerable();
        }
    }
}