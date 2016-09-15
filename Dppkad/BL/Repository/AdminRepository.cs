using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;
using Dppkad.BL;
using Dppkad.BL.Repository;
using Dppkad.Models.Entities;

namespace Dppkad.BL.Repository
{
    public interface IAdminRepository
    {
        IEnumerable<TRealisasiSkpd> SelectAll();
        TRealisasiSkpd SelectById(int Id);
        void Insert(TRealisasiSkpd obj);
        void Update(TRealisasiSkpd obj);
        void UpdateWithRowVersion(TRealisasiSkpd obj, byte[] rowVersion);

        IQueryable<TRealisasiSkpd> AsQueryable(Expression<Func<TRealisasiSkpd, bool>> where);
        IQueryable<TRealisasiSkpd> AsQueryable();
    }

    public class AdminRepository : RepositoryBase<TRealisasiSkpd>, IAdminRepository
    {
        public AdminRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}